namespace PlayerStateMachine
{
    public class PlayerGroundedState : EntityState
    {
        public PlayerGroundedState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            if (this.PlayerInputSet.Player.Jump.WasPerformedThisFrame())
            {
                this.StateMachine.ChangeState(this.Player.JumpState);
            }
        }
    }
}