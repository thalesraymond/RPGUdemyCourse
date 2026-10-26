using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerWallSlideState : EntityState
    {
        public PlayerWallSlideState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            if (this.Player.IsGrounded)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
                this.Player.Flip();
                return;
            }

            if (!this.Player.IsTouchingWall)
            {
                this.StateMachine.ChangeState(this.Player.FallState);
                return;
            }

            this.HandleWallSlide();
            this.HandleWallJump();
        }

        private void HandleWallJump()
        {
            if (!this.PlayerInputSet.Player.Jump.WasPressedThisFrame()) return;

            this.StateMachine.ChangeState(this.Player.WallJumpState);
        }

        private void HandleWallSlide()
        {
            if (this.Player.MovementInput.y < 0) 
                this.Player.SetVelocity(new Vector2(this.Player.MovementInput.x, this.RigidBody2D.linearVelocity.y));
            else
                this.RigidBody2D.linearVelocity =
                    new Vector2(this.Player.MovementInput.x, this.RigidBody2D.linearVelocity.y * this.Player.WallSlideMultiplier);
        }
    }
}
