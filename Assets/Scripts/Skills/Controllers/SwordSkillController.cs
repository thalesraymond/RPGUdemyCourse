using System.Collections;
using System.Collections.Generic;
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
            


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("Rotation", false);

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
