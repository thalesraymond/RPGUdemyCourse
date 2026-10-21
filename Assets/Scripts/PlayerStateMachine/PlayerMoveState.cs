

using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerMoveState : EntityState
    {
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

            if (this.Player.MovementInput == Vector2.zero)
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