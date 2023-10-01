using UnityEngine;

namespace BowAndArrowImproved.Scripts
{
    public class BAAICircleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float radius;
        [SerializeField] private bool relativePosition;

        public void SpawnOnCircleEdge()
        {
            if (prefabToSpawn == null)
            {
                Debug.Log("[ERROR]: no prefab to instantiate");
                return;
            }

            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            Vector3 spawnPos = new Vector3(x, y, 0);

            if (relativePosition)
            {
                spawnPos += transform.position;
            }

            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        }
    }
}