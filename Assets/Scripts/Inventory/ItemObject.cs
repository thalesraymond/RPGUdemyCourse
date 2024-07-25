using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void SetupVisuals()
    {
        if (this._itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = _itemData.ItemIcon;

        gameObject.name = "Item Object - " + _itemData.name;
    }

    public void PickUpItem()
    {
        if(!Inventory.Instance.CanAddEquipmentItem() && this._itemData.ItemType == ItemType.Equipment) 
        {
            this._rigidbody.velocity = new Vector2(0, 7);
            
            return;
        }
            

        Inventory.Instance.AddItem(this._itemData);

        Destroy(gameObject);
    }

    public void SetupItem(ItemData itemData, Vector2 velocity)
    {
        this._itemData = itemData;
        this._rigidbody.velocity = velocity;

        this.SetupVisuals();
    }
}
