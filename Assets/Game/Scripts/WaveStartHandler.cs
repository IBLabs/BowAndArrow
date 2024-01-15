using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaveStartHandler : MonoBehaviour
{
    [SerializeField] private BAAIWaveSpawner waveSpawner;
    [SerializeField] private ScrollController scrollController;
    
    [SerializeField] private Ballon startBalloonPrefab;
    
    public UnityEvent startWave;
    
    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.PreWave)
        {
            ShowShotToStartObject();
        } 
    }   

    public void ShowShotToStartObject()
    {
        Ballon newBalloon = Instantiate(startBalloonPrefab, transform.position, Quaternion.identity);
        newBalloon.isBalloonFrozen = true;
        newBalloon.onDeath.AddListener(OnShotToStartObjectPopped);
    }

    public void OnShotToStartObjectPopped(GameObject died, int scoreValue, bool killedByPlayer)
    {
        StartCoroutine(ShowWaveTitleCoroutine());
        startWave?.Invoke();
    }

    private IEnumerator ShowWaveTitleCoroutine()
    {
        yield return new WaitForSeconds(1f);

        scrollController.SetText("Wave " + waveSpawner.currentWave);
        scrollController.Show(true);
        
        yield return new WaitForSeconds(2f);
        
        scrollController.Hide(true);
    }
}
