using UnityEngine;
using UnityEngine.Events;

public interface BAAIIDeathable
{
    public UnityEvent<GameObject> onDeath { get; }
}