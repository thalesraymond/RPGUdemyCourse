using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    private EnemySkeleton enemy;

    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemySkeleton) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemySkeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(this.enemy.MoveSpeed * enemy.FacingDirection, enemy.Rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            this.StateMachine.ChangeState(enemy.IdleState);
        }
    }
}
