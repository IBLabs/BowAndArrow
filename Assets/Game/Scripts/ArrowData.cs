using System;
using Game.Scenes.Common.ScriptableObjects;
using UnityEngine;

[Serializable]
public class ArrowData
{
    [SerializeField] private ArrowType type;
    [SerializeField] private ArrowConfiguration configuration;

    public ArrowType Type => type;
    public ArrowConfiguration Configuration => configuration;
}