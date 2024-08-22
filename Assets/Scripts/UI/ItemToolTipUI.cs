using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTipUI : ToolTipUI
{
    [SerializeField] private TextMeshProUGUI _itemNameText;

    [SerializeField] private TextMeshProUGUI _itemDescriptionText;

    [SerializeField] private TextMeshProUGUI _itemTypeText;

    public void ShowTooltip(EquipmentItemData item)
    {
        if(item == null)
        {
            HideTooltip();
            return;
        }
         
        _itemNameText.text = item.ItemName;
        _itemTypeText.text = item.ItemType.ToString();
        _itemDescriptionText.text = item.GetDescription();

        this.PositionToolTip();
    }
}
