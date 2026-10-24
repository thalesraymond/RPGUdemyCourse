using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerWallSlideState : EntityState
    {
        public PlayerWallSlideState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
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
                this.Player.Flip();
            }

            if (!this.Player.IsTouchingWall) 
                this.StateMachine.ChangeState(this.Player.FallState);


            this.HandleWallSlide();
            this.HandleWallJump();
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Animator.SetBool(this.AnimationBoolName, false);
        }

        private void HandleWallJump()
        {
            if (!this.PlayerInputSet.Player.Jump.WasPressedThisFrame()) return;
            
            this.StateMachine.ChangeState(this.Player.WallJumpState);
        }
        
        private void HandleWallSlide()
        {
            if (this.Player.MovementInput.y < 0) 
                this.Player.SetVelocity(new Vector2(this.Player.MovementInput.x, this.RigidBody2D.linearVelocity.y));
            else
                this.RigidBody2D.linearVelocity =
                    new Vector2(this.Player.MovementInput.x, this.RigidBody2D.linearVelocity.y * this.Player.WallSlideMultiplier);
        }
    }
}