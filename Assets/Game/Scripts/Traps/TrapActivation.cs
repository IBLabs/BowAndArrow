using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TrapActivation : MonoBehaviour
{
    [SerializeField] private float activationTime = 3f;
    [SerializeField] private List<SimpleTrap> myTraps;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private bool areTrapsActive;

    private void OnTriggerEnter(Collider other)
    {
        if (!hitMask.Contains(other.attachedRigidbody.gameObject.layer) || areTrapsActive) return;

        foreach (SimpleTrap trap in myTraps)
        {
            trap.ToggleTrapActivation(true);
        }

        areTrapsActive = true;
        
        StartCoroutine(CountdownAndDeactivateTrap());
    }

    private IEnumerator CountdownAndDeactivateTrap()
    {
        yield return new WaitForSeconds(activationTime);

        foreach (SimpleTrap trap in myTraps)
        {
            trap.ToggleTrapActivation(false);
        }

        areTrapsActive = false;
    }
}