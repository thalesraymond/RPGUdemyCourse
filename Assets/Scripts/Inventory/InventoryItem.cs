using System;

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

    public override bool Equals(object obj)
    {
        if (obj is InventoryItem other)
        {
            return ItemData.Equals(other.ItemData);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return ItemData.GetHashCode();
    }
}
