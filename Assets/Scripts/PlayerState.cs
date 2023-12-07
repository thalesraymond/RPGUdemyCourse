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

    protected Rigidbody2D Rb;

    protected float StateTimer;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.Player = player;

        this.StateMachine = stateMachine;

        this._animBoolName = animBoolName;

    }

    public virtual void Enter()
    {
        this.Player.Anim.SetBool(this._animBoolName, true);

        this.Rb = this.Player.Rb;
    }

    public virtual void Exit()
    {
        this.Player.Anim.SetBool(this._animBoolName, false);
    }

    public virtual void Update()
    {
        this.xInput = Input.GetAxisRaw("Horizontal");

        this.Player.Anim.SetFloat("yVelocity", this.Rb.velocity.y);

        this.StateTimer -= Time.deltaTime;
    }
}
