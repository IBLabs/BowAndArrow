using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class BouncingArrow : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private BouncingArrow nextArrow;
    [SerializeField] private float duration;
    [SerializeField] private int bounceCount;
    [SerializeField] private float maxDistance = 100f;
    
    private void OnCollisionEnter(Collision other)
    {
        float distance = 10f;
        float sphereRadius = 1f;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        
        bool didHit = Physics.SphereCast(origin, sphereRadius, direction, out var hitInfo, maxDistance, layerMask);

        if (didHit)
        {
            Vector3 hitDirection = (hitInfo.transform.position - transform.position).normalized;
            
            BouncingArrow newArrow = Instantiate(nextArrow, transform.position, Quaternion.LookRotation(hitDirection));
            newArrow.ShootToPreTarget(hitInfo.transform);
            newArrow.bounceCount = bounceCount - 1;
        }
    }

    private void ShootToPreTarget(Transform target)
    {
        if (TryGetComponent<Rigidbody>(out var arrowRb))
        {
            arrowRb.isKinematic = true;
            arrowRb.useGravity = false;
        }
        
        transform.DOMove(target.position, duration);
    }
}
