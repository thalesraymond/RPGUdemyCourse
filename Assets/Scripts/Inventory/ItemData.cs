using System.Text;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType ItemType;

    public string ItemName;

    public Sprite ItemIcon;

    [Range(0, 100)]
    public float DropChance;

    protected StringBuilder sb = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}
