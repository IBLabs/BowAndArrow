using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthSquare;
    
    [SerializeField] private HitCountHittable hittable;
    [SerializeField] private int health;
    
    private List<GameObject> squares = new();

    private void Start()
    {
        SetupHealthBar();
    }

    public void OnTookHit()
    {
        if (squares.Count <= 0) return;
        
        GameObject lastSquare = squares[^1];

        squares.Remove(lastSquare);
        Destroy(lastSquare);
    }

    public void SetupHealthBar()
    {
        hittable.SetCount(health);
        int numOfExistSquares = squares.Count;
        
        for (int i = numOfExistSquares; i < health; i++)
        {
            GameObject newSquare = Instantiate(healthSquare, transform);
            squares.Add(newSquare);
        }
    }
}
