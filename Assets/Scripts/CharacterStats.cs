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


    public Stat Damage;


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

        targetStats.TakeDamage(totalDamage);
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
}
