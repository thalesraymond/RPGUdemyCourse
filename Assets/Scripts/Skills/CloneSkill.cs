using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone Info")]
    [SerializeField] private GameObject ClonePrefab;
    [SerializeField] private float cloneDurantion;
    [Space]
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform clonePosition)
    {
        var newClone = Instantiate(ClonePrefab);

        newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDurantion, canAttack);
    }
}
