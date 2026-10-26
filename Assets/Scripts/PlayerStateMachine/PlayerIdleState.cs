using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.SetHorizontalVelocity(0);
        }

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            if (this.Player.MovementInput != Vector2.zero)
            {
                this.StateMachine.ChangeState(this.Player.MoveState);
            }
        }
    }
}
