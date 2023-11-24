using System;
using System.IO;
using UnityEngine;

public class JsonDataHandler : IDataHandler
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
    
    public void SaveData<T>(string filePath, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        try
        {
            File.WriteAllText(filePath, jsonData);
        }
        catch (Exception e)
        {
            throw new Exception("Error save to Json file: " + e.Message);
        }
    }
}