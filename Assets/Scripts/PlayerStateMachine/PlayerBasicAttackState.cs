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
        private bool _isNextAttackQueued;
        private Coroutine _chainComboCoroutine;
        private int _attackDirection;

        public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animationBoolName) : base(player,
            stateMachine, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            this._attackDirection = this.Player.MovementInput.x != 0
                ? (int)Mathf.Sign(this.Player.MovementInput.x)
                : this.Player.FacingDirection;

            this._isNextAttackQueued = false;
            this.ResetComboIndexIfNeeded();
            this.Animator.SetInteger(BasicAttackIndex, this._comboIndex);
            this.ApplyAttackVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (!this.IsCurrentState) return;

            this.HandleAttackVelocity();

            if (this.PlayerInputSet.Player.Attack.WasPressedThisFrame())
                this.QueueNextAttack();

            if (!this.TriggerCalled) return;

            this.HandleStateExit();
        }

        public override void Exit()
        {
            this.CancelPendingAttack();
            base.Exit();

            this._comboIndex++;

            this._lastAttackTime = Time.time;
        }
        
        private void HandleStateExit()
        {
            if (!this._isNextAttackQueued)
            {
                this.StateMachine.ChangeState(this.Player.IdleState);
                return;
            }

            this.ScheduleNextAttack();
        }

        private void QueueNextAttack()
        {
            if (this._comboIndex < ComboLimit)
            {
                this._isNextAttackQueued = true;
            }
        }

        private void ScheduleNextAttack()
        {
            if (this._chainComboCoroutine != null) return;

            // Let the Animator evaluate the attack exit before starting the next combo step.
            this.Animator.SetBool(this.AnimationBoolName, false);
            this._chainComboCoroutine = this.Player.StartCoroutine(this.ContinueComboAtEndOfFrame());
        }

        private IEnumerator ContinueComboAtEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            this._chainComboCoroutine = null;
            if (!this.IsCurrentState) yield break;

            this.StateMachine.ChangeState(this.Player.BasicAttackState);
        }

        private void CancelPendingAttack()
        {
            if (this._chainComboCoroutine == null) return;

            this.Player.StopCoroutine(this._chainComboCoroutine);
            this._chainComboCoroutine = null;
        }

        private void HandleAttackVelocity()
        {
            this._attackVelocityTimer -= Time.deltaTime;

            if (this._attackVelocityTimer < 0)
            {
                this.SetHorizontalVelocity(0);
            }
        }

        private void ApplyAttackVelocity()
        {
            this._attackVelocityTimer = this.Player.attackVelocityDuration;

            this.SetHorizontalVelocity(
                this._attackDirection * this.Player.attackVelocity[this._comboIndex - FirstComboIndex].x);
        }

        private void ResetComboIndexIfNeeded()
        {
            if (this._comboIndex > ComboLimit || Time.time > this._lastAttackTime + this.Player.comboResetTime)
                this._comboIndex = FirstComboIndex;
        }
    }
}