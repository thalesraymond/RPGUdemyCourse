using SaveAndLoad;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameUI
{
    public class SkillTreeSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, ISaveManager
    {
        public bool Unlocked;

        [SerializeField] private SkillTreeSlotUI[] _shouldBeUnlocked;
        [SerializeField] private SkillTreeSlotUI[] _shouldBeLocked;
        [SerializeField] private Image _skillImage;
        [SerializeField] private Button _unlockButton;

        [SerializeField] private string _skillName;
        [SerializeField][TextArea] private string _skillDescription;

        [SerializeField] private Color _lockedSkillColor;
        [SerializeField] private Color _unlockedSkillColor;

        [SerializeField] private int _skillPrice;

        protected UI UI;

        private void OnValidate()
        {
            gameObject.name = "SkillTreeSlotUI - " + this._skillName;
        }

        public void Start()
        {
            this.GetComponents();

            this.SetupUnlockedStatus();
        }

        private void SetupUnlockedStatus()
        {
            this.GetComponents();

            this._skillImage.color = this.Unlocked ? this._unlockedSkillColor : this._lockedSkillColor;
        }

        private void GetComponents()
        {
            if (this._skillImage == null)
            {
                this._skillImage = this.GetComponent<Image>();
            }

            if (this.UI == null)
            {
                this.UI = GetComponentInParent<UI>();
            }

            if (this._unlockButton == null)
            {
                this._unlockButton = GetComponent<Button>();
            }
        }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(UnlockSkillSlot);
        }

        public void UnlockSkillSlot()
        {
            foreach (var skillTreeSlot in this._shouldBeUnlocked)
            {
                if (!skillTreeSlot.Unlocked)
                {
                    Debug.Log("Requirements not unlocked");
                    return;
                }
            }

            foreach (var skillTreeSlot in this._shouldBeLocked)
            {
                if (skillTreeSlot.Unlocked)
                {
                    Debug.Log("Incompatible skill already unlocked");
                    return;
                }
            }

            if(!PlayerManager.Instance.HaveEnoughMoney(this._skillPrice))
            {
                Debug.Log("Not enough currency");
                return;
            }

            this.Unlocked = true;
            this._skillImage.color = this._unlockedSkillColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.UI.SkillToolTipUI.HideTooltip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.UI.SkillToolTipUI.ShowTooltip(this._skillName, this._skillDescription);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            this.UI.SkillToolTipUI.ShowTooltip(this._skillName, this._skillDescription);
        }

        public void LoadData(GameData data)
        {
            if(data.SkillTree.TryGetValue(this._skillName, out var skillUnlocked))
            {
                this.Unlocked = skillUnlocked;
                this.SetupUnlockedStatus();
            }
        }

        public void SaveData(ref GameData data)
        {
            if (data.SkillTree.TryGetValue(this._skillName, out _))
            {
                data.SkillTree.Remove(this._skillName);
                data.SkillTree.Add(this._skillName, this.Unlocked);
            }
            else
            {
                data.SkillTree.Add(this._skillName, this.Unlocked);
            }
        }
    }
}
