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
    [SerializeField] private float duration;
    [SerializeField] private int bounceCount;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float hitRaiseFactor = 0.5f;

    private void OnCollisionEnter(Collision other)
    {
        if (!layerMask.Contains(other.gameObject.layer))
        {
            HandleNonEnemyHit();
            return;
        }

        if (other.gameObject.TryGetComponent<BAAIEnemy>(out var enemy))
        {
            enemy.Die(true);
        }
        else
        {
            Destroy(other.gameObject);
        }
        
        Destroy(gameObject);
        
        if (other.contacts.Length == 0) return;

        Vector3 hitPoint = other.contacts.First().point;

        Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, radius, layerMask)
            .Except(new[] { other.collider })
            .ToArray();

        if (!FindClosestEnemy(hitPoint, hitColliders, out var closestCollider))
        {
            Debug.Log("[ERROR]: failed to find closest enemy");
            return;
        }

        Vector3 newTargetRaisedPos = closestCollider.transform.position + Vector3.up * hitRaiseFactor;
        
        ShootNewArrow(newTargetRaisedPos, hitPoint);
    }

    private void HandleNonEnemyHit()
    {
        SpawnHitEffect();
        Destroy(gameObject);
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
        
        Debug.DrawRay(origin, direction * 10f, Color.red, 5f);

        BouncingArrow newArrow = Instantiate(nextArrow, origin, Quaternion.LookRotation(direction));

        if (!newArrow.TryGetComponent<Rigidbody>(out var newArrowRb))
        {
            Debug.Log("[ERROR]: failed to find rigidbody on new arrow, destroying it");
            Destroy(newArrow);
            return;
        }
        
        newArrowRb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void SpawnHitEffect()
    {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = transform.position;

        Destroy(effect, 2f);
    }
}