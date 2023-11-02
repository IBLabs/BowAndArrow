using System;
using UnityEngine;

public class ContinuousTrap : MonoBehaviour
{
    [SerializeField] private bool isActive;
    [SerializeField] private Animator animator;
    [SerializeField] private String activateTrigger;
    [SerializeField] private String deactivateTrigger;
    
    public void ToggleTrapActivation(bool didActivated)
    {
        isActive = didActivated;

        animator.SetTrigger(isActive ? activateTrigger : deactivateTrigger);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (other.attachedRigidbody.TryGetComponent(out BAAIEnemy hitEnemy)) hitEnemy.Die(true);
    }
}