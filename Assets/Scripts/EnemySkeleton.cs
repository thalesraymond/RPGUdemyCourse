using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States

    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();

        IdleState = new SkeletonIdleState(this, this.StateMachine, "Idle", this);

        MoveState = new SkeletonMoveState(this, this.StateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();

        this.StateMachine.Initialize(this.IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
