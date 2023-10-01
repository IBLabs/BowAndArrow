using UnityEngine;

namespace BowAndArrowImproved.Scripts
{
    public class BAAICircleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float radius;
        [SerializeField] private bool relativePosition;

        [SerializeField] private Transform targetTransform;

        private GameObject SpawnOnCircleEdge()
        {
            if (prefabToSpawn == null)
            {
                Debug.Log("[ERROR]: no prefab to instantiate");
                return null;
            }

            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);

            Vector3 spawnPos = new Vector3(x, 0, z);

            if (relativePosition)
            {
                spawnPos += transform.position;
            }

            GameObject spawnedObj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, transform);

            return spawnedObj;
        }

        public void SpawnOnCircleEdgeAndSetTarget()
        {
            GameObject spawnedObj = SpawnOnCircleEdge();

            BAAISimpleMoveTowardsTarget spawnedObjMove = spawnedObj.GetComponent<BAAISimpleMoveTowardsTarget>();
            if (spawnedObjMove != null)
            {
                spawnedObjMove.targetPosition = targetTransform.position;
            }
        }
    }
}