using System;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] private BAArrowController arrowPrefab;
    [SerializeField] private float shootForce = 100f;

    private Camera targetCamera;

    private void Start()
    {
        targetCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BAArrowController newArrow = Instantiate(
                arrowPrefab,
                targetCamera.transform.position + targetCamera.transform.forward.normalized * 1f,
                targetCamera.transform.rotation
            );

            newArrow.OnArrowReleased();
        }
    }
}