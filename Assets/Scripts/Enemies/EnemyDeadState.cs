using UnityEngine;

namespace Enemies
{
    public class EnemyDeadState : EnemyState
    {
        public EnemyDeadState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(enemyObject, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.Animator.enabled = false; // disable animation to freeze enemy before death
            this.RigidBody2D.gravityScale = this.EnemyObject.deathGravityScale;
            this.RigidBody2D.linearVelocity = new Vector2(this.RigidBody2D.linearVelocity.x, this.EnemyObject.deathVelocityY);
            
            this.EnemyObject.GetComponent<Collider2D>().enabled = false;
            
            Object.Destroy(this.EnemyObject.gameObject, 4f);
        }
    }
}