using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField] private List<SerializableDictionaryEntry<TKey, TValue>> entries;
    [SerializeField] private TValue defaultValue;

    public TValue GetByKey(TKey key)
    {
        foreach (var entry in entries)
        {
            if (EqualityComparer<TKey>.Default.Equals(entry.Key, key))
            {
                return entry.Value;
            }
        }

        return defaultValue;
    }
}