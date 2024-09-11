using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalSkill : Skill
{
    [SerializeField] private float _crystalDuration = .7f;
    [SerializeField] private GameObject _crystalPrefab;
    private GameObject _currentCrystal;


    [Header("Simple Crystal")]
    [SerializeField] private SkillTreeSlotUI _simpleCrystalSlotUnlockButton;
    public bool SimpleCrystalUnlocked;


    [Header("Crystal Mirage")]
    [SerializeField] private SkillTreeSlotUI _crystalMirageSlotUnlockButton;
    [SerializeField] public bool CrystalMirageUnlocked;

    [Header("Explosive Crystal")]
    [SerializeField] private SkillTreeSlotUI _explosiveCrystalSlotUnlockButton;
    [SerializeField] private bool ExplosiveCrystalUnlocked;

    [Header("Moving Crystal")]
    [SerializeField] private SkillTreeSlotUI _movingCrystalSlotUnlockButton;
    [SerializeField] private bool MovingCrystalUnlocked;
    [SerializeField] private float _moveSpeed;

    [Header("Multi Stacking Crystal")]
    [SerializeField] private SkillTreeSlotUI _multiStackingCrystalSlotUnlockButton;
    [SerializeField] private bool MultiStackingCrystalUnlocked;
    [SerializeField] private int _amountOfStacks;
    [SerializeField] private float _multiStackCooldown;
    [SerializeField] private float _useTimeWindow;

    private List<GameObject> _crystalLeft = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        this._simpleCrystalSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSimpleCrystal);
        this._crystalMirageSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalMirage);
        this._explosiveCrystalSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockExplosiveCrystal);
        this._movingCrystalSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMovingCrystal);
        this._multiStackingCrystalSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultiStackingCrystal);
    }

    public override bool CanUseSkill()
    {
        if (!this.SimpleCrystalUnlocked) return false;

        return base.CanUseSkill();
    }

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
            if (MovingCrystalUnlocked)
            {
                return;
            }

            var playerPosition = this.Player.transform.position;

            this.Player.transform.position = _currentCrystal.transform.position;

            this._currentCrystal.transform.position = playerPosition;

            if (this.CrystalMirageUnlocked)
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

    private void UnlockSimpleCrystal()
    {
        if (this._simpleCrystalSlotUnlockButton.Unlocked)
            this.SimpleCrystalUnlocked = true;
    }

    private void UnlockCrystalMirage()
    {
        if (this._crystalMirageSlotUnlockButton.Unlocked)
            this.CrystalMirageUnlocked = true;
    }

    private void UnlockExplosiveCrystal()
    {
        if (this._explosiveCrystalSlotUnlockButton.Unlocked)
            this.ExplosiveCrystalUnlocked = true;
    }

    private void UnlockMovingCrystal()
    {
        if (this._movingCrystalSlotUnlockButton.Unlocked)
            this.MovingCrystalUnlocked = true;
    }

    private void UnlockMultiStackingCrystal()
    {
        if (this._multiStackingCrystalSlotUnlockButton.Unlocked)
            this.MultiStackingCrystalUnlocked = true;
    }

    public void CreateCrystal()
    {
        _currentCrystal = Instantiate(_crystalPrefab, Player.transform.position, Quaternion.identity);

        var crystalSkillController = _currentCrystal.GetComponent<CrystalSkillController>();

        crystalSkillController.SetupCrystal(_crystalDuration, ExplosiveCrystalUnlocked, MovingCrystalUnlocked, _moveSpeed, this.FindClosestEnemy(this._currentCrystal.transform));
    }

    public void CurrentCrystalChooseRandomTarget()
    {
        _currentCrystal.GetComponent<CrystalSkillController>().ChooseRandomEnemy();
    }

    private bool CanUseMultiCryustal()
    {
        if (this.MultiStackingCrystalUnlocked)
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
                    .SetupCrystal(_crystalDuration, ExplosiveCrystalUnlocked, MovingCrystalUnlocked, _moveSpeed, this.FindClosestEnemy(newCrystal.transform));

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
