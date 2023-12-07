using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
class SoundOnTriggerEnterConfiguration
{
    public LayerMask layerMask;
    public AudioClip audioClip;
}

public class SoundOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private List<SoundOnTriggerEnterConfiguration> configurations;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SoundOnTriggerEnterConfiguration config = configurations.Find(
            (config) => config.layerMask.Contains(other.gameObject.layer)
        );

        if (config != null)
        {
            _audioSource.PlayOneShot(config.audioClip);
        }
    }
}