using System;
using UnityEngine;

[Serializable]
public class SerializableDictionaryEntry<TKey, TValue>
{
    [SerializeField] private TKey key;
    [SerializeField] private TValue value;

    public TKey Key => key;
    public TValue Value => value;
}