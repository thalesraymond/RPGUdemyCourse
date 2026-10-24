using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerJumpState : PlayerGroundedState
    {
        public PlayerJumpState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.Animator.SetBool(this.AnimationBoolName, true);
            
            this.Player.SetVelocity(new Vector2(this.Player.RigidBody2D.linearVelocity.x, this.Player.jumpForce));
            
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