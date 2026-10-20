using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerIdleState : EntityState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
        {
        }

        public override void Update()
        {
            base.Update();
            
            if (this.Player.MovementInput != Vector2.zero)
            {
                this.StateMachine.ChangeState(this.Player.MoveState);
            }
        }
    }
}