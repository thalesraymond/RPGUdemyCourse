using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem 
{
    public ItemData ItemData;
    public int StackSize;

    public InventoryItem(ItemData itemData)
    {
        ItemData = itemData;
        AddStack();
    }

    public void AddStack() => StackSize++;

    public void RemoveStack() => StackSize--;
}
