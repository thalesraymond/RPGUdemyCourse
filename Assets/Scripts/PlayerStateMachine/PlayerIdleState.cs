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

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;
            
            if (Mathf.Approximately(this.Player.MovementInput.x, this.Player.FacingDirection)
                && this.Player.IsTouchingWall)
                return null;

            if (this.Player.MovementInput != Vector2.zero)
            {
                return this.Player.MoveState;
            }

            return null;
        }
    }
}
