using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if(this.xInput != 0 && this.Player.FacingDirection != this.xInput)
            this.StateMachine.ChangeState(this.Player.IdleState);

        if (this.Player.IsGroundDetected())
            this.StateMachine.ChangeState(this.Player.IdleState);

        if (this.yInput < 0)
            this.Rb.velocity = new Vector2(0, Rb.velocity.y);
        else
            this.Rb.velocity = new Vector2(0, Rb.velocity.y * this.Player.WallSlideDrag);
    }
}
