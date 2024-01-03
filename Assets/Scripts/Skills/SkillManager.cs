using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    public DashSkill DashSkill { get; private set; }

    public CloneSkill CloneSkill { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance.gameObject);

            return;
        }

        Instance = this;
    }

    private void Start()
    {
        this.DashSkill = GetComponent<DashSkill>();

        this.CloneSkill = GetComponent<CloneSkill>();
    }
}