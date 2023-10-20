using UnityEngine;
using UnityEngine.Events;

public class WaveStartHandler : MonoBehaviour
{
    [SerializeField] private Ballon balloonPrefab;
    
    public UnityEvent startWave;

    public void ShowShotToStartObject()
    {
        Ballon newBalloon = Instantiate(balloonPrefab);
        newBalloon.isBalloonFrozen = true;
        newBalloon.onDeath.AddListener(OnShotToStartObjectPopped);
    }

    public void OnShotToStartObjectPopped(GameObject died)
    {
        startWave?.Invoke();
    }
}
