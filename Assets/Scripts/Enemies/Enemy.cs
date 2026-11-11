using UnityEngine;

namespace Enemies
{
    public class Enemy : Entity
    {
        [Header("Enemy Movement Settings")]
        public float idleTime;
        [Range(0,2)]
        public float moveAnimationMultiplier;
        
        public EnemyIdleState EnemyIdleState { get; private set; }
        public EnemyMoveState EnemyMoveState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            this.EnemyIdleState = new EnemyIdleState(this, this.StateMachine, "idle");
            this.EnemyMoveState = new EnemyMoveState(this, this.StateMachine, "move");
        }
    }
}
