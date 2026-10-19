using UnityEngine;

public class EntityState
{
    protected StateMachine StateMachine;
    protected string StateName;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.StateMachine = stateMachine;
        this.StateName = stateName;
    }
    
    public virtual void Enter()
    {
        Debug.Log("Entering " + this.StateName);
    }
    
    public virtual void Update()
    {   
        Debug.Log("Updating " + this.StateName);
    }
    
    public virtual void Exit()
    {
        Debug.Log("Exiting " + this.StateName);
    }
}
