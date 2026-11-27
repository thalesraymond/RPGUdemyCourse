using System;
using PlayerStateMachine;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private EntityVFX _entityVFX;
    private Entity _entity;
    
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;
    
    public bool IsDead => this.isDead;
    
    [Header("On Damage Knockback")]
    [SerializeField] protected Vector2 knockbackPower = new(1.5f, 2.5f);
    [SerializeField] protected float knockbackDuration = 0.2f;
    [SerializeField] private Vector2 heavyKnockbackPower = new(7, 7);
    [SerializeField] private float heavyKnockbackDuration = 0.5f;
    
    [Header("On Heavy Damage")]
    [SerializeField] protected float heavyDamageThreshold = 0.3f; 

    protected virtual void Awake()
    {
        this._entityVFX = this.GetComponent<EntityVFX>();
        this._entity = this.GetComponent<Entity>();
        this.currentHealth = this.maxHealth;
        
        if(this._entityVFX == null)
            throw new MissingComponentException("EntityVFX");
    }

    public virtual void TakeDamage(float damage, Transform attacker)
    {
        if (this.isDead)
            return;
        
        this._entityVFX.PlayOnDamageVFX();

        this.ReduceHealth(damage);
        
        var isHeavyDamage = this.IsHeavyDamage(damage);
        this._entity.ReceiveKnockback(
            this.CalculateKnockbackPower(attacker, isHeavyDamage), 
            this.CalculateKnockbackDuration(isHeavyDamage));
    }

    private Vector2 CalculateKnockbackPower(Transform attacker, bool isHeavyDamage)
    {
        var direction = this.transform.position.x > attacker.position.x
            ? 1
            : -1;
        
        var adjustedKnockback = isHeavyDamage ? this.heavyKnockbackPower : this.knockbackPower;

        adjustedKnockback.x *= direction;
        
        return adjustedKnockback;
    }
    
    private float CalculateKnockbackDuration(bool isHeavyDamage) => isHeavyDamage 
        ? this.heavyKnockbackDuration 
        : this.knockbackDuration;
    
    private bool IsHeavyDamage(float damage) => damage / this.maxHealth >= this.heavyDamageThreshold;

    private void ReduceHealth(float damage)
    {
        this.currentHealth -= damage;

        if (this.currentHealth <= 0) this.Die();
    }

    private void Die()
    {
        this.isDead = true;
        Debug.Log("Entity died.");
    }
}