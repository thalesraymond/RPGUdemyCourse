using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> InventoryItems;

    public Dictionary<ItemData, InventoryItem> InventoryDictionary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InventoryItems = new List<InventoryItem>();

        InventoryDictionary = new Dictionary<ItemData, InventoryItem>();
    }

    public void AddItem(ItemData item)
    {
        if (InventoryDictionary.TryGetValue(item, out var foundInventoryItem))
        {
            foundInventoryItem.AddStack();
        }
        else
        {
            var inventoryItem = new InventoryItem(item);

            this.InventoryItems.Add(inventoryItem);
            this.InventoryDictionary.Add(item, inventoryItem);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (!InventoryDictionary.TryGetValue(item, out var foundInventoryItem))
            return;
        

        if (foundInventoryItem.StackSize <= 1)
        {
            this.InventoryItems.Remove(foundInventoryItem);
            this.InventoryDictionary.Remove(item);
        }
        else
        {
            foundInventoryItem.RemoveStack();
        }
    }
}
