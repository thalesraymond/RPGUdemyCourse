using System.Linq;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] public float Cooldown;
    protected float CooldownTimer;
    protected Player Player;

    protected virtual void Start()
    {
        this.Player = PlayerManager.Instance.Player;
    }

    protected virtual void Update()
    {
        CooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (CooldownTimer < 0)
        {
            UseSkill();
            CooldownTimer = Cooldown;
            return true;
        }

        return false;
    }

    public virtual void UseSkill()
    {
        // TODO: Use Skill
    }

    protected virtual Transform FindClosestEnemy(Transform checkTransform)
    {
        var closestEnemy = Physics2D.OverlapCircleAll(checkTransform.position, 25)
                            .Where(hit => hit.GetComponent<Enemy>() is not null)
                            .OrderBy(hit => Vector2.Distance(checkTransform.position, hit.transform.position))
                            .FirstOrDefault();

        if (closestEnemy == null)
        {
            return this.Player.transform;
        }

        return closestEnemy.transform;
    }
}
