using Inventory;
using UnityEngine.EventSystems;

namespace GameUI
{
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

            Inventory.Inventory.Instance.UnequipItem(equipment);

            Inventory.Inventory.Instance.AddItem(equipment);

            this.ClearSlot();
        }
    }
}
