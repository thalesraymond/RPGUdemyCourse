using UnityEngine;

namespace Enemies
{
    public class EnemyMoveState : EnemyState
    {
        public EnemyMoveState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(enemyObject, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            
            if(nextState != null)
                return nextState;
            
            this.EnemyObject.SetVelocity(new Vector2(
                this.EnemyObject.moveSpeed * this.EnemyObject.FacingDirection, 
                this.RigidBody2D.linearVelocity.y)
            );

            if (!this.EnemyObject.IsGrounded || this.EnemyObject.IsTouchingWall)
            {
                this.EnemyObject.Flip();
                return this.EnemyObject.EnemyIdleState;
            }

            return null;
        }
    }
}