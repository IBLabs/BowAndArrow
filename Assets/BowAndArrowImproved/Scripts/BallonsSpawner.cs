using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallonsSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private ParticleSystem.MinMaxCurve spawnInterval;
    [SerializeField] private Ballon ballonPrefab;
    [SerializeField] private int numOfBallons;
    
    private List<Ballon> _spawnedBallons = new List<Ballon>();

    private float _spawnTimer;
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();   
    }
    
    private void Subscribe()
    {
        GameManager.WaveDidStart += OnWaveDidStart;
        GameManager.WaveDidEnd += OnWaveDidEnd;
    }

    private void Unsubscribe()
    {
        GameManager.WaveDidStart -= OnWaveDidStart;
        GameManager.WaveDidEnd -= OnWaveDidEnd;
    }
    
    private void OnWaveDidEnd()
    {
        throw new NotImplementedException();
    }
    
    private void OnWaveDidStart()
    {
    }
    private void Update()
    {
        GenerateBallons();
    }

    private void GenerateBallons()
    {
        if (numOfBallons <= 0) return;
        
        if (_spawnTimer <= 0)
        {
            Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
            Ballon newBallon = Instantiate(ballonPrefab, spawnLocation.position, Quaternion.identity);
            _spawnedBallons.Add(newBallon);

            numOfBallons--;
    
            _spawnTimer = spawnInterval.Evaluate(Random.value);
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }
    }
}