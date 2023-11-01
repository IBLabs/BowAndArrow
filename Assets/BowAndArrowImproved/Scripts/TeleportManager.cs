using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject playerContainer;
    [SerializeField] private List<GameObject> portalsList;
    [SerializeField] private GameObject curPortal;
    
    [SerializeField] private AudioClip teleportationClip;
    public void EnablePortals()
    {
        foreach (GameObject portal in portalsList)
        {
            if (portal != curPortal)
            {
                portal.SetActive(true);
            }
        }
    }

    public void DisablePortals()
    {
        foreach (GameObject portal in portalsList)
        {
            portal.SetActive(false);
        }
    }

    private void Teleport(GameObject portal)
    {
        playerContainer.transform.position = portal.transform.position;
        playerContainer.transform.rotation = Quaternion.LookRotation(portal.transform.forward);
        AudioSource.PlayClipAtPoint(teleportationClip, playerContainer.transform.position);


        DisablePortalAtPlayerLocation(portal);
    }

    private void DisablePortalAtPlayerLocation(GameObject portal)
    {
        GameObject prevPortal = curPortal;
        curPortal = portal;

        curPortal.SetActive(false);
        prevPortal.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        Collider myCollider = other.GetContact(0).thisCollider;

        if (other.gameObject.CompareTag("Arrow"))
        {
            Teleport(myCollider.gameObject);
        }
    }
}