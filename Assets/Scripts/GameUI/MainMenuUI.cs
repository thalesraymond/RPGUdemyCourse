using System.Collections;
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
        [SerializeField] private FadeScreenUI fadeScreenUI;

        private void Start()
        {
            continueButton.gameObject.SetActive(SaveManager.Instance.HasSaveData());
        }
        public void ContinueGame()
        {
            StartCoroutine(LoadSceneWithFadeEffect(1.5f));
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

        private IEnumerator LoadSceneWithFadeEffect(float delay)
        {   
            fadeScreenUI.FadeOut();
            
            yield return new WaitForSeconds(delay);
            
            SceneManager.LoadScene(sceneName);
        }
    }
}