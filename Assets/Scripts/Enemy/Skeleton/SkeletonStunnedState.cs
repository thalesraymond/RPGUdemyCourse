using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private EnemySkeleton enemy;
    public SkeletonStunnedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        this.enemy.FX.InvokeRepeating("RedColorBlink", 0, 0.1f);

        this.StateTimer = enemy.StunDuration;

        this.enemy.Rb.velocity = new Vector2(-enemy.FacingDirection * enemy.StunDirection.x, enemy.StunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        this.enemy.FX.Invoke("CancelRedColorBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        if(this.StateTimer < 0)
            this.StateMachine.ChangeState(this.enemy.IdleState);
    }
}
