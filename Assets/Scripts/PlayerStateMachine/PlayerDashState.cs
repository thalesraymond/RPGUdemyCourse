using System.Numerics;
using Vector2 = UnityEngine.Vector2;

namespace PlayerStateMachine
{
    public class PlayerDashState : EntityState
    {
        private float _originalGravityScale;
        public PlayerDashState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this._originalGravityScale = this.RigidBody2D.gravityScale;
            
            this.RigidBody2D.gravityScale = 0;
            
            this.StateTimer = this.Player.dashDuration;
        }
        
        public override void Update()
        {
            base.Update();
            
            this.CancelDashWhenHitWall();
            
            this.Player.SetVelocity(new Vector2(this.Player.FacingDirection * this.Player.dashSpeed, 0));
            
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
