using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float _flyTime = 0.4f;
    private bool _skillUsed;

    private float _defaultGravityScale;

    public PlayerBlackholeState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        this._skillUsed = false;

        StateTimer = _flyTime;

        this._defaultGravityScale = Player.Rb.gravityScale;

        this.Rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        Player.Rb.gravityScale = this._defaultGravityScale;

        this.Player.ToogleTransparent(false);
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer > 0)
            Rb.velocity = new Vector2(0, 15);

        if (StateTimer < 0)
        {
            this.Rb.velocity = new Vector2(0, -.1f);

            if (!_skillUsed)
            {
                if (this.Player.SkillManager.BlackholeSkill.CanUseSkill())
                    _skillUsed = true;

                this._skillUsed = true;
            }
        }

        if (this.Player.SkillManager.BlackholeSkill.SkillCompleted())
        {
            this.StateMachine.ChangeState(this.Player.AirState);
        }


    }
}
