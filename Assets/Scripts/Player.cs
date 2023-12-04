using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    private void Awake()
    {
        this.StateMachine = new PlayerStateMachine();

        this.IdleState = new PlayerIdleState(this, this.StateMachine, "Idle");

        this.MoveState = new PlayerMoveState(this, this.StateMachine, "Move");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.StateMachine.Initialize(this.IdleState); 
    }

    // Update is called once per frame
    void Update()
    {
        this.StateMachine.CurrentState.Update();
    }
}
