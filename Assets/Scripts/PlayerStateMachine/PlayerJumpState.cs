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

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            return this.RigidBody2D.linearVelocity.y < 0 ? this.Player.FallState : null;
        }
    }
}
