using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using DG.Tweening;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
        if (!layerMask.Contains(other.gameObject.layer)) return;
        
        float distance = 10f;
        float sphereRadius = 1f;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Collider[] hittedColliders = Physics.OverlapSphere(transform.position, sphereRadius, layerMask);

        if (hittedColliders.Length > 0)
        {
            Predicate<Collider> predicate = (collider) =>
            {
                return collider.gameObject != other.gameObject;
            };

            Collider hittedCollider = Array.Find(hittedColliders, predicate);

            if (hittedCollider == null) return;
            
            Debug.Log($"[TEST]: bouncing to {other.gameObject.name}");

            Vector3 hitDirection = (hittedCollider.transform.position - transform.position).normalized;
            
            BouncingArrow newArrow = Instantiate(nextArrow, transform.position, Quaternion.LookRotation(hitDirection));
            newArrow.ShootToPreTarget(hittedCollider.transform);
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

        transform.DOMove(target.position, duration).onComplete += () =>
        {
            if (TryGetComponent<BAAIEnemy>(out var enemy))
            {
                enemy.Die(true);
            }
        };
    }
}
