using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Ballon : MonoBehaviour, BAAIIDeathable
{
    private bool _isBalloonFrozen;

    public bool isBalloonFrozen
    {
        get { return _isBalloonFrozen; }
        set
        {
            _isBalloonFrozen = value;
            rb.useGravity = false;
        }
    }

    [SerializeField] private float buoyancyForce = 10f;
    [SerializeField] private List<AudioClip> popClips;
    [SerializeField] private Rigidbody rb;

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
        Die(0);
    }
}