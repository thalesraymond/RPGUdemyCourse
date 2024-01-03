using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] private GameObject ClonePrefab;

    public void CreateClone(Transform clonePosition)
    {
        var newClone = Instantiate(ClonePrefab);

        newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition);
    }
}
