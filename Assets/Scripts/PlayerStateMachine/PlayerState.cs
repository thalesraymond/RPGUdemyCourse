using PlayerStateMachine;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    protected readonly Player Player;
    protected PlayerInputSet PlayerInputSet => this.Player.Input;
    
    protected PlayerState(Player player, StateMachine stateMachine, string animationBoolName) : base(stateMachine, animationBoolName)
    {
        this.Player = player;
        this.Animator = this.Player.Animator;
        this.RigidBody2D = this.Player.RigidBody2D; 
    }
    
    public override void Enter()
    {
        base.Enter();
    }
    
    // Return null to stay active or return the first eligible next state immediately.
    public override EntityState Update()
    {
        var next = base.Update();
        if (next != null) return next;

        this.Animator.SetFloat(YVelocity, this.RigidBody2D.linearVelocity.y);

        if (this.PlayerInputSet.Player.Dash.WasPressedThisFrame() && this.CanDash()) 
            return this.Player.DashState;

        return null;
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    protected void SetHorizontalVelocity(float velocity)
    {
        this.Player.SetVelocity(new Vector2(velocity, this.RigidBody2D.linearVelocity.y));
    }

    private bool CanDash()
    {
        return this.StateMachine.CurrentState != this.Player.DashState && !this.Player.IsTouchingWall;
    }
}
