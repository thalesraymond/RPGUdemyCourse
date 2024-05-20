using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> InventoryItems;

    public Dictionary<ItemData, InventoryItem> InventoryDictionary;

    public List<InventoryItem> StashItems;
    public Dictionary<ItemData, InventoryItem> StashDictionary;

    [Header("UI")]
    [SerializeField] private Transform _inventorySlotParent;
    [SerializeField] private Transform _stashSlotParent;

    private ItemSlotUI[] _inventoryItemsSlots;
    private ItemSlotUI[] _stashItemsSlots;

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

        StashItems = new List<InventoryItem>();

        InventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        StashDictionary = new Dictionary<ItemData, InventoryItem>();

        this._inventoryItemsSlots = _inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();

        this._stashItemsSlots = _stashSlotParent.GetComponentsInChildren<ItemSlotUI>();
    }

    private void UpdateSlots(ItemSlotUI[] slots, List<InventoryItem> items)
    {
        for (int i = 0; i < items.Count; i++)
            slots[i].UpdateSlot(items[i]);
    }

    private void UpdateSlotUI()
    {
        UpdateSlots(this._inventoryItemsSlots, this.InventoryItems);
        UpdateSlots(this._stashItemsSlots, this.StashItems);
    }

    public void AddItem(ItemData item)
    {
        switch (item.ItemType)
        {
            case ItemType.Material:
                this.AddToInventory(item, StashDictionary, StashItems);
                break;
            case ItemType.Equipment:
                this.AddToInventory(item, InventoryDictionary, InventoryItems);
                break;
            default:
                Debug.Log("Invalid item type");
                break;
        }

        UpdateSlotUI();
    }

    private void AddToInventory(ItemData item, Dictionary<ItemData, InventoryItem> inventoryDictionary, List<InventoryItem> inventoryList)
    {
        if (inventoryDictionary.TryGetValue(item, out var foundInventoryItem))
            foundInventoryItem.AddStack();
        else
        {
            var inventoryItem = new InventoryItem(item);

            inventoryList.Add(inventoryItem);
            inventoryDictionary.Add(item, inventoryItem);
        }
    }

    public void RemoveItem(ItemData item)
    {
        switch (item.ItemType)
        {
            case ItemType.Material:
                this.RemoveFromInventory(item, StashDictionary, StashItems);
                break;
            case ItemType.Equipment:
                this.RemoveFromInventory(item, InventoryDictionary, InventoryItems);
                break;
            default:
                Debug.Log("Invalid item type");
                break;
        }

        UpdateSlotUI();
    }

    private void RemoveFromInventory(ItemData item, Dictionary<ItemData, InventoryItem> inventoryDictionary, List<InventoryItem> inventoryList)
    {
        if (!inventoryDictionary.TryGetValue(item, out var foundInventoryItem))
            return;

        if (foundInventoryItem.StackSize <= 1)
        {
            inventoryList.Remove(foundInventoryItem);
            inventoryDictionary.Remove(item);
        }
        else
            foundInventoryItem.RemoveStack();
    }
}
