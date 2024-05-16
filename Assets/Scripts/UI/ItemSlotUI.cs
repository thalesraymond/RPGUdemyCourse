using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemText;

    public InventoryItem Item;

    public void UpdateSlot(InventoryItem newItem)
    {
        this.Item = newItem;

        _itemImage.color = Color.white;

        if (this.Item != null)
        {
            _itemImage.sprite = this.Item.ItemData.ItemIcon;

            if (Item.StackSize > 1)
            {
                _itemText.text = Item.StackSize.ToString();
            }
            else
            {
                _itemText.text = string.Empty;
            }
        }
    }
}
