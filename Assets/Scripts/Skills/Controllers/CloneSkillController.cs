using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{

    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    public Transform AttackCheck;
    public float AttackCheckRadius;

    private Transform closestEnemyTransform;

    private void SetupClone(float cloneDurantion, bool canAttack)
    {
        cloneTimer = cloneDurantion;

        if (canAttack)
        {
            animator.SetInteger("AttackNumber", Random.Range(1, 3));
        }

        this.FaceClosestTarget();
    }

    public void SetupClone(Transform newTransform, float cloneDurantion, bool canAttack)
    {
        transform.position = newTransform.position + new Vector3(0, -0.45f, 0);

        SetupClone(cloneDurantion, canAttack);
    }

    public void SetupClone(Transform newTransform, float cloneDurantion, bool canAttack, Vector3 offSet)
    {
        transform.position = newTransform.position + offSet;

        SetupClone(cloneDurantion, canAttack);
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            this.spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime * colorLosingSpeed));

            if(spriteRenderer.color.a <= 0)
            {
                Destroy(gameObject);
            }            
        }
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        var colliders = Physics2D.OverlapCircleAll(AttackCheck.position, AttackCheckRadius)
            .Where(hit => hit.GetComponent<Enemy>() is not null);

        foreach (var hit in colliders)
            hit.GetComponent<Enemy>().Damage();
    }

    private void FaceClosestTarget()
    {
        var closestEnemy = Physics2D.OverlapCircleAll(transform.position, 25)
            .Where(hit => hit.GetComponent<Enemy>() is not null)
            .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
            .FirstOrDefault();

        if (closestEnemy != null)
        {
            this.closestEnemyTransform = closestEnemy.transform;

            if (transform.position.x > closestEnemyTransform.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
