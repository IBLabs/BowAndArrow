using System;
using UnityEngine;

public class UpdateScoreOnDeath : MonoBehaviour
{
    public Scoreboard Scoreboard;
    public void Start()
    {
        Scoreboard = FindObjectOfType<Scoreboard>();
        if (Scoreboard == null)
        {
            throw new Exception("Couldn't find Scoreboard");
        }
    }

    public void UpdateScoreboard()
    {
        Scoreboard.IncreaseScore(1);
    }
}