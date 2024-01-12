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


    private void Awake()
    {
        DOTween.Init();
    }

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
        Collider newTarget = FindNewTarget(other, hitColliders);
        
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

    private Collider FindNewTarget(Collision other, Collider[] hitColliders)
    {
        foreach (Collider hitCollider in hitColliders)
        {
            if (other.collider != hitCollider && layerMask.Contains(other.gameObject.layer))
            {
                return hitCollider;
            }
        }

        return null;
    }

    private void ShootToPreTarget(Collider target)
    {
        if (TryGetComponent<Rigidbody>(out var arrowRb))
        {
            // arrowRb.isKinematic = true;
            // arrowRb.useGravity = false;
        }
        Debug.Log("target pos: " + target.transform.position);
        Debug.Log("before lerp: " + transform.position);
        transform.DOMove(target.transform.position, duration);
        // StartCoroutine(MoveObject(targetPos));
        // transform.position = targetPos;
        // transform.position = Vector3.MoveTowards(transform.position, targetPos, duration * Time.deltaTime);
        Debug.Log("after lerp: " + transform.position);

    }

    private void SpawnHitEffect()
    {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = transform.position;
        
        Destroy(effect, 2f);
    }
}