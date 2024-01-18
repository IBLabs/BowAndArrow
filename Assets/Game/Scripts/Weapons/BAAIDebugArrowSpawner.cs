using UnityEngine;

public class BAAIDebugArrowSpawner : MonoBehaviour
{
    [SerializeField] private BAAIArrowController bowController;
    [SerializeField] private ArrowConfigurationManager arrowConfigurationManager;

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
            arrowConfigurationManager.arrowConfig.prefab,
            mainCamera.transform.position,
            Quaternion.LookRotation(mainCamera.transform.forward)
        );

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(mainCamera.transform.forward * arrowConfigurationManager.arrowConfig.shootForce, ForceMode.Impulse);
    }
}