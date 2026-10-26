using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        private bool _isLeft => this.Player.MovementInput.x < 0;

        public PlayerMoveState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            if (this.Player.MovementInput == Vector2.zero)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
                return;
            }

            this.SetHorizontalVelocity(this.Player.MovementInput.x * this.Player.MoveSpeed);
        }
    }
}
