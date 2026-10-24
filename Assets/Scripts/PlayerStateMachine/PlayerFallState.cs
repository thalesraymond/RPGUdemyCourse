namespace PlayerStateMachine
{
    public class PlayerFallState : EntityState
    {
        public PlayerFallState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
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