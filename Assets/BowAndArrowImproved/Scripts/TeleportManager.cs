using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject playerContainer;
    [SerializeField] private List<TeleportAnchor> anchorsList;

    private Coroutine _teleportCoroutine;
    
    private void OnEnable()
    {
        _teleportCoroutine = StartCoroutine(TeleportCoroutine());
    }

    private void OnDisable()
    {
        if (_teleportCoroutine != null)
        {
            StopCoroutine(_teleportCoroutine);
        }
    }

    private IEnumerator TeleportCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);

            EnableRandomAnchor();
            yield return new WaitForSeconds(10.0f);

            DisableEnabledAnchor();
        }
    }

    private void EnableRandomAnchor()
    {
        foreach (TeleportAnchor anchor in anchorsList)
        {
            anchor.EnableAnchor();
        }
    }

    private void DisableEnabledAnchor()
    {
        foreach (TeleportAnchor anchor in anchorsList)
        {
            anchor.DisableAnchor();
        }
    }
    
    public void Teleport(TeleportAnchor teleportAnchor)
    {
        playerContainer.transform.position = teleportAnchor.transform.position;

        //@TODO - add spawning sound and visual effects.
    }
}

