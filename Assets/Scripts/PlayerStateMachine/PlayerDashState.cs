using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace PlayerStateMachine
{
    public class PlayerDashState : EntityState
    {
        private float _originalGravityScale;
        private int _dashDirection;

        public PlayerDashState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            // Define attack direction based of player movement input
            this._dashDirection = this.Player.MovementInput.x != 0
                ? (int)Mathf.Sign(this.Player.MovementInput.x)
                : this.Player.FacingDirection;

            this._originalGravityScale = this.RigidBody2D.gravityScale;
            
            this.RigidBody2D.gravityScale = 0;
            
            this.StateTimer = this.Player.dashDuration;
        }
        
        public override void Update()
        {
            base.Update();
            
            this.CancelDashWhenHitWall();
            
            this.Player.SetVelocity(new Vector2(this._dashDirection * this.Player.dashSpeed, 0));
            
            if (this.StateTimer >= 0) return;
            
            if (this.Player.IsGrounded)
                this.StateMachine.ChangeState(this.Player.IdleState);
            else
                this.StateMachine.ChangeState(this.Player.FallState);

        }

        public override void Exit()
        {
            base.Exit();
            
            this.Player.SetVelocity(Vector2.zero);
            this.RigidBody2D.gravityScale = this._originalGravityScale;
        }
        
        private void CancelDashWhenHitWall()
        {
            if (!this.Player.IsTouchingWall) return;
            
            if(this.Player.IsGrounded)
                this.StateMachine.ChangeState(this.Player.IdleState);
            else
                this.StateMachine.ChangeState(this.Player.WallSlideState);
        }
    }
}
