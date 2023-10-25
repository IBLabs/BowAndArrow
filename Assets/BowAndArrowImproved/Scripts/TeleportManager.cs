using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject playerContainer;
    [SerializeField] private List<GameObject> portalsList;
    [SerializeField] private GameObject curPortal;

    [SerializeField] private float offTime = 15.0f;
    [SerializeField] private float onTime = 10.0f;

    private Coroutine _togglePortalsCoroutine;
    
    private void OnEnable()
    {
        _togglePortalsCoroutine = StartCoroutine(TogglePortalsCoroutine());
    }

    private void OnDisable()
    {
        if (_togglePortalsCoroutine != null)
        {
            StopCoroutine(_togglePortalsCoroutine);
        }

        DisablePortals();
    }

    private IEnumerator TogglePortalsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(offTime);
            EnablePortals();
            yield return new WaitForSeconds(onTime);
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
