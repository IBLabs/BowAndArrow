using System.Collections;
using System.Collections.Generic;
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
    private IEnumerator _activeGenerateCoroutine;

    public void DestroyBallons()
    {
        StopCoroutine(_activeGenerateCoroutine);
     
        foreach (Ballon balloon in _spawnedBallons)
        {
            balloon.Die(Random.value);
        }
        
        _spawnedBallons.Clear();
    }
    
    public void Generate()
    {
        _currentWave++;

        int balloonCount = baseNumOfBallons * _currentWave;


        _activeGenerateCoroutine = GenerateCoroutine(balloonCount);
        StartCoroutine(_activeGenerateCoroutine);
    }

    private IEnumerator GenerateCoroutine(int balloonCount)
    {
        for (int i = 0; i < balloonCount; i++)
        {
            float waitTime = spawnInterval.Evaluate(Random.value);
            
            yield return new WaitForSeconds(waitTime);
            
            Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Count)];
            Ballon newBallon = Instantiate(ballonPrefab, spawnLocation.position, Quaternion.identity, transform);
            _spawnedBallons.Add(newBallon);
        }
    }
}