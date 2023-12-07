using System;
using UnityEngine;

public class SimpleTrap : MonoBehaviour
{
    [SerializeField] private bool isActive;

    [SerializeField] private Animator animator;
    private const string ANIMATION_ACTIVATION_TRIGGER = "ActivateTrap";
    private const string ANIMATION_DEACTIVATION_TRIGGER = "DeactivateTrap";

    [SerializeField] private AudioClip trapClip;

    public void ToggleTrapActivation(bool didActivated)
    {
        isActive = didActivated;

        if (isActive)
        {
            animator.SetTrigger(ANIMATION_ACTIVATION_TRIGGER);
            AudioSource.PlayClipAtPoint(trapClip, transform.position);
        }
        else
        {
            animator.SetTrigger(ANIMATION_DEACTIVATION_TRIGGER);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (other.attachedRigidbody.TryGetComponent(out BAAIEnemy hitEnemy)) hitEnemy.Die(true);
    }
}