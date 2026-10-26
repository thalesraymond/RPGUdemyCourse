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
    protected bool IsCurrentState => this.StateMachine.CurrentState == this;

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
    }
    
    public virtual void Update()
    {   
        this.StateTimer -= Time.deltaTime;
        this.Animator.SetFloat(YVelocity, this.RigidBody2D.linearVelocity.y);

        if (this.PlayerInputSet.Player.Dash.WasPressedThisFrame() && this.CanDash()) 
            this.StateMachine.ChangeState(this.Player.DashState);
    }
    
    public virtual void Exit()
    {
        Debug.Log("Exiting " + this.AnimationBoolName);
        this.Animator.SetBool(this.AnimationBoolName, false);
    }

    protected void SetHorizontalVelocity(float velocity)
    {
        this.Player.SetVelocity(new Vector2(velocity, this.RigidBody2D.linearVelocity.y));
    }

    protected void TransitionToFallIfDescending()
    {
        if (this.RigidBody2D.linearVelocity.y < 0)
        {
            this.StateMachine.ChangeState(this.Player.FallState);
        }
    }

    private bool CanDash()
    {
        return this.StateMachine.CurrentState != this.Player.DashState && !this.Player.IsTouchingWall;
    }
}
