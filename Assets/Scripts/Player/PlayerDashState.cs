public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        this.StateTimer = this.Player.DashDuration;

        this.Player.SkillManager.CloneSkill.CreateClone(this.Player.transform);
    }

    public override void Exit()
    {
        base.Exit();

        this.Player.SetVelocity(0, this.Rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!this.Player.IsGroundDetected() && this.Player.IsWallDetected())
            this.StateMachine.ChangeState(this.Player.WallSlideState);

        this.Player.SetVelocity(this.Player.DashSpeed * this.Player.DashDirection, 0);

        if(this.StateTimer < 0)
        {
            this.StateMachine.ChangeState(this.Player.IdleState);
        }
    }
}
