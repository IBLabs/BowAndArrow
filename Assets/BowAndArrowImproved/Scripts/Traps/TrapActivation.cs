using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;

public class TrapActivation : MonoBehaviour
{
    [SerializeField] private float activationTime = 3f;
    [SerializeField] private List<ContinuousTrap> myTraps;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private bool areTrapsActive;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hitMask.Contains(other.attachedRigidbody.gameObject.layer) || areTrapsActive) return;
        
        areTrapsActive = true;
        foreach (ContinuousTrap trap in myTraps)
        {
            trap.ToggleTrapActivation(true); 
        }
        
        StartCoroutine(CountdownAndDeactivateTrap());
    }
    
    private IEnumerator CountdownAndDeactivateTrap()
    {
        yield return new WaitForSeconds(activationTime);

        foreach (ContinuousTrap trap in myTraps)
        {
            trap.ToggleTrapActivation(false);
        }
        
        areTrapsActive = false;
    }
}