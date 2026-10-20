using System;
using UnityEngine;

namespace PlayerStateMachine
{
    public class Player : MonoBehaviour
    {
        private PlayerInputSet _input;
        private StateMachine StateMachine { get; set; }
        
        #region Player States
        
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        
        #endregion

        public Vector2 MovementInput { get; private set; }

        private void OnEnable()
        {
            this._input.Enable();
            
            this._input.Player.Movement.performed += ctx => this.MovementInput = ctx.ReadValue<Vector2>(); 
            this._input.Player.Movement.canceled += _ => this.MovementInput = Vector2.zero;
        }
        
        private void OnDisable()
        {
            this._input.Disable();
        }

        private void Awake()
        {
            this.StateMachine = new StateMachine();
            this._input = new PlayerInputSet();
        
            this.IdleState = new PlayerIdleState(this, this.StateMachine, "Idle");
            this.MoveState = new PlayerMoveState(this, this.StateMachine, "Move");
        }

        private void Start()
        {
            // Initialize the state machine with the IdleState
            this.StateMachine.Initialize(this.IdleState);
        }

        private void Update()
        {
            // Update the current state
            this.StateMachine.UpdateActiveState();
        }
    }
}
