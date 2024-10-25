using UnityEngine;

namespace Player
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.Rb.velocity = new Vector2(this.Rb.velocity.x, this.Player.JumpForce);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (this.Rb.velocity.y < 0)
                this.StateMachine.ChangeState(Player.AirState);
        }
    }
}
