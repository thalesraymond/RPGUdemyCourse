using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<ItemData> StartingEquipment = new List<ItemData>();

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

    [Header("Items cooldown")]
    [SerializeField] private float _lastTimeUsedFlask;
    [SerializeField] private float _lastTimeUsedArmor;

    [SerializeField] private float _flaskCooldown;
    [SerializeField] private float _armorCooldown;

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

        AddStartingItems();
    }

    private void AddStartingItems()
    {
        foreach (var item in this.StartingEquipment)
        {
            this.AddItem(item);
        }
    }

    public void EquipItem(ItemData item)
    {
        var equipment = item as EquipmentItemData;

        if (equipment == null)
        {
            Debug.LogWarning("Item is not an equipment item");
            return;
        }

        this.RemoveItem(item);

        var sameTypeEquipped = this.EquipmentDictionary.FirstOrDefault(equippedItem => equippedItem.Key.EquipmentType == equipment.EquipmentType);

        if (sameTypeEquipped.Value != null)
        {
            this.UnequipItem(sameTypeEquipped.Key);

            this.AddItem(sameTypeEquipped.Value.ItemData);
        }

        // add new equipment
        this.EquipmentDictionary.Add(equipment, new InventoryItem(item));
        this.EquipmentItems.Add(new InventoryItem(item));

        equipment.AddModifier();

        UpdateSlotUI();
    }

    public void UnequipItem(EquipmentItemData equipment)
    {
        if (equipment == null)
        {
            Debug.LogWarning("Item is not an equipment item");
            return;
        }

        this.EquipmentDictionary.Remove(equipment);

        this.EquipmentItems.Remove(this.EquipmentItems.FirstOrDefault(item => item.ItemData == equipment));

        equipment.RemoveModifier();

        this.UpdateSlotUI();
    }

    private void UpdateSlots(ItemSlotUI[] slots, List<InventoryItem> items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            var itemToPut = i < items.Count ? items[i] : null;

            slots[i].UpdateSlot(itemToPut);
        }

    }

    public void UpdateSlotUI()
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

    public bool CanCraftItem(EquipmentItemData itemToCraft, List<InventoryItem> requiredMaterials)
    {
        var materialsToRemove = new List<InventoryItem>();
        foreach (InventoryItem v in requiredMaterials)
        {
            if (this.StashDictionary.TryGetValue(v.ItemData, out var stashValue))
            {
                if (stashValue.StackSize < v.StackSize)
                {
                    Debug.Log("not enough materials");

                    return false;
                }
                else
                {
                    materialsToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("not enough materials");

                return false;
            }
        }

        foreach (InventoryItem v1 in materialsToRemove)
        {
            this.RemoveItem(v1.ItemData);
        }

        this.AddItem(itemToCraft);

        Debug.Log("Here is your item: " + itemToCraft.ItemName);

        return true;
    }

    public List<InventoryItem> GetEquimentList() => this.EquipmentItems;

    public List<InventoryItem> GetStashList() => this.StashItems;

    public EquipmentItemData GetEquipmentByType(EquipmentType equipmentType)
    {
        var equipmentItemData = this.EquipmentDictionary.FirstOrDefault(item => item.Key.EquipmentType == equipmentType).Key;
        
        return equipmentItemData;
    }

    public void UseFlask()
    {
        var currentFlask = GetEquipmentByType(EquipmentType.Flask);

        if(currentFlask == null)
        {
            Debug.Log("No flask equipped");
            return;
        }

        var canUseFlask = Time.time > _lastTimeUsedFlask + this._flaskCooldown;

        if(!canUseFlask)
        {
            Debug.Log("Can't use flask");
            return;
        }

        currentFlask.ExecuteItemEffect(null);

        _lastTimeUsedFlask = Time.time;

        this._flaskCooldown = currentFlask.CooldownDuration;

        // remove flask
        this.UnequipItem(currentFlask);
        //this.RemoveItem(currentFlask);
    }

    public bool CanUseArmor()
    {
        var currentArmor = this.GetEquipmentByType(EquipmentType.Armor);

        if (currentArmor == null) 
        {
            Debug.Log("No armor equipped");
            return false;
        }

        if (Time.time > this._lastTimeUsedArmor + this._armorCooldown)
        {
            this._lastTimeUsedArmor = Time.time;

            this._armorCooldown = currentArmor.CooldownDuration;

            return true; 
        }

        Debug.Log("Can't use armor");

        return false;
    }
}
