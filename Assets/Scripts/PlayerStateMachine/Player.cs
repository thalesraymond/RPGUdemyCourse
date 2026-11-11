using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace PlayerStateMachine
{
    public class Player : Entity
    {
        [Header("Movement Settings")]
        [SerializeField, Range(0f,1f)] private float inAirMoveMultiplier = 0.5f;
        [SerializeField] public float jumpForce = 10f;
        [SerializeField, Range(0f,1f)] private float wallSlideMultiplier = 0.5f;
        [SerializeField] public Vector2 wallJumpDirection;
        [SerializeField] public float dashDuration = 0.25f;
        [SerializeField] public float dashSpeed = 20f;
        
        [Header("Attack Settings")]
        public Vector2[] attackVelocity;
        public float attackVelocityDuration;
        public float comboResetTime = 1;
        public Vector2 jumpAttackVelocity;
        
        public float MoveSpeed => this.moveSpeed;
        public float InAirMoveMultiplier => this.inAirMoveMultiplier;
        public float WallSlideMultiplier => this.wallSlideMultiplier;
        public PlayerInputSet Input { get; private set; }
        
        #region Player States
        
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerFallState FallState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerBasicAttackState BasicAttackState { get; private set; }
        public PlayerJumpAttackState JumpAttackState { get; private set; }
        
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

        protected override void Awake()
        {
            base.Awake();
            
            
            this.Input = new PlayerInputSet();
        
            this.IdleState = new PlayerIdleState(this, this.StateMachine, "idle");
            this.MoveState = new PlayerMoveState(this, this.StateMachine, "move");
            this.JumpState = new PlayerJumpState(this, this.StateMachine, "jumpFall");
            this.FallState = new PlayerFallState(this, this.StateMachine, "jumpFall");
            this.WallSlideState = new PlayerWallSlideState(this, this.StateMachine, "wallSlide");
            this.WallJumpState = new PlayerWallJumpState(this, this.StateMachine, "jumpFall");
            this.DashState = new PlayerDashState(this, this.StateMachine, "dash");
            this.BasicAttackState = new PlayerBasicAttackState(this, this.StateMachine, "basicAttack");
            this.JumpAttackState = new PlayerJumpAttackState(this, this.StateMachine, "jumpAttack");
        }

        private void Start()
        {
            // Initialize the state machine with the IdleState
            this.StateMachine.Initialize(this.IdleState);
        }

        protected override void Update()
        {
            base.Update();
        }



        public void CallAnimationTrigger()
        {
            this.StateMachine.CurrentState.CallAnimationTrigger();
        }
    }
}
