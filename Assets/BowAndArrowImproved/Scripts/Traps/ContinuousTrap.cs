using System;
using UnityEngine;

public class ContinuousTrap : MonoBehaviour
{
    [SerializeField] private bool isActive;
    
    [SerializeField] private Animator animator;
    [SerializeField] private String myAnimationActivateTrigger;
    [SerializeField] private String myAnimationDeactivateTrigger;
    
    [SerializeField] private AudioClip trapClip;

    public void ToggleTrapActivation(bool didActivated)
    {
        isActive = didActivated;

        if (isActive)
        {
            animator.SetTrigger(myAnimationActivateTrigger);
            AudioSource.PlayClipAtPoint(trapClip, transform.position);
        }
        else
        {
            animator.SetTrigger(myAnimationDeactivateTrigger);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (other.attachedRigidbody.TryGetComponent(out BAAIEnemy hitEnemy)) hitEnemy.Die(true);
    }
}