using PlayerStateMachine;
using UnityEngine;

public abstract class EntityState
{
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    protected readonly Player Player;
    protected readonly StateMachine StateMachine;
    protected readonly string AnimationBoolName;
    protected float StateTimer;

    protected Animator Animator => this.Player.Animator;
    protected Rigidbody2D RigidBody2D => this.Player.RigidBody2D;
    
    protected PlayerInputSet PlayerInputSet => this.Player.Input;
    
    protected bool TriggerCalled { get; private set; }

    protected EntityState(Player player, StateMachine stateMachine, string animationBoolName)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
        this.AnimationBoolName = animationBoolName;
    }
    
    public virtual void Enter()
    {
        Debug.Log("Entering " + this.AnimationBoolName);
        this.Animator.SetBool(this.AnimationBoolName, true);
        this.TriggerCalled = false;
    }
    
    // Return null to stay active, or return the first eligible next state immediately.
    public virtual EntityState Update()
    {   
        this.StateTimer -= Time.deltaTime;
        this.Animator.SetFloat(YVelocity, this.RigidBody2D.linearVelocity.y);

        if (this.PlayerInputSet.Player.Dash.WasPressedThisFrame() && this.CanDash()) 
            return this.Player.DashState;

        return null;
    }
    
    public virtual void Exit()
    {
        Debug.Log("Exiting " + this.AnimationBoolName);
        this.Animator.SetBool(this.AnimationBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        this.TriggerCalled = true;
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
