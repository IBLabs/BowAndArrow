using System.Collections.Generic;
using UnityEngine;

public class BAAIStickingArrowToSurface : MonoBehaviour
{
    [SerializeField] private GameObject stickingArrowPrefab;
    [SerializeField] private GameObject hitEffect;
    
    private List<string> destroyingTags = new List<string>()
    {
        "Enemy",
        "Balloon",
        "Portal",
        "Armor"
    };

    private void OnCollisionEnter(Collision other)
    {
        if (destroyingTags.Contains(other.gameObject.tag))
        {
            Destroy(gameObject);
            return;
        }
        
        gameObject.SetActive(false);

        SpawnStickingArrow();
        SpawnHitEffect();

        Destroy(gameObject);
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
