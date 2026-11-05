namespace PlayerStateMachine
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            if (this.Player.IsGrounded)
            {
                return this.Player.IdleState;
            }

            return null;
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Animator.SetBool(this.AnimationBoolName, false);
        }
    }
}
