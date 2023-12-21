using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : SkeletonGroundedState
{
    private Transform _player;

    private int _moveDirection;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        this._player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (this.Enemy.IsPlayerDetected() && this.Enemy.IsPlayerDetected().distance < this.Enemy.AttackDistance)
        {
            Debug.Log("IN ATTACK RANGE");
            this.Enemy.SetVelocityToZero();
            return;
        }

        this._moveDirection = this.IsPlayerToTheRight() ? 1 : -1;

        this.Enemy.SetVelocity(this._moveDirection * this.Enemy.MoveSpeed, this.Enemy.Rb.velocity.y);
    }

    private bool IsPlayerToTheRight()
    {
        if (this._player.position.x > this.Enemy.transform.position.x)
            return true;
        return false;
    }
}
