using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class BAArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [FormerlySerializedAs("spawnLocation")] [SerializeField] private Transform spawnTransform;
    [SerializeField] private XRBaseInteractable bowGrabInteractable;

    [SerializeField] private GameEventScriptableObject shouldSpawnArrowGameEvent;

    [SerializeField] private float arrowSpawnDelay = .5f;

    private GameObject _currentArrow;
    private bool _isArrowNotched;

    public void OnArrowReleased()
    {
        OnPullDidRelease(1f);
    }
        
    private void OnEnable()
    {
        BAPullInteractable.PullDidRelease += OnPullDidRelease;
    }

    private void OnDisable()
    {
        BAPullInteractable.PullDidRelease -= OnPullDidRelease;
    }

    private void Update()
    {
        if (bowGrabInteractable.isSelected && !_isArrowNotched)
        {
            StartSpawnArrowWithDelay();
        }
    }

    private void OnPullDidRelease(float amount)
    {
        _isArrowNotched = false;
    }

    public void StartSpawnArrowWithDelay()
    {
        StartCoroutine(SpawnArrowWithDelay(arrowSpawnDelay));
    }

    private IEnumerator SpawnArrowWithDelay(float delay)
    {
        _isArrowNotched = true;
            
        yield return new WaitForSeconds(delay);

        Instantiate(arrowPrefab, spawnTransform);

    }
}