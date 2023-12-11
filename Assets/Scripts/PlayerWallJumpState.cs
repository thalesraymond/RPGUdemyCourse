using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        this.StateTimer = .4f;

        this.Player.SetVelocity(5 * -this.Player.FacingDirection, this.Player.JumpForce);
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

        if(this.StateTimer < 0)
            this.StateMachine.ChangeState(this.Player.AirState);

        if(this.Player.IsGroundDetected())
            this.StateMachine.ChangeState(this.Player.IdleState);
    }
}
