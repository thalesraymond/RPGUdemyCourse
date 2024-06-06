using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
    private void OnEnable()
    {
        UpdateSlot(this.Item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        var equipmentToCraft = this.Item?.ItemData as EquipmentItemData;

        if (equipmentToCraft == null)
        {
            Debug.LogWarning("Item is not an equipment item");
            return;
        }

        var canCraft = Inventory.Instance.CanCraftItem(equipmentToCraft, equipmentToCraft.CraftingMaterials);

        if(canCraft)
        {
            Debug.Log("Here is your item: " + equipmentToCraft.ItemName);
        }
    }
}
