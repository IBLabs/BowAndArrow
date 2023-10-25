using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject playerContainer;
    [SerializeField] private List<GameObject> anchorsList;
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
        playerContainer.transform.position = teleportAnchor.transform.position;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            Teleport(other.gameObject);
        }
    }
}
