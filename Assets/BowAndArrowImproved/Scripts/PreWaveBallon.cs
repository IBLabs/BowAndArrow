using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PreWaveBallon : MonoBehaviour
{
    public UnityEvent onPop;
    [SerializeField] private List<AudioClip> popClips;
    
    private void OnCollisionEnter(Collision other)
    {
        //add to onPop Extra functionality
        onPop?.Invoke();
        gameObject.SetActive(false);
        
        AudioSource.PlayClipAtPoint(popClips[Random.Range(0, popClips.Count)], transform.position);
    }
}