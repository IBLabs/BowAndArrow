using System.Collections;
using System.Collections.Generic;
using Game.Scenes.Common.ScriptableObjects;
using UnityEngine;

public class BAAIArrowController : MonoBehaviour
{
    public ArrowConfiguration arrowConfig;

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

        GameObject newArrow = Instantiate(arrowConfig.prefab);
        newArrow.transform.position = arrowSpawnPoint.position;
        newArrow.transform.rotation = midPointVisual.transform.rotation;

        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.AddForce(midPointVisual.transform.forward * strength * arrowConfig.shootForce, ForceMode.Impulse);
    }
}
