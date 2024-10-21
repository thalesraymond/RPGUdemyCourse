using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Currency;
    public SerializableDictionary<string, int> InventoryItems;

    public GameData()
    {
        this.Currency = 0;

        this.InventoryItems = new SerializableDictionary<string, int>();
    }
}
