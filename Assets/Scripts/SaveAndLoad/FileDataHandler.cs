using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";
    private bool _encrypted = false;
    private string _codeWord = "thalesRaymond";

    public FileDataHandler(string dataDirPath, string dataFileName, bool encrypted)
    {
        this._dataDirPath = dataDirPath;
        this._dataFileName = dataFileName;
        this._encrypted = encrypted;
    }

    public void Save(GameData data)
    {
        string filePath = this._dataDirPath + "/" + this._dataFileName;

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            var dataToStore = this.EncryptString(JsonUtility.ToJson(data, true), this._codeWord);

            File.WriteAllText(filePath, dataToStore);
        }
        catch (Exception ex)
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
            string dataToLoad = this.DecryptString(File.ReadAllText(filePath), this._codeWord);

            return JsonUtility.FromJson<GameData>(dataToLoad);
        }
        catch (Exception ex)
        {
            Debug.Log($"Error while loading data from file: {filePath} - error: {ex}");
            return null;
        }
    }

    public void Delete()
    {
        string filePath = this._dataDirPath + "/" + this._dataFileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    public string EncryptString(string plainText, string key)
    {
        if(!this._encrypted)
        {
            return plainText;
        }

        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var outputBytes = new byte[plainTextBytes.Length];

        for (var i = 0; i < plainTextBytes.Length; i++)
        {
            outputBytes[i] = (byte)(plainTextBytes[i] ^ keyBytes[i % keyBytes.Length]);
        }

        return Convert.ToBase64String(outputBytes);
    }

    public string DecryptString(string cipherText, string key)
    {
        if(!this._encrypted)
        {
            return cipherText;
        }
        var cipherTextBytes = Convert.FromBase64String(cipherText);
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var outputBytes = new byte[cipherTextBytes.Length];

        for (var i = 0; i < cipherTextBytes.Length; i++)
        {
            outputBytes[i] = (byte)(cipherTextBytes[i] ^ keyBytes[i % keyBytes.Length]);
        }

        return Encoding.UTF8.GetString(outputBytes);
    }
}
