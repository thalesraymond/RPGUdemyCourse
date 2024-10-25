using Inventory;
using UnityEngine;

namespace Stats
{
    public class EnemyStats : CharacterStats
    {
        private Enemy.Enemy _enemy;
        private ItemDrop _itemDropSystem;

        [Header("Level details")]
        [SerializeField] private int _level = 1;
        [Range(0f, 1f)][SerializeField] private float _percentageModifier = .4f;
        protected override void Start()
        {
            base.Start();

            _enemy = GetComponent<Enemy.Enemy>();

            _itemDropSystem = GetComponentInChildren<ItemDrop>();

            ApplyLevelModifier();
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
            Modify(this.Evasion);
            Modify(this.MagicResistance);

            Modify(this.CriticalHitChance);
            Modify(this.CriticalHitPower);
        }

        private void Modify(Stat stat)
        {
            for (var i = 1; i < _level; i++)
            {
                var modifier = stat.GetValue() * _percentageModifier;

                stat.AddModifier(Mathf.RoundToInt(modifier));
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        protected override void Die()
        {
            base.Die();

            _enemy.Die();

            this._itemDropSystem.GenerateDrops();
        }
    }
}
