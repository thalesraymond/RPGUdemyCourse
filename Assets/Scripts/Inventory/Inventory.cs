using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<InventoryItem> EquipmentItems;
    public Dictionary<EquipmentItemData, InventoryItem> EquipmentDictionary;

    public List<InventoryItem> InventoryItems;
    public Dictionary<ItemData, InventoryItem> InventoryDictionary;

    public List<InventoryItem> StashItems;
    public Dictionary<ItemData, InventoryItem> StashDictionary;

    [Header("UI")]
    [SerializeField] private Transform _inventorySlotParent;
    [SerializeField] private Transform _stashSlotParent;
    [SerializeField] private Transform _equipmentSlotParent;

    private ItemSlotUI[] _inventoryItemsSlots;
    private ItemSlotUI[] _stashItemsSlots;
    private EquipmentSlotUI[] _equipmentItemsSlots;

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

        EquipmentItems = new List<InventoryItem>();

        InventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        StashDictionary = new Dictionary<ItemData, InventoryItem>();

        EquipmentDictionary = new Dictionary<EquipmentItemData, InventoryItem>();

        this._inventoryItemsSlots = _inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();

        this._stashItemsSlots = _stashSlotParent.GetComponentsInChildren<ItemSlotUI>();

        this._equipmentItemsSlots = _equipmentSlotParent.GetComponentsInChildren<EquipmentSlotUI>();
    }

    public void EquipItem(ItemData item)
    {
        var equipment = item as EquipmentItemData;

        if(equipment == null)
        {
            Debug.LogWarning("Item is not an equipment item");
            return;
        }

        this.RemoveItem(item);

        var sameTypeEquipped = this.EquipmentDictionary.FirstOrDefault(equippedItem => equippedItem.Key.EquipmentType == equipment.EquipmentType);

        if(sameTypeEquipped.Value != null)
        {
            this.UnequipItem(sameTypeEquipped);

            this.AddItem(sameTypeEquipped.Value.ItemData);
        }

        // add new equipment
        this.EquipmentDictionary.Add(equipment, new InventoryItem(item));
        this.EquipmentItems.Add(new InventoryItem(item));

        equipment.AddModifier();

        UpdateSlotUI();
    }

    private void UnequipItem(KeyValuePair<EquipmentItemData, InventoryItem> sameTypeEquipped)
    {
        this.EquipmentDictionary.Remove(sameTypeEquipped.Key);

        this.EquipmentItems.Remove(sameTypeEquipped.Value);

        sameTypeEquipped.Key.RemoveModifier();
    }

    private void UpdateSlots(ItemSlotUI[] slots, List<InventoryItem> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var itemToPut = i < items.Count ? items[i] : null;

            slots[i].UpdateSlot(itemToPut);
        }
            
    }

    private void UpdateSlotUI()
    {
        UpdateSlots(this._inventoryItemsSlots, this.InventoryItems);
        UpdateSlots(this._stashItemsSlots, this.StashItems);
        foreach (var slot in this._equipmentItemsSlots)
        {
            slot.UpdateSlot(this.EquipmentDictionary.FirstOrDefault(equipment => equipment.Key.EquipmentType == slot.SlotType).Value);
        }
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
