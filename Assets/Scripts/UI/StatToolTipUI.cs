using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatToolTipUI : ToolTipUI
{
    [SerializeField] private TextMeshProUGUI _statDescriptionText;

    public void ShowTooltip(string text)
    {
        this._statDescriptionText.text = text;

        this.PositionToolTip();
    }
}
