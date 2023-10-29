using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
     
    [SerializeField] private TMP_Text scoreText;
    private int _score = 0;
    
    public void IncreaseScore(GameObject enemy, bool isIncreaseValid)
    {
        if (!isIncreaseValid) return;
        
        if (enemy.TryGetComponent(out BAAIEnemy hitEnemy))
        {
            _score += hitEnemy.scoreValue;
            scoreText.text = "Score\n" + _score;
        }
    }
    
    public void ResetScore()
    {
        _score  = 0;
        scoreText.text = "Score\n" + _score;
    }
}
