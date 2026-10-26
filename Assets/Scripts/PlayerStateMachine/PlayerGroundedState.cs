namespace PlayerStateMachine
{
    public class PlayerGroundedState : EntityState
    {
        public PlayerGroundedState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (!this.IsCurrentState) return;

            if (this.PlayerInputSet.Player.Jump.WasPerformedThisFrame())
            {
                this.StateMachine.ChangeState(this.Player.JumpState);
                return;
            }

            // Avoid player falling (without jump input) in the wrong state
            // this changes to fall state if the player is falling (e.g. after walking off a ledge)
            if (this.RigidBody2D.linearVelocity.y < 0 && !this.Player.IsGrounded)
                this.StateMachine.ChangeState(this.Player.FallState);
        }
    }
}
