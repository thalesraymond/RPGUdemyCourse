using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Mouse1) && this.HasNoSword())
            this.StateMachine.ChangeState(this.Player.PlayerAimSwordState);

        if (Input.GetKeyDown(KeyCode.Q))
            this.StateMachine.ChangeState(this.Player.CounterAttackState);

        if(Input.GetKeyDown(KeyCode.Mouse0))
            this.StateMachine.ChangeState(this.Player.PrimaryAttackState);

        if (!this.Player.IsGroundDetected())
        {
            this.StateMachine.ChangeState(this.Player.AirState);
            return;
        }        

        if (Input.GetKeyDown(KeyCode.Space) && this.Player.IsGroundDetected())
            this.StateMachine.ChangeState(this.Player.JumpState);
    }

    private bool HasNoSword()
    {
        if(!this.Player.Sword)
            return true;

        this.Player.Sword.GetComponent<SwordSkillController>().ReturnSword();

        return false;            
    }
}
