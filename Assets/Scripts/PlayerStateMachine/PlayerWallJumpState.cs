using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerWallJumpState : EntityState
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

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            if (this.PlayerInputSet.Player.Attack.WasPressedThisFrame())
            {
                this.StateMachine.ChangeState(this.Player.JumpAttackState);
            }

            this.TransitionToFallIfDescending();
        }
    }
}
