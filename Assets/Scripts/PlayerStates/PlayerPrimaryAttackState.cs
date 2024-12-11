using Managers;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
        {
        }

        public int ComboCounter { get; private set; }

        private float _timeSinceLastAttack;
        private const float ComboWindow = 2;

        public override void Enter()
        {
            base.Enter();

            this.xInput = 0;

            if (ComboCounter > 2 || Time.time >= _timeSinceLastAttack + ComboWindow)
                ComboCounter = 0;

            this.Player.SetVelocity(this.Player.AttackMovement[ComboCounter] * this.GetAttackDirection(), this.Rb.linearVelocity.y);

            this.Player.Anim.SetInteger("ComboCounter", ComboCounter);

            this.StateTimer = .1f;
        }

        private float GetAttackDirection()
        {
            if (xInput != 0)
                return xInput;

            return this.Player.FacingDirection;
        }

        public override void Exit()
        {
            base.Exit();

            this.Player.StartCoroutine(this.Player.BusyFor(.2f));

            ComboCounter++;

            _timeSinceLastAttack = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (this.StateTimer < 0)
                this.Player.SetVelocityToZero();

            if (this.TriggerCalled)
                this.StateMachine.ChangeState(this.Player.IdleState);
        }
    }
}
