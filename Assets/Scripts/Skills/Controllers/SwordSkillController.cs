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

    [SerializeField] private float returnSpeed = 12;

    public bool isBouncing = true;
    public int amountOfBounces = 4;
    public List<Transform> enemyTargets = new List<Transform>();
    private int targetIndex;
    public float bounceSpeed = 10;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 direction, float gravityScale, Player player)
    {
        rb.velocity = direction;
        rb.gravityScale = gravityScale;

        this.player = player;

        animator.SetBool("Rotation", true);
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
        if(canRotate)
            transform.right = rb.velocity;

        if(isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.returnSpeed * Time.deltaTime);

            if(Vector2.Distance(transform.position, player.transform.position) < .2f)
                player.CatchTheSword();
        }

        if(isBouncing && enemyTargets.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTargets[targetIndex].position, this.bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTargets[targetIndex].position) < .2f)
            {
                targetIndex++;

                amountOfBounces--;

                if(amountOfBounces <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                    

                if (targetIndex >= enemyTargets.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        CheckRadiusForBounceSword(collision);

        StuckInto(collision);
    }

    private void StuckInto(Collider2D collision)
    {
        canRotate = false;

        cd.enabled = false;

        rb.isKinematic = true;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTargets.Count > 0)
            return;

        animator.SetBool("Rotation", false);

        transform.parent = collision.transform;
    }

    private void CheckRadiusForBounceSword(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() is null)
            return;

        if (!isBouncing || enemyTargets.Count > 0)
            return;

        var colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        enemyTargets.AddRange(colliders.Where(hit => hit.GetComponent<Enemy>() is not null).Select(hit => hit.transform));
    }
}
