using TMPro;
using UnityEngine;

namespace GameUI
{
    public class StatToolTipUI : ToolTipUI
    {
        [SerializeField] private TextMeshProUGUI _statDescriptionText;

        public void ShowTooltip(string text)
        {
            this._statDescriptionText.text = text;

            this.PositionToolTip();
        }
    }
}
