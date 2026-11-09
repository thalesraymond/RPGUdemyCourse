using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace PlayerStateMachine
{
    public class PlayerDashState : PlayerState
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
        
        public override EntityState Update()
        {
            var nextState = base.Update();
            if (nextState != null) return nextState;

            if (this.Player.IsTouchingWall)
                return this.Player.IsGrounded ? this.Player.IdleState : this.Player.WallSlideState;

            this.Player.SetVelocity(new Vector2(this._dashDirection * this.Player.dashSpeed, 0));

            if (this.StateTimer < 0)
                return this.Player.IsGrounded ? this.Player.IdleState : this.Player.FallState;

            Debug.Log(this.StateTimer);
            return null;
        }

        public override void Exit()
        {
            base.Exit();
            
            this.Player.SetVelocity(Vector2.zero);
            this.RigidBody2D.gravityScale = this._originalGravityScale;
        }
    }
}
