using TMPro;
using UnityEngine;

namespace UI
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
