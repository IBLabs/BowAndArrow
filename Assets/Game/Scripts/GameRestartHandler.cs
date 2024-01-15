using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameRestartHandler : MonoBehaviour
{
    [SerializeField] private TransitionController transitionController;
    [SerializeField] private ScrollController scrollController;
    
    [SerializeField] private Ballon restartBalloonPrefab;
    
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private float bgmVolume;
    [SerializeField] private float bgmFadeDuration = .5f;

    private void Start()
    {
        bgmAudioSource.DOFade(bgmVolume, bgmFadeDuration);
    }

    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.Lose)
        {
            ShowShotToStartObject();
        }
    }   

    public void ShowShotToStartObject()
    {
        Ballon newBalloon = Instantiate(restartBalloonPrefab, transform.position, Quaternion.identity);
        newBalloon.isBalloonFrozen = true;
        newBalloon.onDeath.AddListener(OnRestartBalloonDeath);
    }
    
    private void OnRestartBalloonDeath(GameObject balloon, int scoreValue, bool killedByPlayer)
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        scrollController.Hide(true);
        
        bgmAudioSource.DOFade(0f, bgmFadeDuration);

        yield return new WaitForSeconds(1f);

        transitionController.FadeIn(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}