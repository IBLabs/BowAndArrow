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
    [SerializeField] private AudioClip hitSoundEffect;

    private void OnCollisionEnter(Collision other)
    {
        if (!hitMask.Contains(other.gameObject.layer)) return;
        AudioSource.PlayClipAtPoint(hitSoundEffect, Camera.main.transform.position);

        if (areTrapsActive) return;
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