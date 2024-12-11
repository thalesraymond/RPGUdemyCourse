using UnityEngine;

namespace PlayerStates
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.Rb.linearVelocity = new Vector2(this.Rb.linearVelocity.x, this.Player.JumpForce);
        }

        public override void Update()
        {
            base.Update();

            if (this.Rb.linearVelocity.y < 0)
                this.StateMachine.ChangeState(Player.AirState);
        }
    }
}
