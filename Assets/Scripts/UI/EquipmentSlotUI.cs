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
        // Intentionaly left blank
        //base.OnPointerDown(eventData);
    }
}
