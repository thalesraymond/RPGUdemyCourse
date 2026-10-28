using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerBasicAttackState : PlayerGroundedState
    {
        private static readonly int BasicAttackIndex = Animator.StringToHash("basicAttackIndex");
        private float _attackVelocityTimer;
        private const int FirstComboIndex = 1;
        private int _comboIndex = FirstComboIndex;
        private readonly int _comboLimit = 3;
        private float _lastAttackTime;
        public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            this.ResetComboIndexIfNeeded();
            this.ApplyAttackVelocity();
        }

        public override void Update()
        {
            base.Update();
            
            this.HandleAttackVelocity();

            if (this.TriggerCalled)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();

            this._comboIndex++;
            
            this._lastAttackTime = Time.time;
        }

        private void HandleAttackVelocity()
        {
            this._attackVelocityTimer -= Time.deltaTime;
            
            if (this._attackVelocityTimer < 0)
            {
                this.Player.SetVelocity(new Vector2(0, this.RigidBody2D.linearVelocity.y));
            }
        }

        private void ApplyAttackVelocity()
        {
            this._attackVelocityTimer = this.Player.attackVelocityDuration;
            
            this.Player.SetVelocity(this.Player.FacingDirection * this.Player.attackVelocity);
        }
        
        private void ResetComboIndexIfNeeded()
        {

            if (this._comboIndex > this._comboLimit) this._comboIndex = FirstComboIndex;
            if (Time.time > this._lastAttackTime + this.Player.comboResetTime)
                this._comboIndex = FirstComboIndex;


            this.Animator.SetInteger(BasicAttackIndex, this._comboIndex);
        }
    }
}