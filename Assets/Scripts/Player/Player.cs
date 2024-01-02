using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("dash info")]
    public float DashCoolDown;
    public float DashUsageTimer;
    public float DashSpeed;
    public float DashDuration;
    public float DashDirection;

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
    }

    protected override void Start()
    {
        base.Start();

        this.StateMachine.Initialize(this.IdleState); 
    }

    protected override void Update()
    {
        base.Update();

        this.StateMachine.CurrentState.Update();
    }
    

    public void AnimationTrigger() => this.StateMachine.CurrentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float seconds)
    {
        this.IsBusy = true;

        yield return new WaitForSeconds(seconds);

        this.IsBusy = false;
    }
}
