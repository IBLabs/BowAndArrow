using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAAISpawnParticleSystemOnDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemPrefab;

    [SerializeField] private Vector3 offset;

    public void SpawnParticleSystem()
    {
        Instantiate(
            particleSystemPrefab, 
            transform.position + offset,
            Quaternion.identity
        );
    }
}