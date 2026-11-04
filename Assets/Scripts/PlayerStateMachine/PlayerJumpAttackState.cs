using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerJumpAttackState : EntityState
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

        public override void Update()
        {
            base.Update();

            if (!this.IsCurrentState)
                return;

            if (this.Player.IsGrounded && !this._touchedGround)
            {
                this.Animator.SetTrigger(JumpAttackTrigger);
                this.Player.SetVelocity(new Vector2(0, this.RigidBody2D.linearVelocity.y));
                this._touchedGround = true;
            }

            if (this.TriggerCalled)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
            }
        }
    }
}