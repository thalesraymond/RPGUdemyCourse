using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerJumpState : PlayerAirState
    {
        public PlayerJumpState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.Player.SetVelocity(new Vector2(this.Player.RigidBody2D.linearVelocity.x, this.Player.jumpForce));
        }

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            this.TransitionToFallIfDescending();
        }
    }
}
