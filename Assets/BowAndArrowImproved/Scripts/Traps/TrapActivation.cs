using System;
using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TrapActivation : MonoBehaviour
{
    [SerializeField] private float activationTime = 3f;
    [SerializeField] private ContinuousTrap myTrap;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private bool isTrapActive;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hitMask.Contains(other.attachedRigidbody.gameObject.layer) || isTrapActive) return;
        
        isTrapActive = true;
        myTrap.ToggleTrapActivation(true);

        StartCoroutine(CountdownAndDeactivateTrap());
    }
    
    private IEnumerator CountdownAndDeactivateTrap()
    {
        yield return new WaitForSeconds(activationTime);

        myTrap.ToggleTrapActivation(false);
        isTrapActive = false;
    }
}