using System;
using System.Collections.Generic;
using Game.Scenes.Common.ScriptableObjects;
using UnityEngine;

[Serializable]
public class ArrowDataDictionary
{
    [SerializeField] private List<ArrowData> arrowConfigurationList;
    [SerializeField] private ArrowConfiguration defaultConfiguration;

    public ArrowConfiguration GetConfigurationByArrowType(ArrowType arrowTypeKey)
    {
        foreach (ArrowData arrowDataSet in arrowConfigurationList)
        {
            if (arrowDataSet.Type == arrowTypeKey)
            {
                return arrowDataSet.Configuration;
            }
        }

        return defaultConfiguration;
    }
}