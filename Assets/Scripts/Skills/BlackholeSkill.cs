using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeSkill : Skill
{
    [SerializeField] private GameObject _blackHolePrefab;

    [SerializeField] private float _maxSize;
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _shrinkSpeed;
    [SerializeField] private int _amountOfAttacks;
    [SerializeField] private float _cloneAttackCooldown;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        var newBlackHole = Instantiate(this._blackHolePrefab, Player.transform.position, Quaternion.identity);

        var newBlackHoleController = newBlackHole.GetComponent<BlackHoleSkillController>();

        newBlackHoleController.SetupBlackHole(_maxSize, _growSpeed, _shrinkSpeed, _amountOfAttacks, _cloneAttackCooldown);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
