using PlayerStateMachine;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected bool isDead;

    public virtual void TakeDamage(float damage, Transform attacker)
    {
        if (this.isDead)
            return;

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