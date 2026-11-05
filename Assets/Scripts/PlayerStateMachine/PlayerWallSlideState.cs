using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerWallSlideState : EntityState
    {
        public PlayerWallSlideState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            if (this.Player.IsGrounded)
            {
                this.Player.Flip();
                return this.Player.IdleState;
            }

            if (!this.Player.IsBodyFullyTouchingWall)
            {
                return this.Player.FallState;
            }

            this.HandleWallSlide();
            return this.PlayerInputSet.Player.Jump.WasPressedThisFrame()
                ? this.Player.WallJumpState
                : null;
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
