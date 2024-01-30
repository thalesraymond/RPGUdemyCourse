using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Animator animator; 
    private Rigidbody2D rb;
    private CircleCollider2D cd;

    private Player player;

    private bool canRotate = true;
    private bool isReturning;

    private float returnSpeed = 12;

    private float freezeTimeDuration;

    [Header("Bounce Info")]
    private bool isBouncing;
    private int amountOfBounces;
    private List<Transform> enemyTargets;
    private int targetIndex;
    private float bounceSpeed = 10;

    [Header("Pierce Info")]
    [SerializeField] private int pierceAmount;

    [Header("Spin Info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSpinning;

    private float hitTimer;
    private float hitCooldown;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 direction, float gravityScale, Player player, float freezeTimeDurantion, float returnSpeed)
    {
        rb.velocity = direction;
        rb.gravityScale = gravityScale;

        this.returnSpeed = returnSpeed;

        this.player = player;

        this.freezeTimeDuration = freezeTimeDurantion;

        if (pierceAmount <= 0)
            animator.SetBool("Rotation", true);

        Invoke("DestroySword", 7);
    }

    private void DestroySword()
    {
        Destroy(gameObject);
    }

    public void SetupBounce(bool isBouncing, int amountOfBounces, float bounceSpeed)
    {
        this.isBouncing = isBouncing;
        this.amountOfBounces = amountOfBounces;
        this.bounceSpeed = bounceSpeed;

        this.enemyTargets = new List<Transform>();
    }

    public void SetupPierce(int pierceAmount)
    {
        this.pierceAmount = pierceAmount;
    }

    public void SetupSpin(bool isSpinning, float maxTravelDistance, float spinDuration, float hitCooldown)
    {
        this.isSpinning = isSpinning;
        this.maxTravelDistance = maxTravelDistance;
        this.spinDuration = spinDuration;
        this.hitCooldown = hitCooldown;
    }

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;

    }

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < .2f)
                player.CatchTheSword();
        }

        this.BounceLogic();
        this.SpinLogic();
    }

    private void SpinLogic()
    {
        if (!this.isSpinning)
            return;


        if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            this.StopWhenSpinning();

        if (!wasStopped)
            return;

        spinTimer -= Time.deltaTime;

        if (spinTimer < 0)
        {
            isReturning = true;
            isSpinning = false;
        }

        hitTimer -= Time.deltaTime;

        if (hitTimer < 0)
        {
            hitTimer = hitCooldown;

            var colliders = Physics2D.OverlapCircleAll(transform.position, 1);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() == null)
                    continue;

                this.DamageAndFreeze(hit.GetComponent<Enemy>());
            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (!isBouncing || enemyTargets.Count <= 0)
            return;

        transform.position = Vector2.MoveTowards(transform.position, enemyTargets[targetIndex].position, this.bounceSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, enemyTargets[targetIndex].position) >= .2f)
            return;

        var enemy = enemyTargets[targetIndex].GetComponent<Enemy>();

        this.DamageAndFreeze(enemy);

        targetIndex++;

        amountOfBounces--;

        if (amountOfBounces <= 0)
        {
            isBouncing = false;
            isReturning = true;
        }


        if (targetIndex >= enemyTargets.Count)
            targetIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        if(collision.GetComponent<Enemy>() != null)
        {
            var enemy = collision.GetComponent<Enemy>();
            this.DamageAndFreeze(enemy);
        }

        CheckRadiusForSwordEffect(collision);

        StuckInto(collision);
    }

    private void DamageAndFreeze(Enemy enemy)
    {
        enemy.Damage();

        enemy.StartCoroutine("FreezeTimeFor", this.freezeTimeDuration);
    }

    private void StuckInto(Collider2D collision)
    {
        if (isSpinning)
        {
            this.StopWhenSpinning();
            return;
        }

            if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        canRotate = false;

        cd.enabled = false;

        rb.isKinematic = true;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTargets.Count > 0)
            return;

        animator.SetBool("Rotation", false);

        transform.parent = collision.transform;
    }

    private void CheckRadiusForSwordEffect(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();

        if (enemy is null)
            return;

        enemy.Damage();

        if (!isBouncing || enemyTargets.Count > 0)
            return;

        var colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        enemyTargets.AddRange(colliders.Where(hit => hit.GetComponent<Enemy>() is not null).Select(hit => hit.transform));
    }
}
