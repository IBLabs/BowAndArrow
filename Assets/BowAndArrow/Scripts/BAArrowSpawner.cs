using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace BowAndArrow.Scripts
{
    public class BAArrowSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform spawnLocation;
        [SerializeField] private XRBaseInteractable bowGrabInteractable;

        [SerializeField] private float arrowSpawnDelay = .5f;

        private GameObject _currentArrow;
        private bool _isArrowNotched;

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
                StartCoroutine(SpawnArrowWithDelay(arrowSpawnDelay));
            }
        }

        private void OnPullDidRelease(float amount)
        {
            _isArrowNotched = false;
        }

        private IEnumerator SpawnArrowWithDelay(float delay)
        {
            _isArrowNotched = true;
            
            yield return new WaitForSeconds(delay);
            _currentArrow = Instantiate(arrowPrefab, spawnLocation);
        }
    }
}