using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class HitCountHittable : MonoBehaviour
{
    public int health => hitCount;
    
    [SerializeField] private int hitCount;
    [SerializeField] private LayerMask hitMask;
    
    public UnityEvent onDeath;
    public UnityEvent onHit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hitMask.Contains(other.attachedRigidbody.gameObject.layer))
        {
            hitCount--;
            
            onHit.Invoke();
            
            if (hitCount <= 0)
            {
                onDeath?.Invoke();
            }
            else
            {
                BAAIEnemy hitEnemy = other.attachedRigidbody.GetComponent<BAAIEnemy>();
                if (hitEnemy != null)
                {
                    hitEnemy.Die(false);
                }
            }
        }
    }
}
