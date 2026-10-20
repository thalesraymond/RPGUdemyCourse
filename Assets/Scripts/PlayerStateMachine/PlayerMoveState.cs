

using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerMoveState : EntityState
    {
        public PlayerMoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
        {
        }

        public override void Update()
        {
            base.Update();

            if (this.Player.MovementInput == Vector2.zero)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
            }
        }
    }
}