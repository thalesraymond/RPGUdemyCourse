using UnityEngine;

namespace PlayerStates
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.StateMachine.ChangeState(this.Player.WallJumpState);
                return;
            }
            
            if(!this.Player.IsWallDetected())
                this.StateMachine.ChangeState(this.Player.AirState);
            
            if (this.xInput != 0 && this.Player.FacingDirection != this.xInput)
                this.StateMachine.ChangeState(this.Player.IdleState);

            if (this.Player.IsGroundDetected())
                this.StateMachine.ChangeState(this.Player.IdleState);

            this.Rb.linearVelocity = this.yInput < 0 ? new Vector2(0, Rb.linearVelocity.y) : new Vector2(0, Rb.linearVelocity.y * this.Player.WallSlideDrag);
        }
    }
}
