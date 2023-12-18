using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine StateMachine;

    protected Player Player;

    private string _animBoolName;

    protected float xInput;

    protected float yInput;

    protected Rigidbody2D Rb;

    protected float StateTimer;

    protected bool TriggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.Player = player;

        this.StateMachine = stateMachine;

        this._animBoolName = animBoolName;

        this.TriggerCalled = false;

    }

    public virtual void Enter()
    {
        this.Player.Anim.SetBool(this._animBoolName, true);

        this.Rb = this.Player.Rb;

        this.TriggerCalled = false;
    }

    public virtual void Exit()
    {
        this.Player.Anim.SetBool(this._animBoolName, false);
    }

    public virtual void Update()
    {
        this.xInput = Input.GetAxisRaw("Horizontal");

        this.yInput = Input.GetAxisRaw("Vertical");

        this.Player.Anim.SetFloat("yVelocity", this.Rb.velocity.y);

        this.StateTimer -= Time.deltaTime;

        this.CheckForDashInput();
    }

    private void CheckForDashInput()
    {
        if (this.Player.IsWallDetected())
            return;

        this.Player.DashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && this.Player.DashUsageTimer < 0)
        {
            this.Player.DashUsageTimer = this.Player.DashCoolDown;

            this.Player.DashDirection = Input.GetAxisRaw("Horizontal");

            if (this.Player.DashDirection == 0)
                this.Player.DashDirection = this.Player.FacingDirection;

            this.StateMachine.ChangeState(this.Player.DashState);
        }
    }

    public virtual void AnimationFinishTrigger()
    {
        this.TriggerCalled = true;
    }
}
