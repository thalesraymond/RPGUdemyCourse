using SaveAndLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameUI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private string sceneName = "MainScene";
        [SerializeField] private Button continueButton;

        private void Start()
        {
            continueButton.gameObject.SetActive(SaveManager.Instance.HasSaveData());
        }
        public void ContinueGame()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void NewGame()
        {   
            SaveManager.Instance.DeleteSaveData();
            
            this.ContinueGame();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}