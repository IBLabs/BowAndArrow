using Unity.XR.CoreUtils;
using UnityEngine;

public abstract class Arrow : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] private GameObject hitEffect;

    protected void OnCollisionEnter(Collision other)
    {
        if (!layerMask.Contains(other.gameObject.layer))
        {
            HandleNonEnemyHit(other);
        }
        else
        {
            HandleEnemyHit(other);
        }

        if (gameObject) Destroy(gameObject);
    }

    protected void SpawnHitEffect()
    {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = transform.position;

        Destroy(effect, 2f);
    }
    
    protected abstract void HandleNonEnemyHit(Collision other);
    protected abstract void HandleEnemyHit(Collision other);
}