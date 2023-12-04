using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine StateMachine;

    protected Player Player;

    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.Player = player;

        this.StateMachine = stateMachine;

        this._animBoolName = animBoolName;

    }

    public virtual void Enter()
    {
        Debug.Log("I enter" + this._animBoolName);
    }

    public virtual void Exit()
    {
        Debug.Log("I Exit" + this._animBoolName);
    }

    public virtual void Update()
    {
        Debug.Log("I Update" + this._animBoolName);
    }
}
