using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace BowAndArrowImproved.Scripts
{
    public class BAAICircleSpawner : MonoBehaviour
    {
        [SerializeField] private InputActionProperty triggerClick;
        
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float radius;
        [SerializeField] private bool relativePosition;
        
        [Range(0, 180)]
        [SerializeField] private float angleRange = 45.0f;

        [SerializeField] private Transform targetTransform;

        private void Start()
        {
            triggerClick.action.started += OnTriggerStarted;
        }

        private void OnTriggerStarted(InputAction.CallbackContext context)
        {
            SpawnOnCircleEdgeAndSetTarget();
        }

        private GameObject SpawnOnCircleEdge()
        {
            if (prefabToSpawn == null)
            {
                Debug.LogError("No prefab assigned to spawn!");
                return null;
            }

            // Generate a random angle within the defined range relative to the forward direction
            float randomAngleWithinRange = Random.Range(-angleRange, angleRange);
            float angleInRadians = randomAngleWithinRange * Mathf.Deg2Rad; // Convert to radians

            // Calculate the x and z position using the angle and the circle's radius
            float x = radius * Mathf.Sin(angleInRadians);
            float z = radius * Mathf.Cos(angleInRadians);

            // Create the position vector relative to the GameObject's forward direction
            Vector3 spawnPosition = transform.position + (transform.forward * z + transform.right * x);

            // Instantiate the prefab at the calculated position
            return Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }

        public void SpawnOnCircleEdgeAndSetTarget()
        {
            GameObject spawnedObj = SpawnOnCircleEdge();
            spawnedObj.transform.rotation = Quaternion.LookRotation(targetTransform.position - spawnedObj.transform.position);

            BAAISimpleMoveTowardsTarget spawnedObjMove = spawnedObj.GetComponent<BAAISimpleMoveTowardsTarget>();
            if (spawnedObjMove != null)
            {
                spawnedObjMove.targetPosition = targetTransform.position;
            }

            BAAISetNavMeshAgentDestination setNavMeshDest = spawnedObj.GetComponent<BAAISetNavMeshAgentDestination>();
            if (setNavMeshDest != null)
            {
                setNavMeshDest.target = targetTransform;
            }
        }
    }
}