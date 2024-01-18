using UnityEngine;

public class BAAIArrowController : MonoBehaviour
{
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject midPointVisual;
    [SerializeField] private AudioSource bowReleaseAudioSource;
    [SerializeField] private ArrowConfigurationManager arrowConfigurationManager;

    public void PrepareArrow()
    {
        midPointVisual.SetActive(true);
    }
    public void ReleaseArrow(float strength)
    {
        bowReleaseAudioSource.Play();
        
        midPointVisual.SetActive(false);

        GameObject newArrow = Instantiate(arrowConfigurationManager.arrowConfig.prefab);
        newArrow.transform.position = arrowSpawnPoint.position;
        newArrow.transform.rotation = midPointVisual.transform.rotation;

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(midPointVisual.transform.forward * strength * arrowConfigurationManager.arrowConfig.shootForce, ForceMode.Impulse);
    }
}