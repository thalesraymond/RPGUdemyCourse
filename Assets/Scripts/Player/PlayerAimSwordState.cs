using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        this.Player.SkillManager.SwordSkill.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        Player.StartCoroutine(Player.BusyFor(.2f));
    }

    public override void Update()
    {
        base.Update();

        this.Player.SetVelocityToZero();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            this.StateMachine.ChangeState(this.Player.IdleState);

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Player.transform.position.x > mousePosition.x && Player.FacingDirection == 1)
            Player.Flip();  
        else if (Player.transform.position.x < mousePosition.x && Player.FacingDirection == -1)
            Player.Flip();
    }
}
