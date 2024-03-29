using System.Collections;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone Info")]
    [SerializeField] private GameObject ClonePrefab;
    [SerializeField] private float cloneDurantion;
    [Space]
    [SerializeField] private bool canAttack;

    [SerializeField] private bool canCreateCloneOnDashStart;
    [SerializeField] private bool canCreateCloneOnDashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
    [SerializeField] private float cloneOnCounterAttackDelay;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float cloneDuplicationPercentageChance;

    [Header("Crystal instead of clone")]
    public bool canCreateCrystalInsteadOfClone;

    public void CreateClone(Transform clonePosition, Vector3? offset = null)
    {
        if (canCreateCrystalInsteadOfClone)
        {
            SkillManager.Instance.CrystalSkill.CreateCrystal();

            return;
        }

        var newClone = Instantiate(ClonePrefab, this.Player.transform.position, Quaternion.identity);

        if (offset == null)
            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDurantion, canAttack, this.FindClosestEnemy(newClone.transform), canDuplicateClone, cloneDuplicationPercentageChance);
        else
            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDurantion, canAttack, this.FindClosestEnemy(newClone.transform), canDuplicateClone, cloneDuplicationPercentageChance, offset.Value);
    }

    public void CreateCloneOnDashStart()
    {
        if (canCreateCloneOnDashStart)
            CreateClone(this.Player.transform);

    }

    public void CreateCloneOnDashOver()
    {
        if (canCreateCloneOnDashOver)
            CreateClone(this.Player.transform);
    }

    public void CreateCloneOnCounterAttack(Transform enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
            StartCoroutine(CreateCloneWithDelay(enemyTransform, new Vector3(1.5f * this.Player.FacingDirection, 0)));

    }

    private IEnumerator CreateCloneWithDelay(Transform transform, Vector3 offSet)
    {
        yield return new WaitForSeconds(cloneOnCounterAttackDelay);

        CreateClone(transform, offSet);
    }
}
