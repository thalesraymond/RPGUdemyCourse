using PlayerStateMachine;
using UnityEngine;

public abstract class EntityState
{
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    protected readonly Player Player;
    protected readonly StateMachine StateMachine;
    protected readonly string AnimationBoolName;

    protected Animator Animator => this.Player.Animator;
    protected Rigidbody2D RigidBody2D => this.Player.RigidBody2D;
    
    protected PlayerInputSet PlayerInputSet => this.Player.Input;

    protected EntityState(Player player, StateMachine stateMachine, string animationBoolName)
    {
        this.Player = player;
        this.StateMachine = stateMachine;
        this.AnimationBoolName = animationBoolName;
    }
    
    public virtual void Enter()
    {
        Debug.Log("Entering " + this.AnimationBoolName);
    }
    
    public virtual void Update()
    {   
        this.Animator.SetFloat(YVelocity, this.RigidBody2D.linearVelocity.y);

        if (this.PlayerInputSet.Player.Dash.WasPressedThisFrame())
        {
            this.StateMachine.ChangeState(this.Player.DashState);
        }
    }
    
    public virtual void Exit()
    {
        Debug.Log("Exiting " + this.AnimationBoolName);
    }
}
