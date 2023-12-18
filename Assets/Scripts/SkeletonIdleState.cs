using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    private EnemySkeleton enemy;

    public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemySkeleton) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemySkeleton;
    }

    public override void Enter()
    {
        base.Enter();

        this.StateTimer = this.enemy.IdleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(this.StateTimer < 0)
            this.StateMachine.ChangeState(this.enemy.MoveState);
    }


}
