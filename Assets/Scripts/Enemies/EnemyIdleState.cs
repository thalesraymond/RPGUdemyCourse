using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class EnemyIdleState : EnemyGroundedState
    {
        public EnemyIdleState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(enemyObject, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.EnemyObject.SetVelocity(new Vector2(0, this.RigidBody2D.linearVelocity.y));
            
            this.StateTimer = this.EnemyObject.idleTime;
        }

        public override EntityState Update()
        {
            var nextState =  base.Update();
            
            if(nextState != null)
                return nextState;
            
            if (this.StateTimer < 0)
                return this.EnemyObject.EnemyMoveState;

            return null;
        }
    }
}