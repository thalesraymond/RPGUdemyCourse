using Enemies;
using Inventory;
using Managers;
using UnityEngine;

namespace Stats
{
    public class EnemyStats : CharacterStats
    {
        private Enemy _enemy;
        private ItemDrop _itemDropSystem;

        [Header("Level details")]
        [SerializeField] private int _level = 1;
        [Range(0f, 1f)][SerializeField] private float _percentageModifier = .4f;
        
        [Header("Souls Info")]
        [SerializeField] private Stat soulsDropAmount;
        
        protected override void Start()
        {
            this.soulsDropAmount.BaseValue = 100;

            _enemy = GetComponent<Enemy>();

            _itemDropSystem = GetComponentInChildren<ItemDrop>();

            ApplyLevelModifier();
            
            base.Start();
        }

        private void ApplyLevelModifier()
        {
            Modify(this.MaxHealthPoints);
            this.CurrentHealthPoints = this.MaxHealthPoints.GetValue();

            // Do not modify strength, agility, intelligence, vitality for enemies
            // Modify(this.Strength);
            // Modify(this.Agility);
            // Modify(this.Intelligence);
            // Modify(this.Vitality);

            Modify(this.Damage);
            Modify(this.FireDamage);
            Modify(this.IceDamage);
            Modify(this.LightningDamage);

            Modify(this.Armor);
            Modify(this.MaxHealthPoints);
            Modify(this.Evasion);
            Modify(this.MagicResistance);

            Modify(this.CriticalHitChance);
            Modify(this.CriticalHitPower);
            
            Modify(this.soulsDropAmount);
        }

        private void Modify(Stat stat)
        {
            for (var i = 1; i < _level; i++)
            {
                var modifier = stat.GetValue() * _percentageModifier;

                stat.AddModifier(Mathf.RoundToInt(modifier));
            }
        }

        protected override void Die()
        {
            base.Die();

            _enemy.Die();

            PlayerManager.Instance.Currency += this.soulsDropAmount.GetValue();

            this._itemDropSystem.GenerateDrops();
        }
    }
}
