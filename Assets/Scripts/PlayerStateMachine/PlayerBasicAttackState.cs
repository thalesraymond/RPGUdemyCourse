using System.Collections;
using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerBasicAttackState : PlayerGroundedState
    {
        private const int FirstComboIndex = 1;
        private const int ComboLimit = 3;
        
        private static readonly int BasicAttackIndex = Animator.StringToHash("basicAttackIndex");
        private float _attackVelocityTimer;
        private int _comboIndex = FirstComboIndex;
        private float _lastAttackTime;
        private bool _comboAttackQueue;
        private Coroutine _chainComboCoroutine;
        private int _attackDirection;
        
        public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            if(this.Player.MovementInput.x != 0)
                this._attackDirection = (int)this.Player.MovementInput.x;
            else
                this._attackDirection = this.Player.FacingDirection;

            this._comboAttackQueue = false;       
            this.ResetComboIndexIfNeeded();
            this.ApplyAttackVelocity();
        }

        public override void Update()
        {
            base.Update();
            
            this.HandleAttackVelocity();

            if (this.PlayerInputSet.Player.Attack.WasPressedThisFrame()) 
                this.QueueNextAttack();

            if (this.TriggerCalled)
            {
                if (this._comboAttackQueue)
                {
                    this.Animator.SetBool(this.AnimationBoolName, false);
                    this.EnterAttackStateWithDelay();
                }
                else
                {
                    this.StateMachine.ChangeState(this.Player.IdleState);   
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            this._comboIndex++;
            
            this._lastAttackTime = Time.time;
        }

        private void QueueNextAttack()
        {
            if (this._comboIndex < ComboLimit)
            {
                this._comboAttackQueue = true;
            }
        }

        private void EnterAttackStateWithDelay()
        {
            if (this._chainComboCoroutine != null)
                this.Player.StopCoroutine(this._chainComboCoroutine);
            
            this._chainComboCoroutine = this.Player.StartCoroutine(this.EnterAttackStateWithDelayCoroutine());
        }

        private IEnumerator EnterAttackStateWithDelayCoroutine()
        {
            yield return new WaitForEndOfFrame();
            this.StateMachine.ChangeState(this.Player.BasicAttackState);
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
            
            this.Player.SetVelocity(new Vector2(
                this._attackDirection * this.Player.attackVelocity[this._comboIndex - 1].x,
                this.RigidBody2D.linearVelocity.y)
            );
        }
        
        private void ResetComboIndexIfNeeded()
        {
            if (this._comboIndex > ComboLimit) this._comboIndex = FirstComboIndex;
            if (Time.time > this._lastAttackTime + this.Player.comboResetTime)
                this._comboIndex = FirstComboIndex;


            this.Animator.SetInteger(BasicAttackIndex, this._comboIndex);
        }
    }
}