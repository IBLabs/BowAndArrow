using System;
using UnityEngine;

public class UpdateScoreOnDeath : MonoBehaviour
{
    private Scoreboard _scoreboard;
    public void Start()
    {
        _scoreboard = FindObjectOfType<Scoreboard>();
        if (_scoreboard == null)
        {
            throw new Exception("Couldn't find Scoreboard");
        }
    }

    public void UpdateScoreboard()
    {
        _scoreboard.IncreaseScore();
    }
}