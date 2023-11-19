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
    
    public void SaveData<T>(string fileName, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            File.WriteAllText(path, jsonData);
        }
        catch (Exception e)
        {
            throw new Exception("Error save to Json file: " + e.Message);
        }
    }
}