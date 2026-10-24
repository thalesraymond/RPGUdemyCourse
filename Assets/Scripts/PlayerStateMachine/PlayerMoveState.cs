

using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        private bool _isLeft => this.Player.MovementInput.x < 0;
        
        public PlayerMoveState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
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

            if (this.Player.MovementInput == Vector2.zero) this.StateMachine.ChangeState(this.Player.IdleState);
            
            // Flip the player based on movement direction
            this.Player.HandleRotate(this.Player.MovementInput);
            
            // Move the player based on input
            var newXVelocity = this.Player.MovementInput.x * this.Player.MoveSpeed;
            
            // Preserve the current Y velocity (for gravity, jumping, etc.)
            var newYVelocity = this.RigidBody2D.linearVelocity.y;
            
            this.Player.SetVelocity(new Vector2(newXVelocity, newYVelocity));
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Animator.SetBool(this.AnimationBoolName, false);
        }


    }
}