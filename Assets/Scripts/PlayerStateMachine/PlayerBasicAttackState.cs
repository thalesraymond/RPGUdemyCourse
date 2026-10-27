using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerBasicAttackState : PlayerGroundedState
    {
        private float _attackVelocityTimer;
        public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.GenerateAttackVelocity();
        }

        public override void Update()
        {
            base.Update();
            
            this.HandleAttackVelocity();

            if (this.TriggerCalled)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
            }
        }

        private void HandleAttackVelocity()
        {
            this._attackVelocityTimer -= Time.deltaTime;
            
            if (this._attackVelocityTimer < 0)
            {
                this.Player.SetVelocity(new Vector2(0, this.RigidBody2D.linearVelocity.y));
            }
        }

        private void GenerateAttackVelocity()
        {
            this._attackVelocityTimer = this.Player.attackVelocityDuration;
            
            this.Player.SetVelocity(this.Player.FacingDirection * this.Player.attackVelocity);
        }
    }
}