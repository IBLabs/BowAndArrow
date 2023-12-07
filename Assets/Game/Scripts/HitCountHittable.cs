using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class HitCountHittable : MonoBehaviour
{
    [SerializeField] private int hitCount;
    [SerializeField] private LayerMask hitMask;

    public UnityEvent onDeath;
    public UnityEvent onHit;
    public int health => hitCount;

    private void OnTriggerEnter(Collider other)
    {
        if (hitMask.Contains(other.attachedRigidbody.gameObject.layer))
        {
            hitCount--;

            if (hitCount <= 0)
            {
                onDeath?.Invoke();
            }
            else
            {
                if (other.attachedRigidbody.TryGetComponent(out BAAIEnemy hitEnemy)) hitEnemy.Die(false);

                onHit.Invoke();
            }
        }
    }
}