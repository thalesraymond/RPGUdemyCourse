using System;
using UnityEngine;

namespace PlayerStateMachine
{
    public class Player : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        public float MoveSpeed => this.moveSpeed;
        
        public Rigidbody2D RigidBody2D { get; private set; }

        public Animator Animator { get; private set; }
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
            this.Animator = this.GetComponentInChildren<Animator>();
            this.RigidBody2D = this.GetComponent<Rigidbody2D>();
            
            this.StateMachine = new StateMachine();
            this._input = new PlayerInputSet();
        
            this.IdleState = new PlayerIdleState(this, this.StateMachine, "idle");
            this.MoveState = new PlayerMoveState(this, this.StateMachine, "move");
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

        public void SetVelocity(Vector2 velocity)
        {
            this.RigidBody2D.linearVelocity = velocity;
            
            this.HandleRotate(this.RigidBody2D.linearVelocity);
        }
        
        public void HandleRotate(Vector2 direction)
        {
            if (direction.x < 0)
                this.transform.localScale = new Vector3(-1, 1, 1);
            else if (direction.x > 0) 
                this.transform.localScale = new Vector3(1, 1, 1);
        }

    }
}
