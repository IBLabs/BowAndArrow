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

    [SerializeField] private int scoreValue = 0;

    [SerializeField] private UnityEvent<GameObject, int, bool> _onDeath;
    public UnityEvent<GameObject, int, bool> onDeath => _onDeath;

    void FixedUpdate()
    {
        if (isBalloonFrozen) return;

        Vector3 buoyancyDirection = Vector3.up * buoyancyForce;
        rb.AddForce(buoyancyDirection, ForceMode.Force);
    }

    public void Die(float delay, bool killedByPlayer)
    {
        StartCoroutine(DieCoroutine(delay, killedByPlayer));
    }

    private IEnumerator DieCoroutine(float delay, bool killedByPlayer)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(popClips[Random.Range(0, popClips.Count)], transform.position);
        _onDeath?.Invoke(this.gameObject, scoreValue, killedByPlayer);
        
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter(Collision other)
    {
        bool killedByPlayer = other.gameObject.CompareTag(weaponHitTag); 
        Die(0, killedByPlayer);
    }
}