namespace PlayerStates
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();

            if (this.Player.IsWallDetected())
                this.StateMachine.ChangeState(this.Player.WallSlideState);

            if (this.Player.IsGroundDetected())
                this.StateMachine.ChangeState(this.Player.IdleState);

            if (this.xInput != 0)
                this.Player.SetVelocity(this.Player.MoveSpeed * .8f * this.xInput, this.Rb.linearVelocity.y);
        }
    }
}
