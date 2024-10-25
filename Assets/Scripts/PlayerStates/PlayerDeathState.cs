namespace PlayerStates
{
    public class PlayerDeathState : PlayerState
    {
        public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();

            this.Player.SetVelocityToZero();
        }
    }
}
