using UnityEngine;
using UnityEngine.Events;

public class GameRestartHandler : MonoBehaviour
{
    [SerializeField] private Ballon restartBalloonPrefab;
    
    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        Debug.Log(newState);
        if (newState == GameManager.State.Lose)
        {
            ShowShotToStartObject();
        }
    }   

    public void ShowShotToStartObject()
    {
        Ballon newBalloon = Instantiate(restartBalloonPrefab, transform.position, Quaternion.identity);
        newBalloon.isBalloonFrozen = true;
    }
}