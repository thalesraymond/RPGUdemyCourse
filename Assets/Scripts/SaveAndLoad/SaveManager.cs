using System.Collections.Generic;
using System.Linq;
using Skills;
using UnityEngine;

namespace SaveAndLoad
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }

        private IList<ISaveManager> _saveManagers;
        private FileDataHandler _fileDataHandler;

        [SerializeField] private string _fileName;
        [SerializeField] private bool _encryptData;

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

            this._fileDataHandler = new FileDataHandler(Application.persistentDataPath, this._fileName, this._encryptData);

            this.LoadGame();
        }

        [ContextMenu("Delete Save Data")]
        public void DeleteSaveData()
        {
            this._fileDataHandler = new FileDataHandler(Application.persistentDataPath, this._fileName, this._encryptData);
            this._fileDataHandler.Delete(); 
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
            
            SkillManager.Instance.ValidateUnlocks();
        }

        public void SaveGame()
        {
            foreach (var saveManager in this._saveManagers)
            {
                saveManager.SaveData(ref this._gameData);
            }

            this._fileDataHandler.Save(this._gameData);
        }

        public bool HasSaveData()
        {            
            return this._fileDataHandler.HasSaveData();
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
}
