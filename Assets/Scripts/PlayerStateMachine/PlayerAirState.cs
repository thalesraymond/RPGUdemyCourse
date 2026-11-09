namespace PlayerStateMachine
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            // make movement possible while on air
            this.SetHorizontalVelocity(
                this.Player.MovementInput.x * this.Player.MoveSpeed * this.Player.InAirMoveMultiplier
            );

            if (this.Player.IsTouchingWall && this.Player.MovementInput.x != 0 && !this.Player.IsGrounded) 
                return this.Player.WallSlideState;
            
            if (this.PlayerInputSet.Player.Attack.WasPressedThisFrame())
                return this.Player.JumpAttackState;

            return null;
        }
    }
}
