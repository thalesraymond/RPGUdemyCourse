using Inventory.Effects;
using Stats;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StatSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        [SerializeField] private string _statName;
        [SerializeField] private TextMeshProUGUI _statValueText;
        [SerializeField] private TextMeshProUGUI _statNameText;
        [SerializeField] private StatType _statType;
        [SerializeField][TextArea] private string _statDescription;

        private UI _ui;

        private void Start()
        {
            this.UpdateStatValueUI();

            _ui = GetComponentInParent<UI>();
        }
        void OnValidate()
        {
            gameObject.name = "Stat - " + _statName;

            if (_statNameText != null)
            {
                _statNameText.text = _statName;
            }
        }
        public void UpdateStatValueUI()
        {
            var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

            if (playerStats == null)
            {
                return;
            }

            switch (_statType)
            {
                case StatType.Health:
                    _statValueText.text = playerStats.GetMaxHealthValue().ToString();
                    break;
                case StatType.Damage:
                    _statValueText.text = (playerStats.Damage.GetValue() + playerStats.Strength.GetValue()).ToString();
                    break;
                case StatType.CriticalHitPower:
                    _statValueText.text = (playerStats.CriticalHitPower.GetValue() + playerStats.Strength.GetValue()).ToString();
                    break;
                case StatType.CriticalHitChance:
                    _statValueText.text = (playerStats.CriticalHitChance.GetValue() + playerStats.Agility.GetValue()).ToString();
                    break;
                case StatType.Evasion:
                    _statValueText.text = (playerStats.Evasion.GetValue() + playerStats.Agility.GetValue()).ToString();
                    break;
                case StatType.MagicResistance:
                    _statValueText.text = (playerStats.MagicResistance.GetValue() + playerStats.Intelligence.GetValue()).ToString();
                    break;
                default:
                    _statValueText.text = playerStats.StatOfType(_statType).GetValue().ToString();
                    break;

            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this._ui.StatToolTipUI.ShowTooltip(_statDescription);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this._ui.StatToolTipUI.HideTooltip();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            this._ui.StatToolTipUI.ShowTooltip(_statDescription);
        }
    }
}
