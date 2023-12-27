using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask WhatIsPlayer;

    [Header("MoveInfo")]
    public float MoveSpeed;
    public float IdleTime;

    [Header("AttackInfo")]
    public float AttackDistance;
    public float AttackCooldown;
    public float LastTimeAttack;

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

        if(this.IsPlayerDetected().collider != null)
            Debug.Log(this.IsPlayerDetected().collider.gameObject.name + "I See");
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(this.wallCheck.position, Vector2.right * FacingDirection, 50, this.WhatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.transform.position, new Vector3(transform.position.x + this.AttackDistance * this.FacingDirection, transform.position.y));
    }

    public virtual void AnimationFinishTrigger() => this.StateMachine.CurrentState.AnimationFinishTrigger();
}
