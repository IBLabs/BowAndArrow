using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationSelector : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private List<string> animationNames;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator.Play(animationNames[Random.Range(0, animationNames.Count)]);
    }
}
