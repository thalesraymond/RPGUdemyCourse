using System;
using System.Collections.Generic;
using System.Linq;
using SaveAndLoad;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameManager : MonoBehaviour, ISaveManager
    {
        public static GameManager Instance { get; private set; }
        
        [SerializeField] private Checkpoint[] checkpoints;

        private GameData _data;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance.gameObject);
            }
        }

        private void Start()
        {
            this.checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
        }
        
        public void RestartScene()
        {
            SaveManager.Instance.SaveGame();
            
            var scene = SceneManager.GetActiveScene();
            
            SceneManager.LoadScene(scene.name);
        }

        public void LoadData(GameData data)
        {
            this._data = data;
            
            Invoke(nameof(LoadCheckpointData), 0.1f); // We need this delay, otherwise the checkpoint array is null and the game crashes
        }

        private void LoadCheckpointData()
        {
            foreach (var pair in this._data.Checkpoints)
            {
                var checkpoint = this.checkpoints.First(checkpoint => checkpoint.checkpointId == pair.Key);

                if (pair.Value) checkpoint.ActivateCheckpoint();
            }
            
            var respawnPoint = this.checkpoints.FirstOrDefault(checkpoint => checkpoint.checkpointId == this._data.ClosestCheckpointId);

            if (respawnPoint == null) return;
             
            PlayerManager.Instance.Player.transform.position = new Vector3(respawnPoint.transform.position.x + Random.Range(-3, 3), respawnPoint.transform.position.y, 0);
        }

        public void SaveData(ref GameData data)
        {
            data.Checkpoints.Clear();
            
            foreach (var checkpoint in this.checkpoints)
            {
                data.Checkpoints.Add(checkpoint.checkpointId, checkpoint.IsActive);
                data.ClosestCheckpointId = this.FindClosestCheckpoint().checkpointId;
            }
        }

        private Checkpoint FindClosestCheckpoint()
        {
            var playerPosition = PlayerManager.Instance.Player.transform.position;

            var closestCheckpoint = this.checkpoints
                .Where(checkpoint => checkpoint.IsActive)
                .OrderBy(checkpoint => Vector2.Distance(checkpoint.transform.position, playerPosition))
                .First();

            return closestCheckpoint;
        }
    }
}
