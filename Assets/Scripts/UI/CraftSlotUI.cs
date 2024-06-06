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
        base.OnPointerDown(eventData);

        // inventory craft item  data
    }
}
