using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAAISpawnParticleSystemOnDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemPrefab;

    public void SpawnParticleSystem()
    {
        Instantiate(particleSystemPrefab, transform.position + Vector3.up * .5f, Quaternion.LookRotation(transform.forward * -1));
    }
}
