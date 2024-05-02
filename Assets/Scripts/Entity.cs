using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Knockback info")]
    [SerializeField] protected Vector2 KnockbackDirection;
    [SerializeField] protected float KnockbackDuration;
    protected bool IsKnockback;

    [Header("Collision Info")]
    public Transform AttackCheck;
    public float AttackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public CharacterStats Stats { get; private set; }

    public int FacingDirection { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components
    public Animator Anim { get; private set; }

    public Rigidbody2D Rb { get; private set; }

    public EntityFX FX { get; private set; }

    public CapsuleCollider2D CapsuleCollider { get; private set; }

    public SpriteRenderer SpriteRenderer { get; private set; }

    public System.Action OnFlipped;

    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        this.Anim = this.GetComponentInChildren<Animator>();

        this.Rb = this.GetComponent<Rigidbody2D>();

        this.FX = GetComponent<EntityFX>();

        this.SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        this.Stats = GetComponent<CharacterStats>();

        this.CapsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    protected virtual void Update()
    {

    }

    public virtual void SlowEntityBy(float slowPercentage, float slowDurantion)
    {

    }

    protected virtual void ReturnToDefaultSpeed()
    {
        Anim.speed = 1;
    }

    public virtual void DamageEffect()
    {
        this.FX.StartCoroutine("FlashFx");

        StartCoroutine(HitKnockback());
    }

    protected virtual IEnumerator HitKnockback()
    {
        this.IsKnockback = true;

        Rb.velocity = new Vector2(KnockbackDirection.x * -FacingDirection, KnockbackDirection.y);

        yield return new WaitForSeconds(KnockbackDuration);

        this.IsKnockback = false;
    }

    #region Collisions
    public virtual bool IsGroundDetected() => Physics2D.Raycast(this.groundCheck.position, Vector2.down, groundCheckDistance, this.whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(this.wallCheck.position, Vector2.right * this.FacingDirection, wallCheckDistance, this.whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Gizmos.DrawWireSphere(this.AttackCheck.position, AttackCheckRadius);

    }

    #endregion

    #region Flip

    public virtual void Flip()
    {
        FacingDirection = FacingDirection * -1;

        facingRight = !facingRight;

        this.transform.Rotate(0, 180, 0);

        if (this.OnFlipped != null)
            this.OnFlipped();

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
        if (IsKnockback) return;

        this.Rb.velocity = new Vector2(xVelocity, yVelocity);

        this.FlipController(xVelocity);
    }

    public void SetVelocityToZero()
    {
        if (this.IsKnockback) return;

        this.Rb.velocity = Vector2.zero;
    }

    #endregion

    public void ToogleTransparent(bool transparent) => this.SpriteRenderer.color = transparent ? Color.clear : Color.white;

    public virtual void Die()
    { 
    }
}
