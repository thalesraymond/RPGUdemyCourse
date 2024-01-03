using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float Cooldown;
    protected float CooldownTimer;

    protected virtual void Update()
    {
        CooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(CooldownTimer < 0)
        {
            UseSkill();
            CooldownTimer = Cooldown;
            return true;
        }

        Debug.Log("Skill on cooldown");
        return false;
    }

    public virtual void UseSkill()
    {
        // TODO: Use Skill
    }
}
