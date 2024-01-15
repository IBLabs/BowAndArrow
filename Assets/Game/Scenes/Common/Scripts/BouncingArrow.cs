using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BouncingArrow : MonoBehaviour
{
    [SerializeField] private float radius = 6f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private BouncingArrow nextArrow;
    [SerializeField] private float force = 10f;
    [SerializeField] private int bounceCount;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float hitRaiseFactor = 0.5f;

    private void OnCollisionEnter(Collision other)
    {
        if (!layerMask.Contains(other.gameObject.layer))
        {
            HandleNonEnemyHit();
        }
        else
        {
            HandleEnemyHit(other);
        }
        
        Destroy(gameObject);
    }

    private void HandleNonEnemyHit()
    {
        SpawnHitEffect();
    }
    
    private void HandleEnemyHit(Collision other)
    {
        if (other.gameObject.TryGetComponent<BAAIEnemy>(out var enemy))
        {
            enemy.Die(true);
        }
        else
        {
            Destroy(other.gameObject);
        }
        
        HandleArrowBounce(other);
    }

    private void HandleArrowBounce(Collision other)
    {
        if (other.contacts.Length == 0) return;
        Vector3 hitPoint = other.contacts.First().point;

        Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, radius, layerMask)
            .Except(new[] { other.collider })
            .ToArray();

        if (!FindClosestEnemy(hitPoint, hitColliders, out var closestCollider))
        {
            return;
        }

        Vector3 newTargetRaisedPos = closestCollider.transform.position + Vector3.up * hitRaiseFactor;
        
        if (bounceCount <= 0) return;
        ShootNewArrow(newTargetRaisedPos, hitPoint);
    }

    private bool FindClosestEnemy(Vector3 hitPoint, Collider[] hitColliders, out Collider closestCollider)
    {
        closestCollider = null;

        float closestDistance = Mathf.Infinity;

        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(hitPoint, hitCollider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = hitCollider;
            }
        }

        return (closestCollider != null);
    }

    private void ShootNewArrow(Vector3 target, Vector3 origin)
    {
        Vector3 direction = (target - origin).normalized;
        
        BouncingArrow newArrow = Instantiate(nextArrow, origin, Quaternion.LookRotation(direction));
        newArrow.bounceCount = bounceCount - 1;
        if (!newArrow.TryGetComponent<Rigidbody>(out var newArrowRb))
        {
            Destroy(newArrow);
            return;
        }

        newArrowRb.useGravity = false;
        newArrowRb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void SpawnHitEffect()
    {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = transform.position;

        Destroy(effect, 2f);
    }
}