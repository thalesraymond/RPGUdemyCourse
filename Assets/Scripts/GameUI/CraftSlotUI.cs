using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUI
{
    public class CraftSlotUI : ItemSlotUI
    {
        public void SetupCraftSlot(EquipmentItemData equipmentItemData)
        {
            if (this.Item == null)
            {
                Debug.LogWarning("Null item");
                return;
            }

            this.Item.ItemData = equipmentItemData;

            this.ItemImage.sprite = equipmentItemData.ItemIcon;

            this.ItemText.text = equipmentItemData.ItemName;

            if (this.ItemText.text.Length > 12)
            {
                this.ItemText.fontSize = this.ItemText.fontSize * .7f;
            }
            else
            {
                this.ItemText.fontSize = 24;
            }
        }

        private void OnEnable()
        {
            UpdateSlot(this.Item);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            var equipment = this.Item?.ItemData as EquipmentItemData;

            if (equipment == null)
            {
                Debug.LogWarning("Item is not an equipment item");
                return;
            }

            this.UI.CraftWindowUI.SetupCraftWindow(this.Item.ItemData as EquipmentItemData);
        }
    }
}
