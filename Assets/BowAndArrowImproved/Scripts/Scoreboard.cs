using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
     
    public TMP_Text scoreText;
    private int _score = 0;

    public void IncreaseScore()
    {
        _score ++;
        scoreText.text = "Score\n" + _score;
    }
}
