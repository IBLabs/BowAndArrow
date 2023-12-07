using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandBowSpawner : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable bowInteractable;
    [SerializeField] private XRGrabInteractable stringInteractable;

    [SerializeField] private AudioClip onActionSoundEffect;

    [SerializeField] private ActionBasedController leftHandController;
    [SerializeField] private ActionBasedController rightHandController;

    [SerializeField] private XRBaseInteractor leftHandInteractor;
    [SerializeField] private XRBaseInteractor rightHandInteractor;

    private AudioSource _audioSource;
    private bool _isBowGrabbed = false;
    private bool _isBowLeftHand = false;
    private bool _isStringGrabbed = false;

    void Start()
    {
        leftHandController.activateAction.action.started += OnLeftActivateStarted;
        leftHandController.activateAction.action.canceled += OnLeftActivateCancelled;

        rightHandController.activateAction.action.started += OnRightActivateStarted;
        rightHandController.activateAction.action.canceled += OnRightActivateCancelled;

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnLeftActivateStarted(InputAction.CallbackContext context)
    {
        GrabBowOrString(leftHandInteractor, leftHandController, true);
    }

    private void OnRightActivateStarted(InputAction.CallbackContext context)
    {
        GrabBowOrString(rightHandInteractor, rightHandController, false);
    }

    private void GrabBowOrString(XRBaseInteractor interactor, XRBaseController controller, bool isLeft)
    {
        if (!_isBowGrabbed)
        {
            GrabBow(interactor, controller);
            _isBowLeftHand = isLeft;
        }
        else
        {
            GrabString(interactor);
        }
    }

    private void OnLeftActivateCancelled(InputAction.CallbackContext context)
    {
        ReleaseGrabbedItem(leftHandInteractor);

        if (_isBowGrabbed && _isBowLeftHand)
        {
            ReleaseGrabbedItem(rightHandInteractor);
        }
    }

    private void OnRightActivateCancelled(InputAction.CallbackContext context)
    {
        ReleaseGrabbedItem(rightHandInteractor);

        if (_isBowGrabbed && !_isBowLeftHand)
        {
            ReleaseGrabbedItem(leftHandInteractor);
        }
    }

    private void GrabBow(XRBaseInteractor interactor, XRBaseController controller)
    {
        interactor.StartManualInteraction(bowInteractable as IXRSelectInteractable);

        _isBowGrabbed = true;

        if (onActionSoundEffect != null)
        {
            AudioSource.PlayClipAtPoint(onActionSoundEffect, transform.position);
        }

        controller.SendHapticImpulse(0.1f, 0.1f);
    }

    private void GrabString(XRBaseInteractor interactor)
    {
        if (interactor.isPerformingManualInteraction)
        {
            Debug.Log("[WARNING]: interactor is already performing manual interaction!");
            return;
        }

        interactor.StartManualInteraction(stringInteractable as IXRSelectInteractable);

        _isStringGrabbed = true;
    }

    private void ReleaseGrabbedItem(XRBaseInteractor interactor)
    {
        if (!interactor.isPerformingManualInteraction)
        {
            Debug.Log("[WARNING]: interactor is not performing manual interaction but was asked to release!");
            return;
        }

        if (_isBowGrabbed && interactor == bowInteractable.GetOldestInteractorSelecting())
        {
            _isBowGrabbed = false;
        }

        interactor.EndManualInteraction();
    }
}