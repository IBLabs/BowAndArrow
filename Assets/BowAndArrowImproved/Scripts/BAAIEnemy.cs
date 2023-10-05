using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BAAIEnemy : MonoBehaviour,BAAIIDeathable
{
    [SerializeField] private UnityEvent<GameObject> _onDeath;
    public UnityEvent<GameObject> onDeath => _onDeath;

    [SerializeField] private List<AudioClip> breakClips;
    [SerializeField] private List<AudioClip> warCryClips;

    private void OnEnable()
    {
        PlayWarCry();
    }

    private void PlayWarCry()
    {
        AudioSource.PlayClipAtPoint(warCryClips[Random.Range(0, warCryClips.Count)], transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDeath.Invoke(gameObject);
        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(breakClips[Random.Range(0, breakClips.Count)], transform.position);
    }
}