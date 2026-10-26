namespace PlayerStateMachine
{
    public class PlayerDashState : EntityState
    {
        public PlayerDashState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }
    }
}
