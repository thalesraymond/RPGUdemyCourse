using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

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

    public bool IsDead {get; private set;}

    // Start is called before the first frame update
    protected virtual void Start()
    {
        CurrentHealthPoints = this.GetMaxHealthValue();

        _entityFX = GetComponent<EntityFX>();
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
        if (this.TargetCanAvoidAttack(targetStats))
            return;

        var totalDamage = Damage.GetValue() + Strength.GetValue();

        totalDamage = this.CheckTargetArmor(targetStats, totalDamage);

        targetStats.TakeDamage(totalDamage);

        //this.DoMagicalDamage(targetStats);
    }

    private int CheckTargetArmor(CharacterStats targetStats, int totalDamage)
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

    private bool TargetCanAvoidAttack(CharacterStats targetStats)
    {
        var totalEvasion = targetStats.Evasion.GetValue();

        if (this.IsShocked)
        {
            totalEvasion += 20;
        }

        return Random.Range(0, 100) < totalEvasion;
    }

    public virtual void TakeDamage(int damage)
    {
        this.DecreaseHealthBy(damage);

        GetComponent<Entity>().DamageImpact();

        this._entityFX.StartCoroutine("FlashFx");

    }

    protected virtual void DecreaseHealthBy(int damage)
    {
        CurrentHealthPoints -= damage;

        if(this.OnHealthChanged != null)
            this.OnHealthChanged();

        if (CurrentHealthPoints <= 0 && !this.IsDead)
            Die();
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
        int trueCount = (ignited ? 1 : 0) + (chilled ? 1 : 0) + (shocked ? 1 : 0);

        // If two or more flags are true, select a random one and set the others to false
        if (trueCount >= 2)
        {
            // Create an array to store the flags
            bool[] flags = { ignited, chilled, shocked };

            // Create a list to store the indices of true flags
            var trueIndices = new List<int>();
            for (int i = 0; i < flags.Length; i++)
            {
                if (flags[i])
                {
                    trueIndices.Add(i);
                }
            }

            // Select a random true flag
            int randomIndex = Random.Range(0, trueIndices.Count);
            int selectedFlagIndex = trueIndices[randomIndex];

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
        else if(this.IsChilled)
        {
            this._entityFX.ChillFxFor(this._ailmentDurantion);

            var slowPercentage = 0.2f;

            GetComponent<Entity>().SlowEntityBy(slowPercentage, this._ailmentDurantion);
        }
        else if(this.IsShocked)
        {
            if(GetComponent<Player>() != null)
                return;

            var closestEnemy = Physics2D.OverlapCircleAll(transform.position, 25)
                    .Where(hit => hit.GetComponent<Enemy>() is not null)
                    .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
                    .Skip(1)
                    .FirstOrDefault();

            if(closestEnemy != null)
            {
                var newThunderstrike = Instantiate(this._thunderStrikePrefab, transform.position, Quaternion.identity);

                newThunderstrike.GetComponent<ThunderstrikeController>().Setup(this._shockDamage, closestEnemy.GetComponent<CharacterStats>());
            }

            this._entityFX.ShockedFxFor(this._ailmentDurantion);
        }
    }

    public void SetupIgniteDamage(int damage) => this._igniteDamage = damage;

    public int GetMaxHealthValue() => MaxHealthPoints.GetValue() + Vitality.GetValue() * 5;
}
