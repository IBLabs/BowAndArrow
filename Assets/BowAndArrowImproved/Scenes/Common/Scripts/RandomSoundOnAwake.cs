using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RandomSoundOnAwake : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;

    void Start()
    {
        Vector3 position = transform.position;
        
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            position = mainCamera.transform.position;
        }
        
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], position);
    }
}
