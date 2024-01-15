using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BAAIWaveSpawner : MonoBehaviour
{
    public int currentWave => _currentWave;
    
    public UnityEvent<int> enemyDidDie;
    public UnityEvent waveFinished;

    [SerializeField] private int enemyToken = 10;
    [SerializeField] private List<EnemyConfiguration> enemies = new();
    [SerializeField] private List<WayPointController> spawnLocations;
    [SerializeField] private ParticleSystem.MinMaxCurve spawnInterval;
    [SerializeField] private float spawnIntervalDecreaseFactor = 0.15f;
    private readonly List<GameObject> _spawnedEnemies = new();
    private int _currentWave = 1;
    private bool _didLoseGame;
    private int _enemiesToKill;

    private int _prevSpawnLocationIndex = -1;

    private void OnEnemyDeath(GameObject enemyGameObject, int scoreValue, bool killedByPlayer)
    {
        if (!_spawnedEnemies.Remove(enemyGameObject))
            Debug.LogError("[ERROR]: failed to remove enemy from spawned enemy list, enemy not found in list");

        if (killedByPlayer) enemyDidDie?.Invoke(scoreValue);

        _enemiesToKill -= 1;

        if (_enemiesToKill <= 0 && !_didLoseGame) EndWave();
    }

    private void EndWave()
    {
        if (spawnInterval.constantMin > 0.1f) spawnInterval.constantMin -= spawnIntervalDecreaseFactor;

        if (spawnInterval.constantMax > 0.3f) spawnInterval.constantMax -= spawnIntervalDecreaseFactor;

        _currentWave++;

        waveFinished.Invoke();
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
        var enemiesToSpawn = GenerateEnemies(_currentWave * enemyToken);
        _enemiesToKill = enemiesToSpawn.Count;
        StartCoroutine(SpawnEnemiesCoroutine(enemiesToSpawn));
    }

    private List<GameObject> GenerateEnemies(int waveValue)
    {
        var generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            var randomEnemyId = Random.Range(0, enemies.Count);
            var randomEnemyCost = enemies[randomEnemyId].cost;

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
        foreach (var enemyToSpawn in enemiesToSpawn)
        {
            var waitTime = spawnInterval.Evaluate(Random.value);
            yield return new WaitForSeconds(waitTime);

            var spawnLocation = spawnLocations[GetSpawnLocationIndex()];
            var newEnemy = Instantiate(enemyToSpawn, spawnLocation.transform.position, spawnLocation.transform.rotation, transform);

            _spawnedEnemies.Add(newEnemy);

            if (newEnemy.TryGetComponent(out BAAIIDeathable deathable)) deathable.onDeath.AddListener(OnEnemyDeath);

            if (newEnemy.TryGetComponent(out NavMeshAgent agent))
                agent.SetDestination(spawnLocation.GetRandomNextWayPoint().position);
        }
    }

    private int GetSpawnLocationIndex()
    {
        int newSpawnLocationIndex;

        do
        {
            newSpawnLocationIndex = Random.Range(0, spawnLocations.Count);
        } while (newSpawnLocationIndex == _prevSpawnLocationIndex);

        _prevSpawnLocationIndex = newSpawnLocationIndex;

        return newSpawnLocationIndex;
    }

    private void DestroyEnemies()
    {
        for (var i = _spawnedEnemies.Count - 1; i >= 0; i--)
            if (_spawnedEnemies[i].TryGetComponent(out BAAIEnemy curEnemy))
                curEnemy.Die(false);
    }

    [Serializable]
    public class EnemyConfiguration
    {
        public GameObject enemyPrefab;
        public int cost;
    }
}