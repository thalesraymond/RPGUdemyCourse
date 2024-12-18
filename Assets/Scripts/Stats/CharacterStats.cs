using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Effects;
using Enemies;
using Inventory.Effects;
using PlayerStates;
using UnityEngine;

namespace Stats
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Major stats")]
        public Stat Strength; //increases damage
        public Stat Agility; // increases evasion and crtical hit chance
        public Stat Intelligence; // increases magic damage
        public Stat Vitality; // increases health

        [Header("Defensive Stats")]
        public Stat MaxHealthPoints;
        public Stat Armor;
        public Stat Evasion;
        public Stat MagicResistance;

        [Header("Offensive Stats")]
        public Stat Damage;
        public Stat CriticalHitChance;
        public Stat CriticalHitPower;

        [Header("Magic Stats")]
        public Stat FireDamage;
        public Stat IceDamage;
        public Stat LightningDamage;

        public bool IsIgnited; // does damage over time
        public bool IsChilled; // reduce armor by 20%
        public bool IsShocked; // reduce accuracy by 20%

        [SerializeField] private float _ailmentDurantion = 4f;

        private float _ignitedTimer;
        private float _igniteDamageCooldown = .3f;
        private float _igniteDamageTimer;

        private float _shockedTimer;
        private float _chilledTimer;

        private int _igniteDamage;

        public int CurrentHealthPoints;

        public System.Action OnHealthChanged;

        private EntityFX _entityFX;

        [SerializeField] private GameObject _thunderStrikePrefab;
        [SerializeField] private int _shockDamage;

        public bool IsDead { get; private set; }

        public bool IsInvulnerable { get; private set; }

        private bool _isVulnerable;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            CurrentHealthPoints = this.GetMaxHealthValue();

            _entityFX = GetComponent<EntityFX>();
        }

        public virtual void OnApplyModifier()
        {
            
        }

        protected virtual void Update()
        {
            this._igniteDamageTimer -= Time.deltaTime;

            this._ignitedTimer -= Time.deltaTime;

            this._shockedTimer -= Time.deltaTime;

            this._chilledTimer -= Time.deltaTime;

            if (_ignitedTimer < 0)
                this.IsIgnited = false;

            if (_chilledTimer < 0)
                this.IsChilled = false;

            if (_shockedTimer < 0)
                this.IsShocked = false;

            this.ApplyIgniteDamage();
        }

        private void ApplyIgniteDamage()
        {
            if (this._igniteDamageTimer < 0 && this.IsIgnited)
            {
                this.DecreaseHealthBy(this._igniteDamage);

                _igniteDamageTimer = _igniteDamageCooldown;
            }
        }

        public virtual void DoDamage(CharacterStats targetStats)
        {
            var criticalStrike = false;
            
            if (this.TargetCanAvoidAttack(targetStats))
                return;
            
            targetStats.GetComponent<Entity>().SetupKnockbackDirection(transform);

            var totalDamage = Damage.GetValue() + Strength.GetValue();

            if (this.CanCritical())
            {
                totalDamage = CalculateCriticalDamage(totalDamage);
                criticalStrike = true;
            }

            totalDamage = this.CheckTargetArmor(targetStats, totalDamage);
            
            _entityFX.CreateHitFX(targetStats.transform, criticalStrike);

            targetStats.TakeDamage(totalDamage);

            this.DoMagicalDamage(targetStats);
        }

        private bool CanCritical()
        {
            var totalCriticalChance = this.CriticalHitChance.GetValue() + this.Agility.GetValue();

            return Random.Range(0, 100) <= totalCriticalChance;
        }

        private int CalculateCriticalDamage(int damage)
        {
            var totalCritPower = (this.CriticalHitPower.GetValue() + this.Strength.GetValue()) * .01f;
            var critDamage = damage * totalCritPower;

            return Mathf.RoundToInt(critDamage);
        }

        protected int CheckTargetArmor(CharacterStats targetStats, int totalDamage)
        {
            if (this.IsChilled)
            {
                totalDamage -= Mathf.RoundToInt(targetStats.Armor.GetValue() * .8f);
            }
            else
            {
                totalDamage -= targetStats.Armor.GetValue();
            }

            totalDamage = Mathf.Clamp(0, totalDamage, int.MaxValue);

            return totalDamage;
        }

        public virtual void OnEvasion()
        {

        }

        protected bool TargetCanAvoidAttack(CharacterStats targetStats)
        {
            var totalEvasion = targetStats.Evasion.GetValue();

            if (this.IsShocked)
            {
                totalEvasion += 20;
            }

            var canAvoid =  Random.Range(0, 100) < totalEvasion;

            if(canAvoid)
            {
                targetStats.OnEvasion();
            }

            return canAvoid;
        }

        public virtual void TakeDamage(int damage)
        {
            if (this.IsInvulnerable)
                return;
            
            this.DecreaseHealthBy(damage);

            GetComponent<Entity>().DamageImpact();

            this._entityFX.StartCoroutine("FlashFx");
        }

        protected virtual void DecreaseHealthBy(int damage)
        {
            if(this._isVulnerable)
                damage = Mathf.RoundToInt(damage * 1.1f);
            
            if(damage > 0)
                _entityFX.CreatePopUpText(damage.ToString(), Color.red);
            
            CurrentHealthPoints -= damage;

            if (this.OnHealthChanged != null)
                this.OnHealthChanged();

            if (CurrentHealthPoints <= 0 && !this.IsDead)
                Die();
        }

        public virtual void IncreaseHealthBy(int amount)
        {
            CurrentHealthPoints += amount;

            if (CurrentHealthPoints > this.GetMaxHealthValue())
                this.CurrentHealthPoints = this.GetMaxHealthValue();

            if (this.OnHealthChanged != null)
                this.OnHealthChanged();
        }

        protected virtual void Die()
        {
            this.IsDead = true;
        }

        public virtual void DoMagicalDamage(CharacterStats targetStats)
        {
            var fireDamage = this.FireDamage.GetValue();

            var iceDamage = this.IceDamage.GetValue();

            var lightningDamage = this.LightningDamage.GetValue();


            var totalMagicalDamage = fireDamage + iceDamage + lightningDamage + this.Intelligence.GetValue();

            totalMagicalDamage = this.CheckTargetMagicResistance(targetStats, totalMagicalDamage);

            targetStats.TakeDamage(totalMagicalDamage);

            var canApplyIgnite = fireDamage > 0;
            var canApplyChill = iceDamage > 0;
            var canApplyShock = lightningDamage > 0;

            targetStats.ApplyAilment(canApplyIgnite, fireDamage, canApplyChill, canApplyShock);
        }

        private int CheckTargetMagicResistance(CharacterStats targetStats, int totalMagicalDamage)
        {
            totalMagicalDamage -= targetStats.MagicResistance.GetValue() + (targetStats.Intelligence.GetValue() * 3);

            totalMagicalDamage = Mathf.Clamp(0, totalMagicalDamage, int.MaxValue);

            return totalMagicalDamage;
        }

        public virtual void ApplyAilment(bool ignited, int fireDamage, bool chilled, bool shocked)
        {
            // Check if any of the flags is already set
            if (this.IsIgnited || this.IsChilled || this.IsShocked)
                return;

            // Count the number of true flags
            var trueCount = (ignited ? 1 : 0) + (chilled ? 1 : 0) + (shocked ? 1 : 0);

            // If two or more flags are true, select a random one and set the others to false
            if (trueCount >= 2)
            {
                // Create an array to store the flags
                bool[] flags = { ignited, chilled, shocked };

                // Create a list to store the indices of true flags
                var trueIndices = new List<int>();
                for (var i = 0; i < flags.Length; i++)
                {
                    if (flags[i])
                    {
                        trueIndices.Add(i);
                    }
                }

                // Select a random true flag
                var randomIndex = Random.Range(0, trueIndices.Count);
                var selectedFlagIndex = trueIndices[randomIndex];

                // Set the selected flag to true and others to false
                this.IsIgnited = selectedFlagIndex == 0;
                this.IsChilled = selectedFlagIndex == 1;
                this.IsShocked = selectedFlagIndex == 2;
            }
            // If only one flag is true, set the corresponding ailment
            else
            {
                this.IsIgnited = ignited;
                this.IsChilled = chilled;
                this.IsShocked = shocked;
            }

            this._ignitedTimer = this.IsIgnited ? this._ailmentDurantion : 0;

            this._chilledTimer = this.IsChilled ? this._ailmentDurantion : 0;

            this._shockedTimer = this.IsShocked ? this._ailmentDurantion : 0;

            if (this.IsIgnited)
            {
                this.SetupIgniteDamage(Mathf.RoundToInt(fireDamage * .2f));

                this._entityFX.IgniteFxFor(this._ailmentDurantion);
            }
            else if (this.IsChilled)
            {
                this._entityFX.ChillFxFor(this._ailmentDurantion);

                var slowPercentage = 0.2f;

                GetComponent<Entity>().SlowEntityBy(slowPercentage, this._ailmentDurantion);
            }
            else if (this.IsShocked)
            {
                if (GetComponent<Player>() != null)
                    return;

                var closestEnemy = Physics2D.OverlapCircleAll(transform.position, 25)
                    .Where(hit => hit.GetComponent<Enemy>() is not null)
                    .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
                    .Skip(1)
                    .FirstOrDefault();

                if (closestEnemy != null)
                {
                    var newThunderstrike = Instantiate(this._thunderStrikePrefab, transform.position, Quaternion.identity);

                    newThunderstrike.GetComponent<ShockStrikeController>().Setup(this._shockDamage, closestEnemy.GetComponent<CharacterStats>());
                }

                this._entityFX.ShockedFxFor(this._ailmentDurantion);
            }
        }

        public void SetupIgniteDamage(int damage) => this._igniteDamage = damage;

        public int GetMaxHealthValue() => MaxHealthPoints.GetValue() + Vitality.GetValue() * 5;

        public virtual void IncreaseStatBy(int modifier, float duration, Stat statToModify)
        {
            StartCoroutine(IncreaseStatByCoroutine(modifier, duration, statToModify));
        }

        private IEnumerator IncreaseStatByCoroutine(int modifier, float duration, Stat statToModify)
        {
            statToModify.AddModifier(modifier);

            yield return new WaitForSeconds(duration);

            statToModify.RemoveModifier(modifier);
        }

        public Stat StatOfType(StatType type)
        {
            switch (type)
            {
                case StatType.Strength:
                    return this.Strength;
                case StatType.Agility:
                    return this.Agility;
                case StatType.Intelligence:
                    return this.Intelligence;
                case StatType.Vitality:
                    return this.Vitality;
                case StatType.Damage:
                    return this.Damage;
                case StatType.CriticalHitChance:
                    return this.CriticalHitChance;
                case StatType.CriticalHitPower:
                    return this.CriticalHitPower;
                case StatType.FireDamage:
                    return this.FireDamage;
                case StatType.IceDamage:
                    return this.IceDamage;
                case StatType.LightningDamage:
                    return this.LightningDamage;
                case StatType.Armor:
                    return this.Armor;
                case StatType.Evasion:
                    return this.Evasion;
                case StatType.MagicResistance:
                    return this.MagicResistance;
                case StatType.Health:
                    return this.MaxHealthPoints;
                default:
                    return null;
            }
        }

        public void MakeVulnerableFor(float duration) => StartCoroutine(VulnerableForCoroutine(duration));

        private IEnumerator VulnerableForCoroutine(float duration)
        {
            this._isVulnerable = true;

            yield return new WaitForSeconds(duration);

            this._isVulnerable = false;
        }
        
        public void MakeInvulnerable(bool isInvulnerable) => this.IsInvulnerable = isInvulnerable;
    }
}
