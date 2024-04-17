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

    [SerializeField] public bool IsIgnited;
    [SerializeField] public bool IsChilled;
    [SerializeField] public bool IsShocked;


    public int CurrentHealthPoints;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        CurrentHealthPoints = MaxHealthPoints.GetValue();
    }

    public virtual void DoDamage(CharacterStats targetStats)
    {
        if (this.TargetCanAvoidAttack(targetStats))
            return;

        var totalDamage = Damage.GetValue() + Strength.GetValue();

        totalDamage = this.CheckTargetArmor(targetStats, totalDamage);

        //targetStats.TakeDamage(totalDamage);

        this.DoMagicalDamage(targetStats);
    }

    private int CheckTargetArmor(CharacterStats targetStats, int totalDamage)
    {
        totalDamage -= targetStats.Armor.GetValue();

        totalDamage = Mathf.Clamp(0, totalDamage, int.MaxValue);

        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats targetStats)
    {
        var totalEvasion = targetStats.Evasion.GetValue();

        return Random.Range(0, 100) < totalEvasion;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealthPoints -= damage;

        Debug.Log(damage);

        if (CurrentHealthPoints <= 0)
            Die();
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
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

        this.ApplyAilment(canApplyIgnite, canApplyChill, canApplyShock);
    }

    private int CheckTargetMagicResistance(CharacterStats targetStats, int totalMagicalDamage)
    {
        totalMagicalDamage -= targetStats.MagicResistance.GetValue() + (targetStats.Intelligence.GetValue() * 3);

        totalMagicalDamage = Mathf.Clamp(0, totalMagicalDamage, int.MaxValue);

        return totalMagicalDamage;
    }

    public virtual void ApplyAilment(bool ignited, bool chilled, bool shocked)
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

        // Log the resulting values
        Debug.Log("is ignited: " + this.IsIgnited + " is chilled: " + this.IsChilled + " is shocked: " + this.IsShocked);
    }
}
