using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BAAIStickingArrowToSurface : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider myCollider;
    [SerializeField] private GameObject stickingArrowPrefab;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private LayerMask layerMask;

    private List<string> destroyingTags = new List<string>()
    {
        "Enemy",
        "Balloon",
        "Portal",
        "Armor"
    };

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
    }

    private void HandleNonEnemyHit()
    {
        gameObject.SetActive(false);

        SpawnStickingArrow();
        SpawnHitEffect();
    }
    
    private void SpawnStickingArrow()
    {
        GameObject stickingArrow = Instantiate(stickingArrowPrefab);
        stickingArrow.transform.position = transform.position + transform.forward.normalized * 0.2f;
        stickingArrow.transform.forward = transform.forward;
        stickingArrow.transform.parent = transform;
    }

    private void SpawnHitEffect()
    {
        GameObject effect = Instantiate(hitEffect);
        effect.transform.position = transform.position;
        
        Destroy(effect, 2f);
    }
}
