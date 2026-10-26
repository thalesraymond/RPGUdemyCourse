namespace PlayerStateMachine
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            if (this.Player.IsGrounded)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Animator.SetBool(this.AnimationBoolName, false);
        }
    }
}
