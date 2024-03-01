using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField] private float _crystalDuration = .7f;
    [SerializeField] private GameObject _crystalPrefab;
    private GameObject _currentCrystal;

    [Header("Explosive Crystal")]
    [SerializeField] private bool _canExplode;

    [Header("Moving Crystal")]
    [SerializeField] private bool _canMoveToEnemy;
    [SerializeField] private float _moveSpeed;

    public override void UseSkill()
    {
        base.UseSkill();

        if(_currentCrystal == null)
        {
            _currentCrystal = Instantiate(_crystalPrefab, Player.transform.position, Quaternion.identity);

            var crystalSkillController = _currentCrystal.GetComponent<CrystalSkillController>();

            crystalSkillController.SetupCrystal(_crystalDuration, _canExplode, _canMoveToEnemy, _moveSpeed, this.FindClosestEnemy(this._currentCrystal.transform));
        }
        else
        {
            if (_canMoveToEnemy)
            {
                return;
            }

            var playerPosition = this.Player.transform.position;

            this.Player.transform.position = _currentCrystal.transform.position;

            this._currentCrystal.transform.position = playerPosition;

            Debug.Log("Destroy Crystal");

            this._currentCrystal.GetComponent<CrystalSkillController>().FinishCrystal();
        }
    }
}
