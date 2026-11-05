using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        private bool _isLeft => this.Player.MovementInput.x < 0;

        public PlayerMoveState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            if (Mathf.Approximately(this.Player.MovementInput.x, this.Player.FacingDirection)
                && this.Player.IsTouchingWall || this.Player.MovementInput == Vector2.zero)
            {
                return this.Player.IdleState;
            }


            this.SetHorizontalVelocity(this.Player.MovementInput.x * this.Player.MoveSpeed);

            return null;
        }
    }
}
