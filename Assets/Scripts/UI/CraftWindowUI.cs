using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftWindowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;

    [SerializeField] private TextMeshProUGUI _itemDescription;

    [SerializeField] private Image _itemIcon;

    [SerializeField] private Image[] _materialsImages;

    [SerializeField] private Button _craftButton;

    private void Start()
    {
        
    }

    public void SetupCraftWindow(EquipmentItemData equipmentItemData)
    {
        this._craftButton.onClick.RemoveAllListeners();

        for (int i = 0; i < _materialsImages.Length; i++)
        {
            _materialsImages[i].color = Color.clear;
            _materialsImages[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0; i < equipmentItemData.CraftingMaterials.Count; i++)
        {
            _materialsImages[i].sprite = equipmentItemData.CraftingMaterials[i].ItemData.ItemIcon;
            _materialsImages[i].color = Color.white;

            var textField = _materialsImages[i].GetComponentInChildren<TextMeshProUGUI>();

            textField.color = Color.white;
            textField.text = equipmentItemData.CraftingMaterials[i].StackSize.ToString();

        }

        this._itemIcon.sprite = equipmentItemData.ItemIcon;

        this._itemName.text = equipmentItemData.ItemName;

        this._itemDescription.text = equipmentItemData.GetDescription();

        this._craftButton.onClick.AddListener(() => Inventory.Instance.CanCraftItem(equipmentItemData, equipmentItemData.CraftingMaterials));
    }
}
