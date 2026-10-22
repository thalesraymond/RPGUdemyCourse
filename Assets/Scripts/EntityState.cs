using PlayerStateMachine;
using UnityEngine;

public abstract class EntityState
{   
    protected Player Player;
    protected StateMachine StateMachine;
    protected readonly string AnimationBoolName;

    protected Animator Animator => this.Player.Animator;
    protected Rigidbody2D RigidBody2D => this.Player.RigidBody2D;

    public EntityState(Player player, StateMachine stateMachine, string animationBoolName)
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
        Debug.Log("Updating " + this.AnimationBoolName);
    }
    
    public virtual void Exit()
    {
        Debug.Log("Exiting " + this.AnimationBoolName);
    }
}
