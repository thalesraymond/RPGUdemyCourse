using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int FacingDirection { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components
    public Animator Anim { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        this.Anim = this.GetComponentInChildren<Animator>();

        this.Rb = this.GetComponent<Rigidbody2D>();

    }

    protected virtual void Update()
    {

    }

    #region Collisions
    public virtual bool IsGroundDetected() => Physics2D.Raycast(this.groundCheck.position, Vector2.down, groundCheckDistance, this.whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(this.wallCheck.position, Vector2.right * this.FacingDirection, wallCheckDistance, this.whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

    }

    #endregion

    #region Flip

    public virtual void Flip()
    {
        FacingDirection = FacingDirection * -1;

        facingRight = !facingRight;

        this.transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float x)
    {
        //if (this.StateMachine.CurrentState is PlayerWallSlideState)
        //    return;

        if (x > 0 && !this.facingRight)
            this.Flip();
        else if (x < 0 && this.facingRight)
            this.Flip();
    }

    #endregion

    #region Velocity

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        this.Rb.velocity = new Vector2(xVelocity, yVelocity);

        this.FlipController(xVelocity);
    }

    public void SetVelocityToZero() => this.Rb.velocity = Vector2.zero;

    #endregion
}
