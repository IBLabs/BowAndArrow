using UnityEngine;

public class BAAIStickingArrowToSurface : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private SphereCollider myCollider;

    [SerializeField] private GameObject stickingArrowPrefab;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") 
            || other.gameObject.CompareTag("Balloon")
            || other.gameObject.CompareTag("Portal"))
        {
            Destroy(gameObject);
            return;
        }
        
        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject stickingArrow = Instantiate(stickingArrowPrefab);
        stickingArrow.transform.position = transform.position;
        stickingArrow.transform.forward = transform.forward;

        if (other.collider.attachedRigidbody != null)
        {
            stickingArrow.transform.parent = other.collider.attachedRigidbody.transform;
        }
        
        Destroy(gameObject);
    }
}
