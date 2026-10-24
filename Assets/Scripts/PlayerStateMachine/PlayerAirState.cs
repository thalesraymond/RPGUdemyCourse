using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerAirState : EntityState
    {
        public PlayerAirState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            
            // make movement possible while on air
            this.Player.SetVelocity(new Vector2(
                this.Player.MovementInput.x * this.Player.MoveSpeed * this.Player.InAirMoveMultiplier, 
                this.Player.RigidBody2D.linearVelocity.y)
            );
        }
    }
}