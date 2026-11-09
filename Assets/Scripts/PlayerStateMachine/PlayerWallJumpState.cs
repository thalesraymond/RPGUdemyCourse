using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            var awayFromWall = -this.Player.FacingDirection;

            this.Player.SetVelocity(new Vector2(
                this.Player.wallJumpDirection.x * awayFromWall, 
                this.Player.wallJumpDirection.y)
            );
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            if (this.PlayerInputSet.Player.Attack.WasPressedThisFrame())
            {
                return this.Player.JumpAttackState;
            }

            return this.RigidBody2D.linearVelocity.y < 0 ? this.Player.FallState : null;
        }
    }
}
