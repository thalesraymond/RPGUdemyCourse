using UnityEngine;

namespace Enemies
{
    public class EnemyState : EntityState
    {
        private static readonly int MoveAnimSpeedMultiplier = Animator.StringToHash("moveAnimSpeedMultiplier");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        private static readonly int BattleAnimSpeedMultiplier = Animator.StringToHash("battleAnimSpeedMultiplier");

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
            
            if (nextState != null) 
                return nextState;
            
            if (this.EnemyObject.HealthComponent.IsDead && this.StateMachine.CurrentState != this.EnemyObject.DeadState) 
                return this.EnemyObject.DeadState;
            
            return null;
        }

        public override void UpdateAnimationParameters()
        {
            base.UpdateAnimationParameters();
            
            this.Animator.SetFloat(MoveAnimSpeedMultiplier, this.EnemyObject.moveAnimationMultiplier);
            this.Animator.SetFloat(BattleAnimSpeedMultiplier, this.EnemyObject.BattleMoveAnimationMultiplier);
            this.Animator.SetFloat(XVelocity, this.RigidBody2D.linearVelocity.x);
        }
    }
}