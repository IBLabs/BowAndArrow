using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BAAIWaveSpawner : MonoBehaviour
{
    public UnityEvent<int> updateScoreboard;
    public UnityEvent waveFinished;

    [SerializeField] private int enemyToken = 10;
    [SerializeField] private List<EnemyConfiguration> enemies = new();
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private ParticleSystem.MinMaxCurve spawnInterval;

    private int _currentWave = 1;
    private int _enemiesToKill = 0;
    private bool _didLoseGame;

    private List<GameObject> _spawnedEnemies = new();

    private void OnEnemyDeath(GameObject enemyGameObject, int scoreValue, bool killedByPlayer)
    {
        if (!_spawnedEnemies.Remove(enemyGameObject))
        {
            Debug.LogError("[ERROR]: failed to remove enemy from spawned enemy list, enemy not found in list");
        }

        if (killedByPlayer)
        {
            updateScoreboard?.Invoke(scoreValue);
        }

        _enemiesToKill -= 1;


        if (_enemiesToKill <= 0 && !_didLoseGame)
        {
            _currentWave++;
            waveFinished.Invoke();
        }
    }

    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.Lose)
        {
            _didLoseGame = true;
            DestroyEnemies();
        }
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

            Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
            GameObject newEnemy = Instantiate(enemyToSpawn, spawnLocation.position, Quaternion.identity, transform);

            _spawnedEnemies.Add(newEnemy);

            if (newEnemy.TryGetComponent(out BAAIIDeathable deathable))
            {
                deathable.onDeath.AddListener(OnEnemyDeath);
            }

            if (newEnemy.TryGetComponent(out BAAIINavMeshAgentHolder navMeshComponent))
            {
                navMeshComponent.SetTargetTransform(targetTransform);
            }
        }
    }

    private void DestroyEnemies()
    {
        for (int i = _spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (_spawnedEnemies[i].TryGetComponent(out BAAIEnemy curEnemy))
            {
                curEnemy.Die(false);
            }
        }
    }

    [Serializable]
    public class EnemyConfiguration
    {
        public GameObject enemyPrefab;
        public int cost;
    }
}