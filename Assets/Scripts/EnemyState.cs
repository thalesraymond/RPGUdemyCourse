using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine StateMachine;

    protected Enemy Enemy;

    protected bool triggerCalled;

    private string animBoolName;

    protected float StateTimer;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.Enemy = enemy;
        this.StateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        this.Enemy.Anim.SetBool(this.animBoolName, true);
    }

    public virtual void Exit()
    {
        this.Enemy.Anim.SetBool(this.animBoolName, false);
    }

    public virtual void Update()
    {
        this.StateTimer -= Time.deltaTime; 

    }

}
