using System;
using UnityEngine;

namespace PlayerStateMachine
{
    public class Player : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float inAirMoveMultiplier = 0.5f;
        [SerializeField] public float jumpForce = 10f;
        public float MoveSpeed => this.moveSpeed;
        public float InAirMoveMultiplier => this.inAirMoveMultiplier;
        public bool IsGrounded { get; private set; }
        
        
        [Header("Ground Detection")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckDistance = 0.1f;
        [SerializeField] private LayerMask groundLayer;
        
        public Rigidbody2D RigidBody2D { get; private set; }

        public Animator Animator { get; private set; }
        public PlayerInputSet Input { get; private set; }
        private StateMachine StateMachine { get; set; }
        
        #region Player States
        
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerFallState FallState { get; private set; }
        
        #endregion

        public Vector2 MovementInput { get; private set; }

        private void OnEnable()
        {
            this.Input.Enable();
            
            this.Input.Player.Movement.performed += ctx => this.MovementInput = ctx.ReadValue<Vector2>(); 
            this.Input.Player.Movement.canceled += _ => this.MovementInput = Vector2.zero;
        }
        
        private void OnDisable()
        {
            this.Input.Disable();
        }

        private void Awake()
        {
            this.Animator = this.GetComponentInChildren<Animator>();
            this.RigidBody2D = this.GetComponent<Rigidbody2D>();
            
            this.StateMachine = new StateMachine();
            this.Input = new PlayerInputSet();
        
            this.IdleState = new PlayerIdleState(this, this.StateMachine, "idle");
            this.MoveState = new PlayerMoveState(this, this.StateMachine, "move");
            this.JumpState = new PlayerJumpState(this, this.StateMachine, "jumpFall");
            this.FallState = new PlayerFallState(this, this.StateMachine, "jumpFall");
        }

        private void Start()
        {
            // Initialize the state machine with the IdleState
            this.StateMachine.Initialize(this.IdleState);
        }

        private void Update()
        {
            this.SetIsGrounded();
            
            // Update the current state
            this.StateMachine.UpdateActiveState();
        }

        private void SetIsGrounded()
        {
            var hit = Physics2D.Raycast(this.groundCheck.position, 
                                        Vector2.down, 
                                        this.groundCheckDistance, 
                                        this.groundLayer);
            
            this.IsGrounded = hit.collider is not null;
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
        
        private void OnDrawGizmosSelected()
        {
            if (this.groundCheck == null)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(this.groundCheck.position, 0.01f);
            Gizmos.DrawLine(
                this.groundCheck.position,
                this.groundCheck.position
                + Vector3.down * this.groundCheckDistance
            );
        }

    }
}
