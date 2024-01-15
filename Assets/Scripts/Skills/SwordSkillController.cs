using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Animator animator; 
    private Rigidbody2D rb;
    private CircleCollider2D cd;

    private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 direction, float gravityScale)
    {
        rb.velocity = direction;
        rb.gravityScale = gravityScale;
    }
}
