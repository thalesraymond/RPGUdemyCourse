using TMPro;
using UnityEngine;

public class SkillToolTipUI : ToolTipUI
{
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private TextMeshProUGUI _skillDescriptionText;

    public void ShowTooltip(string name, string description)
    {
        _skillNameText.text = name;
        this._skillDescriptionText.text = description;

        this.PositionToolTip(80);
    }
}
