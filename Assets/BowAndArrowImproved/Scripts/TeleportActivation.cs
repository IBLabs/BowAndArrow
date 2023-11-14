using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportActivation : MonoBehaviour
{
    public InputActionProperty enableTeleportationAction;

    public UnityEvent teleportationEnabled;
    public UnityEvent teleportationDisabled;

    private void Start()
    {
        enableTeleportationAction.action.performed += OnEnableTeleportAction;
        enableTeleportationAction.action.canceled += OnDisableTeleportAction;
    }

    private void OnEnableTeleportAction(InputAction.CallbackContext context)
    {
        teleportationEnabled?.Invoke();
    }

    private void OnDisableTeleportAction(InputAction.CallbackContext context)
    {
        teleportationDisabled?.Invoke();
    }
}