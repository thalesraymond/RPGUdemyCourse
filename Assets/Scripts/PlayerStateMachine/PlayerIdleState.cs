using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.Animator.SetBool(this.AnimationBoolName, true);
            
            this.Player.SetVelocity(new Vector2(0, this.Player.RigidBody2D.linearVelocity.y));
        }

        public override void Update()
        {
            base.Update();
            
            if (this.Player.MovementInput != Vector2.zero)
            {
                this.StateMachine.ChangeState(this.Player.MoveState);
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            
            this.Animator.SetBool(this.AnimationBoolName, false);
        }
    }
}