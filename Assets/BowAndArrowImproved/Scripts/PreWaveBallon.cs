using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreWaveBallon : MonoBehaviour
{
    [SerializeField] private List<AudioClip> popClips;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(popClips[Random.Range(0, popClips.Count)], transform.position);
        GameManager.Instance.StartWave();
    }
    
    //Mechanic consideration - should we use onTrigger and let the arrow pass the ballon or should we use collision
    private void OnCollisionEnter(Collision other)
    {
        Hide();        
    }
}
