namespace PlayerStates
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this.Player.SetVelocityToZero();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (this.Player.IsBusy)
                return;

            if (this.xInput == this.Player.FacingDirection && this.Player.IsWallDetected())
                return;

            if (this.xInput != 0)
                this.StateMachine.ChangeState(this.Player.MoveState);
        }
    }
}
