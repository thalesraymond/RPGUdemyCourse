using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    [Header("MoveInfo")]
    public float MoveSpeed;
    public float IdleTime;


    public EnemyStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        this.StateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();

        this.StateMachine.CurrentState.Update();
    }
}
