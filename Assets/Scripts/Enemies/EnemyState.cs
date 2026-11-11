using UnityEngine;

namespace Enemies
{
    public class EnemyState : EntityState
    {
        private static readonly int MoveAnimSpeedMultiplier = Animator.StringToHash("moveAnimSpeedMultiplier");
        protected Enemy EnemyObject { get; set; }
        
        protected EnemyState(Enemy enemyObject, StateMachine stateMachine, string animationBoolName) : base(stateMachine, animationBoolName)
        {
            this.EnemyObject = enemyObject;
            this.RigidBody2D = enemyObject.RigidBody2D;
            this.Animator = enemyObject.Animator;
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;
            
            this.Animator.SetFloat(MoveAnimSpeedMultiplier, this.EnemyObject.moveAnimationMultiplier);

            return null;
        }
    }
}