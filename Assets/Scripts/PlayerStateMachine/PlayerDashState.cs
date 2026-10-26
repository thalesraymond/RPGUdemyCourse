namespace PlayerStateMachine
{
    public class PlayerDashState : EntityState
    {
        public PlayerDashState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.Animator.SetBool(this.AnimationBoolName, true);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Animator.SetBool(this.AnimationBoolName, false);
        }
    }
}