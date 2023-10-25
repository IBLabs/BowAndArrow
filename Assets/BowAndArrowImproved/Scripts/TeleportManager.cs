using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject playerContainer;
    [SerializeField] private List<GameObject> anchorsList;
    [SerializeField] private List<Transform> towersCoordinatesList;
    [SerializeField] private float anchorsSpawnTimer = 10.0f;
    [SerializeField] private float anchorsSpawnCircleTimer = 10.0f;

    private Coroutine _teleportCoroutine;
    
    private void OnEnable()
    {
        _teleportCoroutine = StartCoroutine(AnchorSpawnCoroutine());
    }

    private void OnDisable()
    {
        if (_teleportCoroutine != null)
        {
            StopCoroutine(_teleportCoroutine);
        }

        DisableAnchors();
    }

    private IEnumerator AnchorSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(anchorsSpawnTimer);
            EnableAnchors();
            yield return new WaitForSeconds(anchorsSpawnCircleTimer);
            DisableAnchors();
        }
    }

    private void EnableAnchors()
    {
        foreach (GameObject anchor in anchorsList)
        {
            anchor.SetActive(true);
        }
    }

    private void DisableAnchors()
    {
        foreach (GameObject anchor in anchorsList)
        {
            anchor.SetActive(false);
        }
    }
    
    public void Teleport(GameObject teleportAnchor)
    {
        Transform coordinates = teleportAnchor.transform.Find("Coordinates");
        if (coordinates == null) return;
 
        playerContainer.transform.position = coordinates.position;
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
