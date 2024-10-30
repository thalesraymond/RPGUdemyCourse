using System.Collections;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace GameUI
{
    public class UI : MonoBehaviour
    {
        public ItemToolTipUI ItemToolTipUI;
        public StatToolTipUI StatToolTipUI;
        public CraftWindowUI CraftWindowUI;
        public SkillToolTipUI SkillToolTipUI;

        [SerializeField] private GameObject _characterUI;
        [SerializeField] private GameObject _skillTreeUI;
        [SerializeField] private GameObject _craftUI;
        [SerializeField] private GameObject _optionsUI;
        [SerializeField] private GameObject _inGameUI;
        
        [Space]
        
        [Header("End Screen")]
        [SerializeField] private FadeScreenUI fadeScreenUI;
        [SerializeField] private GameObject endText;
        [SerializeField] private GameObject restartButton;

        private void Awake()
        {
            SwitchTo(this._skillTreeUI); // We need this to fix the order of the assigned events
            
            this.fadeScreenUI.gameObject.SetActive(true);
        }
        // Start is called before the first frame update
        private void Start()
        {
            this.ItemToolTipUI = GetComponentInChildren<ItemToolTipUI>(true);

            Invoke(nameof(SwitchToInGameUI), 0f); // We need this delay to prevent bugs in SaveManager when loading the game

            this.ItemToolTipUI.HideTooltip();

            this.StatToolTipUI.HideTooltip();
        }

        private void SwitchToInGameUI()
        {   
            SwitchTo(null);
            SwitchTo(this._inGameUI);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SwitchWithKeyTo(this._characterUI);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                SwitchWithKeyTo(this._craftUI);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                SwitchWithKeyTo(this._skillTreeUI);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                SwitchWithKeyTo(this._optionsUI);
            }
        }

        public void SwitchTo(GameObject menu)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var isFadeScreen = transform.GetChild(i).GetComponent<FadeScreenUI>() is not null;
                
                if (isFadeScreen) continue;
                
                transform.GetChild(i).gameObject.SetActive(false);
            }

            if (!menu)
            {
                SwitchTo(this._inGameUI);
                return;
            }
            
            menu.SetActive(true);
        }

        public void SwitchWithKeyTo(GameObject menu)
        {
            if (menu && menu.activeSelf)
            {
                menu.SetActive(false);
                SwitchTo(this._inGameUI);
                return;
            }

            SwitchTo(menu);
        }

        public void SwitchOnEndScreen()
        {
            this.fadeScreenUI.FadeOut();
            
            this.SwitchTo(null);
            
            StartCoroutine(EndScreenCoroutine());
        }

        IEnumerator EndScreenCoroutine()
        {
            yield return new WaitForSeconds(1f);
            
            endText.SetActive(true);
            
            yield return new WaitForSeconds(1f);
            
            restartButton.SetActive(true);
        }
        
        public void RestartGameAction() => GameManager.Instance.RestartScene();
    }
}
