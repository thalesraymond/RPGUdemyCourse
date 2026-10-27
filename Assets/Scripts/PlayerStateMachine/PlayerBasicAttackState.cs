using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerBasicAttackState : PlayerGroundedState
    {
        public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            
            this.Player.SetVelocity(Vector2.zero);

            if (this.TriggerCalled)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
            }
        }
    }
}