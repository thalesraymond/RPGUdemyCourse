using System.Collections;
using Skills;
using UnityEngine;

namespace PlayerStates
{
    public class Player : Entity
    {

        [Header("Attack Details")]
        public float[] AttackMovement;
        public bool IsBusy { get; private set; }
        public float CounterAttackDuration = 0.2f;


        [Header("Move Info")]
        public float MoveSpeed = 12f;
        public float JumpForce;
        public float WallSlideDrag;
        public float SwordReturnImpact;
        private float _defaultMoveSpeed;
        private float _defaultJumpForce;

        [Header("dash info")]
        public float DashSpeed;
        public float DashDuration;
        public float DashDirection;
        private float _defaultDashSpeed;

        public SkillManager SkillManager { get; private set; }
        public GameObject Sword { get; private set; }

        #region States
        public PlayerStateMachine StateMachine { get; private set; }

        public PlayerIdleState IdleState { get; private set; }

        public PlayerMoveState MoveState { get; private set; }

        public PlayerJumpState JumpState { get; private set; }

        public PlayerAirState AirState { get; private set; }

        public PlayerWallSlideState WallSlideState { get; private set; }

        public PlayerDashState DashState { get; private set; }

        public PlayerWallJumpState WallJumpState { get; private set; }

        public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }

        public PlayerCounterAttackState CounterAttackState { get; private set; }

        public PlayerAimSwordState PlayerAimSwordState { get; private set; }

        public PlayerCatchSwordState PlayerCatchSwordState { get; private set; }

        public PlayerBlackholeState PlayerBlackholeState { get; private set; }

        public PlayerDeathState PlayerDeathState { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();

            this.StateMachine = new PlayerStateMachine();

            this.IdleState = new PlayerIdleState(this, this.StateMachine, "Idle");

            this.MoveState = new PlayerMoveState(this, this.StateMachine, "Move");

            this.JumpState = new PlayerJumpState(this, this.StateMachine, "Jump");

            this.AirState = new PlayerAirState(this, this.StateMachine, "Jump");

            this.WallSlideState = new PlayerWallSlideState(this, this.StateMachine, "WallSlide");

            this.DashState = new PlayerDashState(this, this.StateMachine, "Dash");

            this.WallJumpState = new PlayerWallJumpState(this, this.StateMachine, "Jump");

            this.PrimaryAttackState = new PlayerPrimaryAttackState(this, this.StateMachine, "Attack");

            this.CounterAttackState = new PlayerCounterAttackState(this, this.StateMachine, "CounterAttack");

            this.PlayerAimSwordState = new PlayerAimSwordState(this, this.StateMachine, "AimSword");

            this.PlayerCatchSwordState = new PlayerCatchSwordState(this, this.StateMachine, "CatchSword");

            this.PlayerBlackholeState = new PlayerBlackholeState(this, this.StateMachine, "Float");

            this.PlayerDeathState = new PlayerDeathState(this, this.StateMachine, "Death");
        }

        protected override void Start()
        {
            base.Start();

            this.StateMachine.Initialize(this.IdleState);

            this.SkillManager = SkillManager.Instance;

            this._defaultMoveSpeed = this.MoveSpeed;
            this._defaultJumpForce = this.JumpForce;
            this._defaultDashSpeed = this.DashSpeed;
        }

        protected override void Update()
        {
            base.Update();

            this.StateMachine.CurrentState.Update();

            if (Input.GetKeyDown(KeyCode.F))
            {
                this.SkillManager.CrystalSkill.CanUseSkill();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Inventory.Inventory.Instance.UseFlask();
            }
        }

        public override void SlowEntityBy(float slowPercentage, float slowDurantion)
        {
            this.MoveSpeed = this.MoveSpeed * (1 - slowPercentage);
            this.JumpForce = this.JumpForce * (1 - slowPercentage);
            this.DashSpeed = this.DashSpeed * (1 - slowPercentage);

            this.Anim.speed = this.Anim.speed * (1 - slowPercentage);

            Invoke(nameof(ReturnToDefaultSpeed), slowDurantion);
        }

        protected override void ReturnToDefaultSpeed()
        {
            this.MoveSpeed = this._defaultMoveSpeed;
            this.JumpForce = this._defaultJumpForce;
            this.DashSpeed = this._defaultDashSpeed;

            base.ReturnToDefaultSpeed();
        }


        public void AnimationTrigger() => this.StateMachine.CurrentState.AnimationFinishTrigger();

        public IEnumerator BusyFor(float seconds)
        {
            this.IsBusy = true;

            yield return new WaitForSeconds(seconds);

            this.IsBusy = false;
        }

        public void AssignNewSword(GameObject sword)
        {
            this.Sword = sword;
        }

        public void CatchTheSword()
        {
            this.StateMachine.ChangeState(PlayerCatchSwordState);

            Destroy(this.Sword);
        }

        public override void Die()
        {
            base.Die();

            this.StateMachine.ChangeState(PlayerDeathState);
        }
    }
}
