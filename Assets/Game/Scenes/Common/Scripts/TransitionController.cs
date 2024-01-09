using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private Image transitionImage;

    [SerializeField] private float duration = 1f;
    [SerializeField] private bool fadeOutOnAwake = true;

    private void Awake()
    {
        if (fadeOutOnAwake) FadeOut();
    }

    public void FadeIn(Action onComplete = null)
    {
        transitionImage.DOFade(1f, duration)
            .onComplete += () => onComplete?.Invoke();
    }

    public void FadeOut(Action onComplete = null)
    {
        transitionImage.DOFade(0f, duration)
            .onComplete += () => onComplete?.Invoke();
    }
}