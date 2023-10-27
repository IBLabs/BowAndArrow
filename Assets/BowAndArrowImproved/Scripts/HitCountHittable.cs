using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class HitCountHittable : MonoBehaviour
{
    
    [SerializeField] private int hitCount;
    [SerializeField] private LayerMask hitMask;
    
    public UnityEvent onDeath;
    public UnityEvent onHit;

    public void SetCount(int baseHitCount)
    {
        hitCount = baseHitCount;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (hitMask.Contains(other.attachedRigidbody.gameObject.layer))
        {
            hitCount--;
            
            onHit.Invoke();
            
            if (hitCount <= 0)
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
