using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerWallJumpState : EntityState
    {
        public PlayerWallJumpState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.Animator.SetBool(this.AnimationBoolName, true);
            
            var awayFromWall = -this.Player.FacingDirection;
            
            this.Player.SetVelocity(new Vector2(
                this.Player.wallJumpDirection.x * awayFromWall, 
                this.Player.wallJumpDirection.y)
            );
            
        }

        public override void Update()
        {
            base.Update();
            
            if (this.Player.RigidBody2D.linearVelocity.y < 0)
            {
                this.StateMachine.ChangeState(this.Player.FallState);
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            
            this.Animator.SetBool(this.AnimationBoolName, false);
        }
    }
}