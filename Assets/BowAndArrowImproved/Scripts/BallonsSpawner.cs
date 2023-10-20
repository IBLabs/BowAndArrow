﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BallonsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private ParticleSystem.MinMaxCurve spawnInterval;
    [SerializeField] private Ballon ballonPrefab;
    [SerializeField] private int baseNumOfBallons;
    private int _curWaveNumOfBallons;
    private int _currentWave = 0;
    
    private List<Ballon> _spawnedBallons = new List<Ballon>();

    private float _spawnTimer;

    public void OnWaveDidEnd()
    {
        DestroyBallons();
    }
    
    public void OnWaveDidStart()
    {
        GenerateWave();
    }

    private void DestroyBallons()
    {
        _curWaveNumOfBallons = 0;
     
        foreach (Ballon ballon in _spawnedBallons)
        {
            Destroy(ballon.gameObject);
        }
        
        _spawnedBallons.Clear();
    }
    
    private void GenerateWave()
    {
        _currentWave++;
        _curWaveNumOfBallons = baseNumOfBallons * _currentWave;
    }
    private void Update()
    {
        GenerateBallons();
    }

    private void GenerateBallons()
    {
        if (_curWaveNumOfBallons <= 0) return;
        
        if (_spawnTimer <= 0)
        {
            Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
            Ballon newBallon = Instantiate(ballonPrefab, spawnLocation.position, Quaternion.identity);
            _spawnedBallons.Add(newBallon);

            _curWaveNumOfBallons--;
    
            _spawnTimer = spawnInterval.Evaluate(Random.value);
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }
    }
}