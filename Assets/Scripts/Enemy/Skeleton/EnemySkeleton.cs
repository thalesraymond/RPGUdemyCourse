using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States

    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }
    public SkeletonBattleState BattleState { get; private set; }
    public SkeletonAttackState AttackState { get; private set; }
    public SkeletonStunnedState StunnedState { get; private set; }

    public SkeletonDeathState DeathState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();

        IdleState = new SkeletonIdleState(this, this.StateMachine, "Idle", this);

        MoveState = new SkeletonMoveState(this, this.StateMachine, "Move", this);

        BattleState = new SkeletonBattleState(this, this.StateMachine, "Move", this);

        AttackState = new SkeletonAttackState(this, this.StateMachine, "Attack", this);

        StunnedState = new SkeletonStunnedState(this, this.StateMachine, "Stunned", this);

        DeathState = new SkeletonDeathState(this, this.StateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();

        this.StateMachine.Initialize(this.IdleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U))
        {
            StateMachine.ChangeState(this.StunnedState);
        }
    }

    public override bool CheckCanBeStunned()
    {
        if (!base.CheckCanBeStunned())
            return false;

        this.StateMachine.ChangeState(this.StunnedState);

        return true;
    }

    public override void Die()
    {
        base.Die();

        this.StateMachine.ChangeState(this.DeathState);
    }
}
