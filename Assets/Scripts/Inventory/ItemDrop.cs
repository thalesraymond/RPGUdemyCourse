using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int _possibleItemDrop;
    [SerializeField] private GameObject _dropPrefab;
    [SerializeField] private ItemData[] _possibleDrops;
    [SerializeField] private List<ItemData> _actualDrops = new List<ItemData>();

    private int _actualPossibleItemDropCount => Mathf.Min(this._possibleItemDrop, this._actualDrops.Count);

    public virtual void GenerateDrops()
    {
        foreach (var possibleDrop in _possibleDrops)
        {
            if (Random.Range(0, 100) > possibleDrop.DropChance)
                continue;

            _actualDrops.Add(possibleDrop);
        }

        for (var i = 0; i < this._actualPossibleItemDropCount; i++)
        {
            var itemData = this._actualDrops[Random.Range(0, _actualDrops.Count)];

            this._actualDrops.Remove(itemData);

            DropItem(itemData);
        }
    }
    protected void DropItem(ItemData itemData)
    {
        var newDrop = Instantiate(_dropPrefab, transform.position, Quaternion.identity);

        var randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newDrop.GetComponent<ItemObject>().SetupItem(itemData, randomVelocity);
    }
}
