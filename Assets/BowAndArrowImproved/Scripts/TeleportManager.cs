using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject playerContainer;
    [SerializeField] private List<GameObject> portalsList;
    [SerializeField] private GameObject curPortal;

    [SerializeField] private float portalsSpawnTimer = 10.0f;
    [SerializeField] private float portalsSpawnCircleTimer = 10.0f;

    private Coroutine _portalsSpawnCoroutine;
    
    private void OnEnable()
    {
        _portalsSpawnCoroutine = StartCoroutine(PortalsSpawnCoroutine());
    }

    private void OnDisable()
    {
        if (_portalsSpawnCoroutine != null)
        {
            StopCoroutine(_portalsSpawnCoroutine);
        }

        DisablePortals();
    }

    private IEnumerator PortalsSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(portalsSpawnTimer);
            EnablePortals();
            yield return new WaitForSeconds(portalsSpawnCircleTimer);
            DisablePortals();
        }
    }

    private void EnablePortals()
    {
        foreach (GameObject portal in portalsList)
        {
            if (portal != curPortal)
            {
                portal.SetActive(true);
            }
        }
    }

    private void DisablePortals()
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
