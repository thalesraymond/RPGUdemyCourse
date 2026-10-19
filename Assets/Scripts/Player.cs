using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    
    private EntityState _idleState;
    
    private void Awake()
    {
        this.StateMachine = new StateMachine();
        
        this._idleState = new EntityState(this.StateMachine, "Idle");
    }

    private void Start()
    {
        // Initialize the state machine with the IdleState
        this.StateMachine.Initialize(this._idleState);
    }

    private void Update()
    {
        // Update the current state
        this.StateMachine.CurrentState.Update();
    }
}
