using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BAAIEnemy : MonoBehaviour
{
    public UnityEvent onDeath;

    [SerializeField] private List<AudioClip> breakClips;

    private void OnCollisionEnter(Collision other)
    {
        onDeath.Invoke();
        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(breakClips[Random.Range(0, breakClips.Count)], transform.position);
    }
}
