using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : ItemSlotUI
{
    public EquipmentType SlotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment Slot - " + SlotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        var equipment = this.Item?.ItemData as EquipmentItemData;

        if (equipment == null)
            return;

        Inventory.Instance.UnequipItem(new KeyValuePair<EquipmentItemData, InventoryItem>(equipment, this.Item));

        Inventory.Instance.AddItem(equipment);

        this.ClearSlot();
    }
}
