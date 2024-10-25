namespace Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (this.Player.IsWallDetected())
                this.StateMachine.ChangeState(this.Player.IdleState);

            this.Player.SetVelocity(xInput * this.Player.MoveSpeed, this.Rb.velocity.y);

            if (this.xInput == 0)
                this.StateMachine.ChangeState(this.Player.IdleState);
        }
    }
}
