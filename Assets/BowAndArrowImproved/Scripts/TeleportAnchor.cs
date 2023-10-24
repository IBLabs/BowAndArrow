using UnityEngine;
using UnityEngine.Events;

public class TeleportAnchor : MonoBehaviour
{
    public UnityEvent<TeleportAnchor> teleportByArrow;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void EnableAnchor()
    {
        gameObject.SetActive(true);
    }

    public void DisableAnchor()
    {
        gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            teleportByArrow?.Invoke(this);
        }
    }
}