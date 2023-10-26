using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
     
    [SerializeField] private TMP_Text scoreText;
    private int _score = 0;
    
    public void IncreaseScore(int increaseValue)
    {
        _score += increaseValue;
        scoreText.text = "Score\n" + _score;
    }
    
    public void ResetScore()
    {
        _score  = 0;
        scoreText.text = "Score\n" + _score;
    }
}
