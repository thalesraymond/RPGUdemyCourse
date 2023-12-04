using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float MoveSpeed = 12f;
    public float JumpForce;

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerAirState AirState { get; private set; }
    #endregion

    #region Components
    public Animator Anim { get; private set; }

    public Rigidbody2D Rb {  get; private set; }

    #endregion


    private void Awake()
    {
        this.StateMachine = new PlayerStateMachine();

        this.IdleState = new PlayerIdleState(this, this.StateMachine, "Idle");

        this.MoveState = new PlayerMoveState(this, this.StateMachine, "Move");

        this.JumpState = new PlayerJumpState(this, this.StateMachine, "Jump");

        this.AirState = new PlayerAirState(this, this.StateMachine, "Jump");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Anim = this.GetComponentInChildren<Animator>();

        this.Rb = this.GetComponent<Rigidbody2D>();

        this.StateMachine.Initialize(this.IdleState); 
    }

    // Update is called once per frame
    void Update()
    {
        this.StateMachine.CurrentState.Update();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        this.Rb.velocity = new Vector2 (xVelocity, yVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(this.groundCheck.position, Vector2.down, groundCheckDistance, this.whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

    }
}
