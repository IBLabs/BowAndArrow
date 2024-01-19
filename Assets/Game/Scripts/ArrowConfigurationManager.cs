using System;
using System.Collections.Generic;
using Game.Scenes.Common.ScriptableObjects;
using UnityEngine;

public class ArrowConfigurationManager : MonoBehaviour
{
    public ArrowConfiguration arrowConfig;
    [SerializeField] private ArrowConfiguration defaultConfiguration;
    [SerializeField] List<ArrowDataSet> arrowsConfigurations;

    public void OnBalloonDeath(GameObject balloonGameObject, int score, bool killedByPlayer)
    {
        if (!killedByPlayer || !balloonGameObject.TryGetComponent<Ballon>(out var balloon)) return;
        SwitchArrow(balloon.arrowType);
    }

    public void SwitchArrow(ArrowType arrowType)
    {
        arrowConfig = GetArrowConfigurationByType(arrowType);
    }

    private ArrowConfiguration GetArrowConfigurationByType(ArrowType arrowType)
    {
        foreach (ArrowDataSet arrowDataSet in arrowsConfigurations)
        {
            if (arrowDataSet.Type == arrowType) return arrowDataSet.Configuration;
        }

        return defaultConfiguration;
    }
}