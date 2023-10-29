using System;
using UnityEngine;

public class UpdateScoreOnDeath : MonoBehaviour
{
    public Scoreboard scoreboard;
    [SerializeField] private int scoreToIncrease;

    public bool isUpdateValid = false;
    
    public void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        if (scoreboard == null)
        {
            throw new Exception("Couldn't find Scoreboard");
        }
    }

    public void UpdateScoreboard()
    {
        if (!isUpdateValid) return;
        
        scoreboard.IncreaseScore(scoreToIncrease);
    }
}