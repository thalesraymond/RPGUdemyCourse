using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffEffec", menuName = "Data/Item Effect/Buff Effect")]
public class BuffEffect : ItemEffect
{
    private PlayerStats _playerStats;

    [SerializeField] private int _buffAmount;
    [SerializeField] private float _buffDuration;
    [SerializeField] private StatType _type;

    public override void ExecuteEffect(Transform enemyPosition)
    {
        base.ExecuteEffect(enemyPosition);

        _playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        _playerStats.IncreaseStatBy(_buffAmount, _buffDuration, this.GetStatToModify());
    }

    private Stat GetStatToModify()
    {
        switch (_type)
        {
            case StatType.Strength:
                return _playerStats.Strength;
            case StatType.Agility:
                return _playerStats.Agility;
            case StatType.Intelligence:
                return _playerStats.Intelligence;
            case StatType.Vitality:
                return _playerStats.Vitality;
            case StatType.Damage:
                return _playerStats.Damage;
            case StatType.CriticalHitChance:
                return _playerStats.CriticalHitChance;
            case StatType.CriticalHitPower:
                return _playerStats.CriticalHitPower;
            case StatType.FireDamage:
                return _playerStats.FireDamage;
            case StatType.IceDamage:
                return _playerStats.IceDamage;
            case StatType.LightningDamage:
                return _playerStats.LightningDamage;
            case StatType.Armor:
                return _playerStats.Armor;
            case StatType.Evasion:
                return _playerStats.Evasion;
            case StatType.MagicResistance:
                return _playerStats.MagicResistance;
            case StatType.Health:
                return _playerStats.MaxHealthPoints;
            default:
                return null;
        }
    }
}
