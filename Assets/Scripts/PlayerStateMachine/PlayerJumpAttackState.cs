using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerJumpAttackState : PlayerState
    {
        private static readonly int JumpAttackTrigger = Animator.StringToHash("jumpAttackTrigger");

        private bool _touchedGround;
        public PlayerJumpAttackState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this._touchedGround = false;
            
            var x = this.Player.jumpAttackVelocity.x * this.Player.FacingDirection;
            this.Player.SetVelocity(new Vector2(x, this.Player.jumpAttackVelocity.y));
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            if (this.Player.IsGrounded && !this._touchedGround)
            {
                this.Animator.SetTrigger(JumpAttackTrigger);
                this.Player.SetVelocity(new Vector2(0, this.RigidBody2D.linearVelocity.y));
                this._touchedGround = true;
            }

            if (this.TriggerCalled)
            {
                return this.Player.IdleState;
            }

            return null;
        }
    }
}