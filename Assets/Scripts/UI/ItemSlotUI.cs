using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemText;

    public InventoryItem Item;

    public void UpdateSlot(InventoryItem newItem)
    {
        this.Item = newItem;

        if (this.Item != null)
        {
            _itemImage.color = Color.white;

            _itemImage.sprite = this.Item.ItemData.ItemIcon;

            if (Item.StackSize > 1)
                _itemText.text = Item.StackSize.ToString();
            else
                _itemText.text = string.Empty;
        }
        else
        {
            this.ClearSlot();
        }
    }

    protected void ClearSlot()
    {
        _itemImage.sprite = null;
        _itemText.text = string.Empty;

        _itemImage.color = Color.clear;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (this.Item?.ItemData == null)
            return;

        if (this.Item.ItemData.ItemType != ItemType.Equipment)
            return;

        Inventory.Instance.EquipItem(this.Item.ItemData);
    }
}
