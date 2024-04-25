using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField] private float _crystalDuration = .7f;
    [SerializeField] private GameObject _crystalPrefab;
    private GameObject _currentCrystal;

    [Header("Crystal Mirage")]
    [SerializeField] private bool _cloneInsteadOfCrystal;

    [Header("Explosive Crystal")]
    [SerializeField] private bool _canExplode;

    [Header("Moving Crystal")]
    [SerializeField] private bool _canMoveToEnemy;
    [SerializeField] private float _moveSpeed;

    [Header("Multi Stacking Crystal")]
    [SerializeField] private bool _canUseMultiStack;
    [SerializeField] private int _amountOfStacks;
    [SerializeField] private float _multiStackCooldown;
    [SerializeField] private float _useTimeWindow;
    private List<GameObject> _crystalLeft = new List<GameObject>();

    public override void UseSkill()
    {
        base.UseSkill();

        if (this.CanUseMultiCryustal())
            return;

        if (_currentCrystal == null)
        {
            CreateCrystal();
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

            if (this._cloneInsteadOfCrystal)
            {
                SkillManager.Instance.CloneSkill.CreateClone(this._currentCrystal.transform);

                Destroy(_currentCrystal);
            }
            else
            {
                this._currentCrystal.GetComponent<CrystalSkillController>().FinishCrystal();
            }
        }
    }

    public void CreateCrystal()
    {
        _currentCrystal = Instantiate(_crystalPrefab, Player.transform.position, Quaternion.identity);

        var crystalSkillController = _currentCrystal.GetComponent<CrystalSkillController>();

        crystalSkillController.SetupCrystal(_crystalDuration, _canExplode, _canMoveToEnemy, _moveSpeed, this.FindClosestEnemy(this._currentCrystal.transform));
    }

    public void CurrentCrystalChooseRandomTarget()
    {
        _currentCrystal.GetComponent<CrystalSkillController>().ChooseRandomEnemy();
    }

    private bool CanUseMultiCryustal()
    {
        if (this._canUseMultiStack)
        {
            if (_crystalLeft.Count <= 0)
                RefilCrystal();

            if (this._crystalLeft.Count > 0)
            {
                if (this._crystalLeft.Count == this._amountOfStacks)
                    Invoke(nameof(ResetHability), this._useTimeWindow);

                this.Cooldown = 0;

                var crystalToSpawn = this._crystalLeft[this._crystalLeft.Count - 1];

                var newCrystal = Instantiate(crystalToSpawn, Player.transform.position, Quaternion.identity);

                _crystalLeft.Remove(crystalToSpawn);

                newCrystal.GetComponent<CrystalSkillController>()
                    .SetupCrystal(_crystalDuration, _canExplode, _canMoveToEnemy, _moveSpeed, this.FindClosestEnemy(newCrystal.transform));

                if (this._crystalLeft.Count <= 0)
                {
                    this.Cooldown = this._multiStackCooldown;
                    RefilCrystal();
                }

                return true;
            }
        }

        return false;
    }

    private void RefilCrystal()
    {
        this._crystalLeft.Clear();

        for (int i = 0; i < _amountOfStacks; i++)
        {
            this._crystalLeft.Add(this._crystalPrefab);
        }
    }

    private void ResetHability()
    {
        if (CooldownTimer > 0)
            return;

        this.CooldownTimer = _multiStackCooldown;

        RefilCrystal();
    }
}
