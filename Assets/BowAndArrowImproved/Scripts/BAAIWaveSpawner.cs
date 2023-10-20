using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BAAIWaveSpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyConfiguration> enemies = new List<EnemyConfiguration>();
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private ParticleSystem.MinMaxCurve spawnInterval;

    public UnityEvent waveFinished;

    private int _currentWave = 1;
    private List<GameObject> _enemiesToSpawn = new List<GameObject>();
    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private float _spawnTimer;
    
    private float _waveTimer;

    private void Update()
    {
        if (_spawnTimer <= 0)
        {
            if (_enemiesToSpawn.Count > 0)
            {
                Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
                GameObject newEnemy = Instantiate(_enemiesToSpawn[0], spawnLocation.position, Quaternion.identity);

                BAAIIDeathable enemyComponent = newEnemy.GetComponent<BAAIIDeathable>(); 
                if (enemyComponent != null)
                {
                    enemyComponent.onDeath.AddListener(OnEnemyDeath);
                }
                
                BAAIINavMeshAgentHolder navMeshComponent = newEnemy.GetComponent<BAAIINavMeshAgentHolder>(); 
                if (navMeshComponent != null)
                {
                    navMeshComponent.SetTargetTransform(targetTransform);
                }
                
                _enemiesToSpawn.RemoveAt(0);
                _spawnedEnemies.Add(newEnemy);

                _spawnTimer = spawnInterval.Evaluate(Random.value);
            }
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }
    }

    public void OnEnemyDeath(GameObject enemyGameObject)
    {
        if (!_spawnedEnemies.Remove(enemyGameObject))
        {
            Debug.LogError("[ERROR]: failed to remove enemy from spawned enemy list, enemy not found in list");
        }
        
        if (_spawnedEnemies.Count == 0)
        {
            _currentWave++;

            waveFinished.Invoke();
        }
    }

    public void Generate()
    {
        GenerateEnemies(_currentWave * 10);
    }

    private void GenerateEnemies(int waveValue)
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
        
        _enemiesToSpawn.Clear();
        _enemiesToSpawn = generatedEnemies;
    }

    [Serializable]
    public class EnemyConfiguration
    {
        public GameObject enemyPrefab;
        public int cost;
    }
}
