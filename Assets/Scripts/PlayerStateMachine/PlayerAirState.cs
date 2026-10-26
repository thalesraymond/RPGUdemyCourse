namespace PlayerStateMachine
{
    public class PlayerAirState : EntityState
    {
        public PlayerAirState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            // make movement possible while on air
            this.SetHorizontalVelocity(
                this.Player.MovementInput.x * this.Player.MoveSpeed * this.Player.InAirMoveMultiplier);

            if (this.Player.IsTouchingWall && this.Player.MovementInput.x != 0 && !this.Player.IsGrounded)
            {
                this.StateMachine.ChangeState(this.Player.WallSlideState);
            }
        }
    }
}
