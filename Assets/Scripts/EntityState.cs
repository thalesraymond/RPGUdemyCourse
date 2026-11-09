
using UnityEngine;

public class EntityState
{
    protected readonly StateMachine StateMachine;
    protected readonly string AnimationBoolName;
    
    protected Animator Animator { get; set; }
    protected Rigidbody2D RigidBody2D { get; set; }
    
    protected bool TriggerCalled { get; set; }
    
    protected float StateTimer { get; set; }

    protected EntityState(StateMachine stateMachine, string animationBoolName)
    {
        this.StateMachine = stateMachine;
        this.AnimationBoolName = animationBoolName;
    }
    
    public virtual void Enter()
    {
        Debug.Log("Entering " + this.AnimationBoolName);
        this.Animator.SetBool(this.AnimationBoolName, true);
        this.TriggerCalled = false;
    }
    
    public virtual EntityState Update()
    {
        Debug.Log(this.StateTimer);
        this.StateTimer -= Time.deltaTime;
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
}
