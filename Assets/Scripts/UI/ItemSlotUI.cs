using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] protected Image ItemImage;
    [SerializeField] protected TextMeshProUGUI ItemText;

    protected UI UI;

    public InventoryItem Item;

    protected virtual void Start()
    {
        this.UI = GetComponentInParent<UI>();
    }

    public void UpdateSlot(InventoryItem newItem)
    {
        this.Item = newItem;

        if (this.Item != null)
        {
            ItemImage.color = Color.white;

            ItemImage.sprite = this.Item.ItemData.ItemIcon;

            if (Item.StackSize > 1)
                ItemText.text = Item.StackSize.ToString();
            else
                ItemText.text = string.Empty;
        }
        else
        {
            this.ClearSlot();
        }
    }

    protected void ClearSlot()
    {
        if (this.ItemImage == null || this.ItemText == null)
        {
            return;
        }

        ItemImage.sprite = null;
        ItemText.text = string.Empty;

        ItemImage.color = Color.clear;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl) && this.Item != null)
        {
            Inventory.Instance.RemoveItem(this.Item.ItemData);

            return;
        }

        if (this.Item?.ItemData == null)
            return;

        if (this.Item.ItemData.ItemType != ItemType.Equipment)
            return;

        Inventory.Instance.EquipItem(this.Item.ItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.UI.ItemToolTipUI.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.Item == null)
            return;

        this.UI.ItemToolTipUI.ShowTooltip(this.Item.ItemData as EquipmentItemData);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (this.Item == null)
            return;

        this.UI.ItemToolTipUI.ShowTooltip(this.Item.ItemData as EquipmentItemData);
    }
}
