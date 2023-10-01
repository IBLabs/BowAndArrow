using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAAIArrowController : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private GameObject midPointVisual;
    [SerializeField] private AudioSource bowReleaseAudioSource;

    [SerializeField] private float arrowMaxSpeed = 10f;
    
    public void PrepareArrow()
    {
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        bowReleaseAudioSource.Play();
        
        midPointVisual.SetActive(false);

        GameObject newArrow = Instantiate(arrowPrefab);
        newArrow.transform.position = arrowSpawnPoint.position;
        newArrow.transform.rotation = midPointVisual.transform.rotation;

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(midPointVisual.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);
    }
}
