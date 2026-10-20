using PlayerStateMachine;
using UnityEngine;

public abstract class EntityState
{   
    protected Player Player;
    protected StateMachine StateMachine;
    protected readonly string StateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.Player = player;
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
