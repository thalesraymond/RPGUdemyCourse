using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask WhatIsPlayer;

    [Header("MoveInfo")]
    public float MoveSpeed;
    public float IdleTime;
    public float BattleTime;
    private float defaultMoveSpeed;

    [Header("AttackInfo")]
    public float AttackDistance;
    public float AttackCooldown;
    public float LastTimeAttack;

    [Header("Stunned Info")]
    public float StunDuration;
    public Vector2 StunDirection;
    protected bool CanBeStunned;
    [SerializeField] protected GameObject CounterImage;

    public EnemyStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        this.StateMachine = new EnemyStateMachine();

        defaultMoveSpeed = MoveSpeed;
    }

    protected override void Update()
    {
        base.Update();

        this.StateMachine.CurrentState.Update();
    }

    public virtual void FreezeTime(bool timeFrozen)
    {
        if (timeFrozen)
        {
            MoveSpeed = 0;
            Anim.speed = 0;
        }
        else
        {
            MoveSpeed = defaultMoveSpeed;
            Anim.speed = 1;
        }
    }

    protected virtual IEnumerator FreezeTimeFor(float seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(seconds);

        FreezeTime(false);
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(this.wallCheck.position, Vector2.right * FacingDirection, 30, this.WhatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.transform.position, new Vector3(transform.position.x + this.AttackDistance * this.FacingDirection, transform.position.y));
    }

    public virtual void AnimationFinishTrigger() => this.StateMachine.CurrentState.AnimationFinishTrigger();

    public virtual void OpenCounterAttackWindow()
    {
        this.CanBeStunned = true;
        this.CounterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        this.CanBeStunned = false;
        this.CounterImage.SetActive(false);
    }

    public virtual bool CheckCanBeStunned()
    {
        if (!this.CanBeStunned)
            return false;

        this.CloseCounterAttackWindow();

        return true;
    }
}
