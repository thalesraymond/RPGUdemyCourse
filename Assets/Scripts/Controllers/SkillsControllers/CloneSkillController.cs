using System.Linq;
using UnityEngine;

public class CloneSkillController : SkillController
{

    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    public Transform AttackCheck;
    public float AttackCheckRadius;

    private Transform closestEnemyTransform;

    private bool canDuplicateClone;

    private float cloneDuplicationPercentageChance;

    private int facingDirection = 1;

    private void SetupClone(float cloneDurantion, bool canAttack, Transform closestEnemy, bool canDuplicateClone, float cloneDuplicationPercentageChance)
    {
        cloneTimer = cloneDurantion;

        if (canAttack)
        {
            animator.SetInteger("AttackNumber", Random.Range(1, 3));
        }

        this.closestEnemyTransform = closestEnemy;

        this.canDuplicateClone = canDuplicateClone;

        this.cloneDuplicationPercentageChance = cloneDuplicationPercentageChance;

        this.FaceClosestTarget();
    }

    public void SetupClone(Transform newTransform, float cloneDurantion, bool canAttack, Transform closestEnemy, bool canDuplicateClone, float cloneDuplicationPercentageChance)
    {
        transform.position = newTransform.position + new Vector3(0, -0.45f, 0);

        SetupClone(cloneDurantion, canAttack, closestEnemy, canDuplicateClone, cloneDuplicationPercentageChance);
    }

    public void SetupClone(Transform newTransform, float cloneDurantion, bool canAttack, Transform closestEnemy, bool canDuplicateClone, float cloneDuplicationPercentageChance, Vector3 offSet)
    {
        transform.position = newTransform.position + offSet;

        SetupClone(cloneDurantion, canAttack, closestEnemy, canDuplicateClone, cloneDuplicationPercentageChance);
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

            if (spriteRenderer.color.a <= 0)
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
        {
            this.Player.Stats.DoDamage(hit.GetComponent<CharacterStats>());

            if (this.canDuplicateClone)
            {
                if(Random.Range(0, 100) < this.cloneDuplicationPercentageChance)
                {
                    SkillManager.Instance.CloneSkill.CreateClone(hit.transform, new Vector3(1f * this.facingDirection, 0));
                }
            }
        }
            
    }

    private void FaceClosestTarget()
    {
        if (this.closestEnemyTransform != null)
        {
            if (transform.position.x > closestEnemyTransform.position.x)
            {
                this.facingDirection = -1;
                this.transform.Rotate(0, 180, 0);
            }
        }
    }
}
