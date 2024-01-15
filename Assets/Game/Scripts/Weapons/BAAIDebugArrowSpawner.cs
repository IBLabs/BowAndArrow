using System;
using Game.Scenes.Common.ScriptableObjects;
using UnityEngine;

public class BAAIDebugArrowSpawner : MonoBehaviour
{
    [SerializeField] private BAAIArrowController bowController;

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
            bowController.arrowConfig.prefab,
            mainCamera.transform.position,
            Quaternion.LookRotation(mainCamera.transform.forward)
        );

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(mainCamera.transform.forward * bowController.arrowConfig.shootForce, ForceMode.Impulse);
    }
}