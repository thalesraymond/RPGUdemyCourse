using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat Damage;
    public Stat MaxHealthPoints;

    public int CurrentHealthPoints;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints.GetValue();

        //example equip sword

        this.Damage.AddModifier(4);
    }

    public void TakeDamage(int demage)
    {
        CurrentHealthPoints -= demage;

        if (CurrentHealthPoints <= 0)
            Die();
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}
