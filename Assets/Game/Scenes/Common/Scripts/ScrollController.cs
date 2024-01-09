using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    public bool isShown => currentState == ScrollState.VISIBLE;
    public ScrollState currentState => _currentState;

    [SerializeField] private Transform scrollContainer;
    [SerializeField] private TextMeshProUGUI scrollText;
    [SerializeField] private Animator scrollAnimator;
    [SerializeField] private AnimationClip closeAnimationClip;
    [SerializeField] private ParticleSystem smokeParticleSystem;

    private ScrollState _currentState = ScrollState.HIDDEN;

    private static readonly int OpenScroll = Animator.StringToHash("openScroll");
    private static readonly int CloseScroll = Animator.StringToHash("closeScroll");

    private void Start()
    {
        Hide(false);
        StartFloating();
    }

    public void SetText(string text)
    {
        scrollText.text = text;
    }

    public void Show(bool animated)
    {
        _currentState = ScrollState.VISIBLE;

        StartCoroutine(ShowCoroutine());
    }

    public void Hide(bool animated)
    {
        _currentState = ScrollState.HIDDEN;

        StartCoroutine(HideCoroutine(animated));
    }

    private IEnumerator ShowCoroutine()
    {
        scrollContainer.gameObject.SetActive(true);
        
        smokeParticleSystem.Play();
        
        scrollAnimator.SetTrigger(OpenScroll);

        yield return null;
    }
    
    private IEnumerator HideCoroutine(bool animated)
    {
        if (animated)
        {
            scrollAnimator.SetTrigger(CloseScroll);
            yield return new WaitForSeconds(closeAnimationClip.length);
            smokeParticleSystem.Play();    
        }

        scrollContainer.gameObject.SetActive(false);
    }

    private void StartFloating()
    {
        scrollContainer.transform.DOMoveY(transform.position.y - .3f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public enum ScrollState
    {
        HIDDEN, VISIBLE
    }
}
