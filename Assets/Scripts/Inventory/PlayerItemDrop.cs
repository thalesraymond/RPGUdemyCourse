using System.Linq;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's Drops")]
    [SerializeField][Range(0, 100)] private float _chanceToLooseItems;

    [SerializeField][Range(0, 100)] private float _chanceToLooseMaterials;

    public override void GenerateDrops()
    {
        var inventory = Inventory.Instance;

        var currentEquipment = inventory.GetEquimentList();
        var currentStash = inventory.GetStashList();

        var droppedItems = inventory.EquipmentItems.Where(item => Random.Range(0, 100) <= _chanceToLooseItems).ToList();

        var droppedStashItems = currentStash.Where(item => Random.Range(0, 100) <= _chanceToLooseMaterials).ToList();

        droppedItems.AddRange(droppedStashItems);

        foreach (var item in droppedItems)
        {
            inventory.RemoveItem(item.ItemData);

            if (item.ItemData is EquipmentItemData equipmentItemData)
                inventory.UnequipItem(equipmentItemData);

            DropItem(item.ItemData);
        }

        inventory.UpdateSlotUI();
    }
}
