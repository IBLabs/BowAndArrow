using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class WaveStartHandler : MonoBehaviour
{
    [SerializeField] private Ballon balloonPrefab;
    
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
        Ballon newBalloon = Instantiate(balloonPrefab, transform.position, Quaternion.identity);
        newBalloon.isBalloonFrozen = true;
        newBalloon.onDeath.AddListener(OnShotToStartObjectPopped);
    }

    public void OnShotToStartObjectPopped(GameObject died)
    {
        startWave?.Invoke();
    }
}
