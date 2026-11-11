using System;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected StateMachine StateMachine { get; set; }
    
    [Header("Base Movement Settings")]
    [SerializeField] public float moveSpeed = 5f;
    
    [Header("Base Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
        
    [Header("Base Wall Detection")]
    [SerializeField] private float wallCheckDistance = 0.1f;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool IsBodyFullyTouchingWall { get; private set; }
    public bool IsTouchingWall { get; private set; }
    
    public int FacingDirection => this.transform.localScale.x > 0 ? 1 : -1;
    
    public Rigidbody2D RigidBody2D { get; private set; }
    public Animator Animator { get; private set; }
    

    public bool IsGrounded { get; private set; }
    
    protected virtual void Awake()
    {
        this.StateMachine = new StateMachine();
        this.Animator = this.GetComponentInChildren<Animator>();
        this.RigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        this.SetIsGrounded();
        this.SetIsTouchingWall();
        
        // Update the current state
        this.StateMachine.UpdateActiveState();
    }

    protected virtual void Start()
    {
        // Base 
    }

    private void SetIsGrounded()
    {
        var hit = Physics2D.Raycast(this.groundCheck.position, 
            Vector2.down, 
            this.groundCheckDistance, 
            this.groundLayer);
            
        this.IsGrounded = hit.collider;
    }
    
    private void SetIsTouchingWall()
    {
        var direction = this.FacingDirection > 0 ? Vector2.right : Vector2.left;
            
        var hit = Physics2D.Raycast(this.primaryWallCheck.position, 
            direction * this.wallCheckDistance, 
            this.wallCheckDistance, 
            this.groundLayer);
            
        var hitSecondary = Physics2D.Raycast(this.secondaryWallCheck.position, 
            direction * this.wallCheckDistance, 
            this.wallCheckDistance, 
            this.groundLayer);
            
        this.IsTouchingWall = hit.collider is not null || hitSecondary.collider is not null;
            
        this.IsBodyFullyTouchingWall = hit.collider is not null && hitSecondary.collider is not null;
    }
    
    public void SetVelocity(Vector2 velocity)
    {
        this.RigidBody2D.linearVelocity = velocity;
            
        this.HandleRotate(this.RigidBody2D.linearVelocity);
    }

    private void HandleRotate(Vector2 direction)
    {
        var localScale = this.transform.localScale;

        if (direction.x < 0)
            localScale.x = -Mathf.Abs(localScale.x);
        else if (direction.x > 0)
            localScale.x = Mathf.Abs(localScale.x);

        this.transform.localScale = localScale;
    }

    public void Flip()
    {
        var localScale = this.transform.localScale;
        localScale.x *= -1;
        this.transform.localScale = localScale;
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
            
        if (this.primaryWallCheck == null || this.secondaryWallCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.primaryWallCheck.position, 0.01f);
        Gizmos.DrawLine(
            this.primaryWallCheck.position,
            this.primaryWallCheck.position
            + Vector3.right * this.wallCheckDistance
        );
            
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.secondaryWallCheck.position, 0.01f);
        Gizmos.DrawLine(
            this.secondaryWallCheck.position,
            this.secondaryWallCheck.position
            + Vector3.right * this.wallCheckDistance
        );
    }
    
    

}
