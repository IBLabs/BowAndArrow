using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Ballon : MonoBehaviour, BAAIIDeathable
{
    public bool isBalloonFrozen;
    
    private void Start()
    {
        rb.useGravity = !isBalloonFrozen;
    }

    [SerializeField] private float buoyancyForce = 10f;
    [SerializeField] private List<AudioClip> popClips;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private string weaponHitTag = "Arrow";

    [SerializeField] private UnityEvent<GameObject> _onDeath;
    public UnityEvent<GameObject> onDeath => _onDeath;

    void FixedUpdate()
    {
        if (isBalloonFrozen) return;

        Vector3 buoyancyDirection = Vector3.up * buoyancyForce;
        rb.AddForce(buoyancyDirection, ForceMode.Force);
    }

    public void Die(float delay)
    {
        StartCoroutine(DieCoroutine(delay));
    }

    private IEnumerator DieCoroutine(float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(popClips[Random.Range(0, popClips.Count)], transform.position);
        _onDeath?.Invoke(this.gameObject);
        
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(weaponHitTag)){
            Die(0);
        }
    }
}