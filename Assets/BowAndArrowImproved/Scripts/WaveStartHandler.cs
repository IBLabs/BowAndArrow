using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveStartHandler : MonoBehaviour
{
    [SerializeField] private List<AudioClip> popClips;

    public void OnWaveDidEnd()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(popClips[Random.Range(0, popClips.Count)], transform.position);
        GameManager.instance.StartWave();       
    }
}
