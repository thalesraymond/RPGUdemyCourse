using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    private int comboCounter;

    private float timeSinceLastAttack;
    private float comboWindow = 2;

    public override void Enter()
    {
        base.Enter();

        this.xInput = 0;

        if (comboCounter > 2 || Time.time >= timeSinceLastAttack + comboWindow)
            comboCounter = 0;

        this.Player.SetVelocity(this.Player.AttackMovement[comboCounter] * this.GetAttackDirection(), this.Rb.velocity.y);

        this.Player.Anim.SetInteger("ComboCounter", comboCounter);

        this.StateTimer = .1f;
    }

    private float GetAttackDirection()
    {
        if (xInput != 0)
            return xInput;

        return this.Player.FacingDirection;
    }

    public override void Exit()
    {
        base.Exit();

        this.Player.StartCoroutine(this.Player.BusyFor(.2f));

        comboCounter++;

        timeSinceLastAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (this.StateTimer < 0)
            this.Player.SetVelocityToZero();

        if (this.TriggerCalled)
            this.StateMachine.ChangeState(this.Player.IdleState);
    }
}
