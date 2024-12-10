using Inventory;
using Managers;
using PlayerStates;
using UnityEngine;

namespace Stats
{
    public class PlayerStats : CharacterStats
    {
        private Player _player;

        private const float MinPlayerHealthPercentageToKnockback = .3f;
        
        private int MinPlayerHealthToKnockback => Mathf.RoundToInt(this.GetMaxHealthValue() * MinPlayerHealthPercentageToKnockback);
        
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _player = GetComponent<Player>();
        }
        
        
        protected override void Die()
        {
            base.Die();

            this._player.Die();

            var playerDrops = GetComponent<PlayerItemDrop>();

            if (playerDrops != null)
                playerDrops.GenerateDrops();
        }

        protected override void DecreaseHealthBy(int damage)
        {
            base.DecreaseHealthBy(damage);
            
            if (this.CurrentHealthPoints > 0 && this.CurrentHealthPoints <= this.MinPlayerHealthToKnockback)
            {
                this._player.SetupKnockbackPower(new Vector2(10,15));
                AudioManager.Instance.PlaySoundEffect(SoundEffect.PainScream01);
                this._player.PlayerFX.ScreenShake(this._player.PlayerFX.shakeHighDamage);
            }
            else
            {
                this._player.SetupKnockbackPower(Vector2.zero);
            }

            var currentArmor = Inventory.Inventory.Instance.GetEquipmentByType(EquipmentType.Armor);

            if (currentArmor != null)
                currentArmor.ExecuteItemEffect(this._player.transform);
        }

        public override void OnEvasion()
        {
            base.OnEvasion();

            this._player.SkillManager.DodgeSkill.CreateMirageOnDodge();
        }

        public void CloneDoDamage(CharacterStats targetStats, float attackMultiplier)
        {
            if (this.TargetCanAvoidAttack(targetStats))
                return;

            var totalDamage = Damage.GetValue() + Strength.GetValue();

            totalDamage = this.CheckTargetArmor(targetStats, totalDamage);
        
            if(attackMultiplier > 0)
                totalDamage = Mathf.RoundToInt(totalDamage * attackMultiplier);

            targetStats.TakeDamage(totalDamage);

            this.DoMagicalDamage(targetStats);
        }

        public override void OnApplyModifier()
        {
            base.OnApplyModifier();
            
            Inventory.Inventory.Instance.UpdateSlotAndStatsUI();
        }
    }
}