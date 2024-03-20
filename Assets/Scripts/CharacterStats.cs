using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat Strength;
    public Stat Damage;
    public Stat MaxHealthPoints;

    public int CurrentHealthPoints;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        CurrentHealthPoints = MaxHealthPoints.GetValue();
    }

    public virtual void DoDamage(CharacterStats targetStats)
    {
        var totalDamage = Damage.GetValue() + Strength.GetValue();

        targetStats.TakeDamage(totalDamage);
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
