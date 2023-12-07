using System;
using UnityEngine;

public class BAAIDebugArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    [SerializeField] private float strength;
    
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SpawnArrow();
        }
    }

    private void SpawnArrow()
    {
        GameObject newArrow = Instantiate(
            arrowPrefab,
            mainCamera.transform.position,
            Quaternion.LookRotation(mainCamera.transform.forward)
        );

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(mainCamera.transform.forward * strength, ForceMode.Impulse);
    }
}