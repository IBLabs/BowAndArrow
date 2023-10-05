using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Ballon : MonoBehaviour
{
    [SerializeField] private float buoyancyForce = 10f;
    [SerializeField] private UnityEvent<GameObject> onPop;
    [SerializeField] private List<AudioClip> popClips;
    [SerializeField] private Rigidbody rb;

    void FixedUpdate()
    {
        Vector3 buoyancyDirection = Vector3.up * buoyancyForce;
        rb.AddForce(buoyancyDirection, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hitttttt");
        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(popClips[Random.Range(0, popClips.Count)], transform.position);
    }
}