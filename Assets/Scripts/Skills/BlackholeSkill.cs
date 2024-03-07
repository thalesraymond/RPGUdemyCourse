using UnityEngine;

public class BlackholeSkill : Skill
{
    [SerializeField] private GameObject _blackHolePrefab;

    [SerializeField] private float _maxSize;
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _shrinkSpeed;
    [SerializeField] private int _amountOfAttacks;
    [SerializeField] private float _cloneAttackCooldown;
    [SerializeField] private float _blackHoleDuration;

    private BlackHoleSkillController _currentBlackHoleController;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        var newBlackHole = Instantiate(this._blackHolePrefab, Player.transform.position, Quaternion.identity);

        this._currentBlackHoleController = newBlackHole.GetComponent<BlackHoleSkillController>();

        this._currentBlackHoleController.SetupBlackHole(_maxSize, _growSpeed, _shrinkSpeed, _amountOfAttacks, _cloneAttackCooldown, _blackHoleDuration);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool SkillCompleted()
    {
        if (this._currentBlackHoleController is null)
            return false;

        if (this._currentBlackHoleController.PlayerCanExitState)
        {
            this._currentBlackHoleController = null;
            return true;
        }

        return false;
    }

    public float GetBlackholeRadius()
    {
        return this._maxSize / 2;
    }
}
