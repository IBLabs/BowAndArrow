using System;
using System.Collections;
using DG.Tweening;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BouncingArrow : MonoBehaviour
{
    [SerializeField] private float radius = 6f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private BouncingArrow nextArrow;
    [SerializeField] private float duration;
    [SerializeField] private int bounceCount;
    [SerializeField] private GameObject hitEffect;

    private void OnCollisionEnter(Collision other)
    {
        Vector3 origin = other.gameObject.transform.position;
        
        if (!layerMask.Contains(other.gameObject.layer))
        {
            SpawnHitEffect();
            Destroy(gameObject);
            return;
        }
        
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius, layerMask);
        Collider newTarget = FindClosestEnemy(other, hitColliders);
        
        if (newTarget == null) return;

        HandleNewArrow(newTarget, origin);
    }

    private void HandleNewArrow(Collider newTarget, Vector3 origin)
    {
        Vector3 hitDirection = (newTarget.transform.position - origin).normalized;
        
        BouncingArrow newArrow = Instantiate(nextArrow, origin, Quaternion.LookRotation(hitDirection));
        newArrow.ShootToPreTarget(newTarget);
        newArrow.bounceCount = bounceCount - 1;
        Destroy(gameObject);
    }

    private Collider FindClosestEnemy(Collision other, Collider[] hitColliders)
    {
        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hitCollider in hitColliders)
        {
            if (other.collider == hitCollider || !layerMask.Contains(other.gameObject.layer)) continue;
                
            float distance = Vector3.Distance(other.transform.position, hitCollider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = hitCollider;
            }
        }

        return closestCollider;
    }

    private void ShootToPreTarget(Collider target)
    {
        if (TryGetComponent<Rigidbody>(out var arrowRb))
        {
            Destroy(arrowRb);
            // arrowRb.isKinematic = true;
            // arrowRb.useGravity = false;
        }

        Debug.Log("target pos: " + target.transform.position);
        Debug.Log("before lerp: " + transform.position);

        StartCoroutine(MoveTowardsTarget(target));
    }

    private IEnumerator MoveTowardsTarget(Collider target)
    {
        while (transform.position != target.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, duration * Time.deltaTime);
            Debug.Log("after lerp: " + transform.position);

            yield return null;
        }

        Debug.Log("Reached the target!");
    }

    private void SpawnHitEffect()
    {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = transform.position;
        
        Destroy(effect, 2f);
    }
}