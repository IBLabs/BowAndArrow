using UnityEngine;
using UnityEngine.Events;

public class WaveStartHandler : MonoBehaviour
{
    [SerializeField] private Ballon startBalloonPrefab;
    [SerializeField] private Ballon restartBalloonPrefab;
    
    public UnityEvent startWave;
    public UnityEvent resetGame;

    private bool _isStartBalloon;

    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.PreWave)
        {
            _isStartBalloon = true;
            ShowShotToStartObject(startBalloonPrefab);
        } 
        else if (newState == GameManager.State.Lose)
        {
            _isStartBalloon = false;
            ShowShotToStartObject(restartBalloonPrefab);
        }
    }   

    public void ShowShotToStartObject(Ballon shotToStartPrefab)
    {
        Ballon newBalloon = Instantiate(shotToStartPrefab, transform.position, Quaternion.identity);
        newBalloon.isBalloonFrozen = true;
        newBalloon.onDeath.AddListener(OnShotToStartObjectPopped);
    }

    public void OnShotToStartObjectPopped(GameObject died)
    {
        if (_isStartBalloon)
        {
            startWave?.Invoke();
        }
        else
        {
            resetGame?.Invoke();
        }
    }
    
}
