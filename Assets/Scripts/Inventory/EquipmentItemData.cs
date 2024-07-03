using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Equipment Data", menuName = "Data/Equipment")]
public class EquipmentItemData : ItemData
{
    public EquipmentType EquipmentType;

    public ItemEffect[] Effects;

    [Header("Major Stats")]
    public int StrengthModifier;
    public int AgilityModifier;
    public int IntelligenceModifier;
    public int VitalityModifier;

    [Header("Offensive Stats")]
    public int Damage;
    public int CriticalHitChance;
    public int CriticalHitPower;

    [Header("Magic Stats")]
    public int FireDamage;
    public int IceDamage;
    public int LightningDamage;

    [Header("Defensive Stats")]
    public int Armor;
    public int Evasion;
    public int MagicResistance;
    public int Health;

    [Header("Crafting")]
    public List<InventoryItem> CraftingMaterials;

    public void AddModifier()
    {
        var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        playerStats.Strength.AddModifier(this.StrengthModifier);
        playerStats.Agility.AddModifier(this.AgilityModifier);
        playerStats.Intelligence.AddModifier(this.IntelligenceModifier);
        playerStats.Vitality.AddModifier(this.VitalityModifier);

        playerStats.Damage.AddModifier(this.Damage);
        playerStats.CriticalHitChance.AddModifier(this.CriticalHitChance);
        playerStats.CriticalHitPower.AddModifier(this.CriticalHitPower);

        playerStats.FireDamage.AddModifier(this.FireDamage);
        playerStats.IceDamage.AddModifier(this.IceDamage);
        playerStats.LightningDamage.AddModifier(this.LightningDamage);

        playerStats.Armor.AddModifier(this.Armor);
        playerStats.Evasion.AddModifier(this.Evasion);
        playerStats.MagicResistance.AddModifier(this.MagicResistance);
        playerStats.MaxHealthPoints.AddModifier(this.Health);
    }

    public void RemoveModifier()
    {
        var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        playerStats.Strength.RemoveModifier(this.StrengthModifier);
        playerStats.Agility.RemoveModifier(this.AgilityModifier);
        playerStats.Intelligence.RemoveModifier(this.IntelligenceModifier);
        playerStats.Vitality.RemoveModifier(this.VitalityModifier);

        playerStats.Damage.RemoveModifier(this.Damage);
        playerStats.CriticalHitChance.RemoveModifier(this.CriticalHitChance);
        playerStats.CriticalHitPower.RemoveModifier(this.CriticalHitPower);

        playerStats.FireDamage.RemoveModifier(this.FireDamage);
        playerStats.IceDamage.RemoveModifier(this.IceDamage);
        playerStats.LightningDamage.RemoveModifier(this.LightningDamage);

        playerStats.Armor.RemoveModifier(this.Armor);
        playerStats.Evasion.RemoveModifier(this.Evasion);
        playerStats.MagicResistance.RemoveModifier(this.MagicResistance);
        playerStats.MaxHealthPoints.RemoveModifier(this.Health);

    }

    public void ExecuteItemEffect(Transform enemyPosition)
    {
        foreach (var effect in this.Effects)
        {
            effect.ExecuteEffect(enemyPosition);
        }
    }
}
