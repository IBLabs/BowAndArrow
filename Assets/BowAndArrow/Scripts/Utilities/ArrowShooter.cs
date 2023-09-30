using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] private BAArrowController arrowPrefab;
    [SerializeField] private float shootForce = 100f;

    [SerializeField] private InputActionProperty someActionProperty;

    private Camera targetCamera;

    private bool isTriggerHold = false;
    private bool didTriggerShoot = false;

    private void Start()
    {
        targetCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TriggerShoot();
        }
        
        float triggerValue = someActionProperty.action.ReadValue<float>();
        if (triggerValue > 0.5f)
        {
            isTriggerHold = true;
        }
        else
        {
            isTriggerHold = false;
            didTriggerShoot = false;
        }

        if (isTriggerHold && !didTriggerShoot)
        {
            didTriggerShoot = true;
            TriggerShoot();
        }
    }

    private void TriggerShoot()
    {
        BAArrowController newArrow = Instantiate(
            arrowPrefab,
            targetCamera.transform.position + targetCamera.transform.forward.normalized * 1f,
            targetCamera.transform.rotation
        );

        newArrow.OnArrowReleased();
    }
}