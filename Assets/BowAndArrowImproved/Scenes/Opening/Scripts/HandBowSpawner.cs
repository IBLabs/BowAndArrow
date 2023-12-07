using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class HandBowSpawner : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable _interactable;

    [SerializeField] private AudioClip onActionSoundEffect;
    
    private ActionBasedController _controller;
    private XRBaseInteractor _interactor;
    private AudioSource _audioSource;
    
    void Start()
    {
        _controller = GetComponent<ActionBasedController>();
        _controller.activateAction.action.started += OnActivateStarted;
        _controller.activateAction.action.canceled += OnActivateCancelled;

        _interactor = GetComponent<XRBaseInteractor>();

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnActivateStarted(InputAction.CallbackContext context)
    {
        _interactor.StartManualInteraction(_interactable as IXRSelectInteractable);

        if (_audioSource != null && onActionSoundEffect != null)
        {
            _audioSource.PlayOneShot(onActionSoundEffect);
        }

        _controller.SendHapticImpulse(0.1f, 0.1f);
    }

    private void OnActivateCancelled(InputAction.CallbackContext context)
    {
        _interactor.EndManualInteraction();
    }
}
