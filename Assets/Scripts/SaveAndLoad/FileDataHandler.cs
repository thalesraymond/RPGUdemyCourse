using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this._dataDirPath = dataDirPath;
        this._dataFileName = dataFileName;
    }

    public void Save(GameData data)
    {
        string filePath = this._dataDirPath + "/" + this._dataFileName;
        
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            var dataToStore = JsonUtility.ToJson(data, true);

            File.WriteAllText(filePath, dataToStore);
        }
        catch(Exception ex)
        {
            Debug.Log($"Error while saving data to file: {filePath} - error: {ex}");
        }
    }

    public GameData Load()
    {
        string filePath = this._dataDirPath + "/" + this._dataFileName;

        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            string dataToLoad = File.ReadAllText(filePath);

            return JsonUtility.FromJson<GameData>(dataToLoad);
        }
        catch(Exception ex)
        {
            Debug.Log($"Error while loading data from file: {filePath} - error: {ex}");
            return null;
        }
    }
}
