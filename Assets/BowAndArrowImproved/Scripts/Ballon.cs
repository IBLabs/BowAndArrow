using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Ballon : MonoBehaviour, BAAIIDeathable
{
    public bool isBalloonFrozen = false;

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

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(popClips[Random.Range(0, popClips.Count)], transform.position);
        
        _onDeath?.Invoke(this.gameObject);
    }
}