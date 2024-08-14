using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemText;

    private UI _ui;

    public InventoryItem Item;

    private void Start()
    {
        _ui = GetComponentInParent<UI>();
    }

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
        if(Input.GetKey(KeyCode.LeftControl) && this.Item != null)
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
        this._ui.ItemToolTipUI.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.Item == null)
            return;

        this._ui.ItemToolTipUI.ShowTooltip(this.Item.ItemData as EquipmentItemData);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (this.Item == null)
            return;

        this._ui.ItemToolTipUI.ShowTooltip(this.Item.ItemData as EquipmentItemData);
    }
}
