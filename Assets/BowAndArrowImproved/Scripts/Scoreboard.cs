using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    public int _score { get; private set; } = 0;

    public void IncreaseScore(int scoreValue)
    {
        _score += scoreValue;
        scoreText.text = "Score\n" + _score;
    }
    
    public void OnGameManagerStateChanged(GameManager.State newState)
    {
        if (newState == GameManager.State.Lose)
        {
            gameObject.SetActive(false);
        }
    }
}