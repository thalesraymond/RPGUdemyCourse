using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private IList<ISaveManager> _saveManagers;
    private FileDataHandler _fileDataHandler;

    [SerializeField] private string _fileName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private GameData _gameData;

    public void Start()
    {
        this._saveManagers = this.FindAllSaveManagers();

        this._fileDataHandler = new FileDataHandler(Application.persistentDataPath, this._fileName);

        this.LoadGame();
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        this._gameData = this._fileDataHandler.Load();

        if(this._gameData == null)
        {
            this.NewGame();

            return;
        }

        foreach (var saveManager in this._saveManagers)
        {
            saveManager.LoadData(this._gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var saveManager in this._saveManagers)
        {
            saveManager.SaveData(ref this._gameData);
        }

        this._fileDataHandler.Save(this._gameData);
    }

    private void OnApplicationQuit()
    {
        this.SaveGame();
    }

    private IList<ISaveManager> FindAllSaveManagers()
    {
        var saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return saveManagers.ToList();
    }
}
