using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = _itemData.ItemIcon;

        gameObject.name = "Item Object - " + _itemData.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Inventory.Instance.AddItem(this._itemData);

            Destroy(gameObject);
        }

    }
}
