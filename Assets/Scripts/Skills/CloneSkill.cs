using System;
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

    public void CreateClone(Transform clonePosition, Vector3? offset = null)
    {
        var newClone = Instantiate(ClonePrefab);

        if(offset == null)
            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDurantion, canAttack);
        else
            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDurantion, canAttack, offset.Value);
    }
}
