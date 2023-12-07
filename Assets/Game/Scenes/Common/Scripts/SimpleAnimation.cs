using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public enum AnimationType
{
    RotationLoop,
    TranslateLoop
}

public class SimpleAnimation : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private AnimationType animationType;

    void Start()
    {
        switch (animationType)
        {
            case AnimationType.RotationLoop:
                StartRotationLoop();
                break;
            case AnimationType.TranslateLoop:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void StartRotationLoop()
    {
        Vector3 currentLocalRotation = transform.localRotation.eulerAngles;
        transform.DOLocalRotate(currentLocalRotation + new Vector3(0f, 360f, 0f), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
