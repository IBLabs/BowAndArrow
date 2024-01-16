using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BAAIStickingArrowToSurface : Arrow
{
    [SerializeField] private GameObject stickingArrowPrefab;

    protected override void HandleNonEnemyHit(Collision other)
    {
        gameObject.SetActive(false);

        SpawnStickingArrow();
        SpawnHitEffect();
    }

    protected override void HandleEnemyHit(Collision other)
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

    private void SpawnStickingArrow()
    {
        GameObject stickingArrow = Instantiate(stickingArrowPrefab);
        stickingArrow.transform.position = transform.position + transform.forward.normalized * 0.2f;
        stickingArrow.transform.forward = transform.forward;
        stickingArrow.transform.parent = transform;
    }
}
