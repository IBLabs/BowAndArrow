using System;
using UnityEngine;

public class JsonDataLoader : IDataLoader
{
    public T LoadData<T>(string asset)
    {
        try
        {
            T data = JsonUtility.FromJson<T>(asset);
            return data;
        }
        catch (Exception e)
        {
            throw new Exception("Error loading Json data: " + e.Message);
        }
    }
}