using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BAAIWaveSpawner : MonoBehaviour
{
    [SerializeField] private int enemyToken = 10;
    [SerializeField] private List<EnemyConfiguration> enemies = new();
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private ParticleSystem.MinMaxCurve spawnInterval;
    [SerializeField] private float spawnIntervalDecreaseFactor = 0.15f;
    private int _prevSpawnLocationIndex = -1;
    public UnityEvent waveFinished;

    private int _currentWave = 1;
    private int _enemiesToKill = 0;
    private List<GameObject> _spawnedEnemies = new();

    public void OnEnemyDeath(GameObject enemyGameObject)
    {
        if (!_spawnedEnemies.Remove(enemyGameObject))
        {
            Debug.LogError("[ERROR]: failed to remove enemy from spawned enemy list, enemy not found in list");
        }

        _enemiesToKill -= 1;
        
        if (_enemiesToKill <= 0) EndWave();
    }

    private void EndWave()
    {
        if (spawnInterval.constantMin > 0.1f)
        {
            spawnInterval.constantMin -= spawnIntervalDecreaseFactor;
        }
            
        if (spawnInterval.constantMax > 0.3f)
        {
            spawnInterval.constantMax -= spawnIntervalDecreaseFactor;
        }

        _currentWave++;
        waveFinished.Invoke();
    }

    public void Generate()
    {
        List<GameObject> enemiesToSpawn = GenerateEnemies(_currentWave * enemyToken);
        _enemiesToKill = enemiesToSpawn.Count;
        StartCoroutine(SpawnEnemiesCoroutine(enemiesToSpawn));
    }

    private List<GameObject> GenerateEnemies(int waveValue)
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randomEnemyId = Random.Range(0, enemies.Count);
            int randomEnemyCost = enemies[randomEnemyId].cost;

            if (waveValue - randomEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randomEnemyId].enemyPrefab);
                waveValue -= randomEnemyCost;
            }
        }

        return generatedEnemies;
    }
    
    private IEnumerator SpawnEnemiesCoroutine(List<GameObject> enemiesToSpawn)
    {
        foreach (GameObject enemyToSpawn in enemiesToSpawn)
        {
            float waitTime = spawnInterval.Evaluate(Random.value);
            yield return new WaitForSeconds(waitTime);

            Transform spawnLocation = spawnLocations[GetSpawnLocation()];
            GameObject newEnemy = Instantiate(enemyToSpawn, spawnLocation.position, Quaternion.identity, transform);

            _spawnedEnemies.Add(newEnemy);

            BAAIIDeathable deathable = newEnemy.GetComponent<BAAIIDeathable>();
            if (deathable != null)
            {
                deathable.onDeath.AddListener(OnEnemyDeath);
            }

            BAAIINavMeshAgentHolder navMeshComponent = newEnemy.GetComponent<BAAIINavMeshAgentHolder>();
            if (navMeshComponent != null)
            {
                navMeshComponent.SetTargetTransform(targetTransform);
            }
        }
    }

    private int GetSpawnLocation()
    {
        int newSpawnLocationIndex;
        do
        {
            newSpawnLocationIndex = Random.Range(0, spawnLocations.Count);
        } while (newSpawnLocationIndex == _prevSpawnLocationIndex);

        _prevSpawnLocationIndex = newSpawnLocationIndex;
        return newSpawnLocationIndex;
    }

    [Serializable]
    public class EnemyConfiguration
    {
        public GameObject enemyPrefab;
        public int cost;
    }
}
