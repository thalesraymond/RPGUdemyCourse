namespace PlayerStateMachine
{
    public class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            if (this.PlayerInputSet.Player.Jump.WasPerformedThisFrame())
            {
                return this.Player.JumpState;
            }

            // Avoid player falling (without jump input) in the wrong state
            // this changes to fall state if the player is falling (e.g. after walking off a ledge)
            if (this.RigidBody2D.linearVelocity.y < 0 && !this.Player.IsGrounded)
                return this.Player.FallState;
            
            if(this.PlayerInputSet.Player.Attack.WasPerformedThisFrame() && this.StateMachine.CurrentState != this.Player.BasicAttackState)
            {
                return this.Player.BasicAttackState;
            }

            return null;
        }
    }
}
