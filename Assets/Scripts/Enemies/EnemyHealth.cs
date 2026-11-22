using System;
using PlayerStateMachine;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : EntityHealth
    {
        private Enemy _enemy;

        private void Awake()
        {
            this._enemy = this.GetComponent<Enemy>();
        }

        public override void TakeDamage(float damage, Transform attacker)
        {
            base.TakeDamage(damage, attacker);
            
            if (!this.isDead && damage > 0f)
            {
                this._enemy.RecordDamage(attacker);
            }   
        }
    }
}