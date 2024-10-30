using System.Collections.Generic;
using Inventory.Effects;
using Managers;
using Stats;
using UnityEngine;

namespace Inventory
{
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

        [Header("Unique Effects")]
        public ItemEffect[] Effects;
        [SerializeField][TextArea] public string ItemEffectDescription;

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

        [Header("Cooldown")]
        public float CooldownDuration;
    

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

        public override string GetDescription()
        {
            sb.Length = 0;

            this.AddItemDescription(this.StrengthModifier, "Strength");
            this.AddItemDescription(this.AgilityModifier, "Agility");
            this.AddItemDescription(this.IntelligenceModifier, "Intelligence");
            this.AddItemDescription(this.VitalityModifier, "Vitality");

            this.AddItemDescription(this.Damage, "Damage");
            this.AddItemDescription(this.CriticalHitChance, "Critical Hit Chance");
            this.AddItemDescription(this.CriticalHitPower, "Critical Hit Power");

            this.AddItemDescription(this.FireDamage, "Fire Damage");
            this.AddItemDescription(this.IceDamage, "Ice Damage");
            this.AddItemDescription(this.LightningDamage, "Lightning Damage");

            this.AddItemDescription(this.Armor, "Armor");
            this.AddItemDescription(this.Evasion, "Evasion");
            this.AddItemDescription(this.MagicResistance, "Magic Resistance");
            this.AddItemDescription(this.Health, "Health");

            for (var i = 0; i < this.Effects.Length; i++)
            {
                if(this.Effects[i].EffectDescription.Length > 0)
                {
                    sb.AppendLine();
                    sb.Append(this.Effects[i].EffectDescription);
                }
            }

            return sb.ToString();
        }

        private void AddItemDescription(int value, string name)
        {
            if (value != 0)
            {
                if (sb.Length > 0)
                    sb.AppendLine();

                if (value > 0)
                    sb.Append("+" + value + " " + name);
            }
        }
    }
}