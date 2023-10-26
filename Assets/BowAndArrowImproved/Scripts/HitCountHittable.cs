using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HitCountHittable : MonoBehaviour
{
    public int health;
    
    private int _curHitCount;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private int baseHitCount;
    public UnityEvent onDeath;
    public UnityEvent onHit;

    private void Awake()
    {
        ResetCount();
    }

    public void ResetCount()
    {
        _curHitCount = baseHitCount;
        health = baseHitCount;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (hitMask.Contains(other.attachedRigidbody.gameObject.layer))
        {
            _curHitCount--;
            
            onHit.Invoke();
            
            if (_curHitCount <= 0)
            {
                onDeath.Invoke();
            }

            BAAIEnemy hitEnemy = other.attachedRigidbody.GetComponent<BAAIEnemy>();
            if (hitEnemy != null)
            {
                hitEnemy.Die(false);
            }
        }
    }
}
