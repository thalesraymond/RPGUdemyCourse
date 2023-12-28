using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine StateMachine;

    protected Enemy EnemyBase;

    protected bool triggerCalled;

    private string animBoolName;

    protected float StateTimer;

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.EnemyBase = enemyBase;
        this.StateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        this.EnemyBase.Anim.SetBool(this.animBoolName, true);
    }

    public virtual void Exit()
    {
        this.EnemyBase.Anim.SetBool(this.animBoolName, false);
    }

    public virtual void Update()
    {
        this.StateTimer -= Time.deltaTime; 

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
