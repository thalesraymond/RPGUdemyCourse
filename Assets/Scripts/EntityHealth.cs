using System;
using PlayerStateMachine;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private EntityVFX _entityVFX;
    
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        this._entityVFX = this.GetComponent<EntityVFX>();
        
        if(this._entityVFX == null)
            throw new MissingComponentException("EntityVFX");
    }

    public virtual void TakeDamage(float damage, Transform attacker)
    {
        if (this.isDead)
            return;
        
        this._entityVFX.PlayOnDamageVFX();

        this.ReduceHealth(damage);
    }

    private void ReduceHealth(float damage)
    {
        this.maxHealth -= damage;

        if (this.maxHealth <= 0) this.Die();
    }

    private void Die()
    {
        this.isDead = true;
        Debug.Log("Entity died.");
    }
}