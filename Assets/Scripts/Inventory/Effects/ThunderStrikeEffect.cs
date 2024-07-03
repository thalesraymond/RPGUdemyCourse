using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThunderStrikeEffect", menuName = "Data/Item Effect/Thunder Strike")]
public class ThunderStrikeEffect : ItemEffect
{
    [SerializeField] private GameObject _thunderStrikePrefab;
    public override void ExecuteEffect(Transform enemyPosition)
    {
        var newThunderstrike = Instantiate(_thunderStrikePrefab, enemyPosition.position, Quaternion.identity);

        Destroy(newThunderstrike, 0.7f);
    }
}
