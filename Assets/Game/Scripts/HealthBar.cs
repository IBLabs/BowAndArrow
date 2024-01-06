using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthSquare;
    
    [SerializeField] private HitCountHittable hittable;
    
    private List<GameObject> _squares = new();

    private void Start()
    {
        SetupHealthBar();
    }

    public void OnTookHit()
    {
        if (_squares.Count <= 0) return;
        
        GameObject lastSquare = _squares[^1];

        _squares.Remove(lastSquare);
        Destroy(lastSquare);
    }

    private void SetupHealthBar()
    {
        for (int i = 0; i < hittable.health; i++)
        {
            GameObject newSquare = Instantiate(healthSquare, transform);
            _squares.Add(newSquare);
        }
    }
}
