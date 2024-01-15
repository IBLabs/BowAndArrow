using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTargetController : MonoBehaviour
{
    [Header("Transitions")]
    [SerializeField] private TransitionController transitionController;
    
    [Header("Background Music")]
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private float bgmFadeDuration = .4f;
    [SerializeField] private float bgmVolume = .2f;
    
    private const float Delay = 3f;

    private void Start()
    {
        bgmAudioSource.DOFade(bgmVolume, bgmFadeDuration);
    }

    public void OnTargetCollided(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            StartCoroutine(StartGameAfterDelay());
        }
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSeconds(Delay);

        bgmAudioSource.DOFade(0f, bgmFadeDuration);
        transitionController.FadeIn(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
