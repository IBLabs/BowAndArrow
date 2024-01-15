using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplodingArrow : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticleSystem;
    [SerializeField] private Rigidbody arrowRb;
    [SerializeField] private AudioClip explosionSfx;

    [SerializeField] private float hitRadius = 4f;
    [SerializeField] private LayerMask hitLayerMask;

    private void Update()
    {
        Quaternion targetRot = Quaternion.LookRotation(arrowRb.velocity.normalized);
        arrowRb.MoveRotation(targetRot);
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 hitPoint = other.contacts.First().point;

        SpawnAudioSource(hitPoint);

        Instantiate(explosionParticleSystem, hitPoint, Quaternion.identity);
        Destroy(gameObject);

        Collider[] hitColliders = Physics.OverlapSphere(hitPoint, hitRadius, hitLayerMask);
        
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<BAAIEnemy>(out var enemy))
            {
                enemy.Die(true);
            }
        }
    }

    private void SpawnAudioSource(Vector3 position)
    {
        GameObject audioSourceGameObject = new GameObject("Exploding Arrow Audio Source");
        
        AudioSource audioSource = audioSourceGameObject.AddComponent<AudioSource>();
        audioSource.pitch = 1f + Random.Range(-.2f, .2f);
        // audioSource.spatialBlend = 1f;
        audioSource.volume = 1f;
        
        audioSource.PlayOneShot(explosionSfx);
        
        Destroy(audioSourceGameObject, 3f);
    }
}
