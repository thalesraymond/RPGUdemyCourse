using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField] private float _crystalDuration = .7f;
    [SerializeField] private GameObject _crystalPrefab;
    private GameObject _currentCrystal;

    public override void UseSkill()
    {
        base.UseSkill();

        if(_currentCrystal == null)
        {
            _currentCrystal = Instantiate(_crystalPrefab, Player.transform.position, Quaternion.identity);

            var crystalSkillController = _currentCrystal.GetComponent<CrystalSkillController>();

            crystalSkillController.SetupCrystal(_crystalDuration);
        }
        else
        {
            this.Player.transform.position = _currentCrystal.transform.position;
            Destroy(this._currentCrystal);
        }
    }
}
