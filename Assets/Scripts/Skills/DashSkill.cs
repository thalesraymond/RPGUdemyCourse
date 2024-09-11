using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill
{
    [Header("Vanilla Dash")]
    public bool DashUnlocked;
    [SerializeField] private SkillTreeSlotUI _dashSlotUnlockButton;

    [Header("Clone on Dash")]
    public bool CloneOnDashUnlocked;
    [SerializeField] private SkillTreeSlotUI _cloneOnDashSlotUnlockButton;

    [Header("Clone on Dash Arrival")]
    public bool CloneOnDashArrivalUnlocked;
    [SerializeField] private SkillTreeSlotUI _cloneOnDashArrivalSlotUnlockButton;

    protected override void Start()
    {
        base.Start();

        this._dashSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        this._cloneOnDashSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        this._cloneOnDashArrivalSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDashArrival);
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }

    private void UnlockDash()
    {
        if (this._dashSlotUnlockButton.Unlocked)
            this.DashUnlocked = true;
    }

    private void UnlockCloneOnDash()
    {
        if (this._cloneOnDashSlotUnlockButton.Unlocked)
            this.CloneOnDashUnlocked = true;
    }

    private void UnlockCloneOnDashArrival()
    {
        if (this._cloneOnDashArrivalSlotUnlockButton.Unlocked)
            this.CloneOnDashArrivalUnlocked = true;
    }

    public void CreateCloneOnDash()
    {
        if (this.CloneOnDashUnlocked)
            SkillManager.Instance.CloneSkill.CreateClone(this.Player.transform);

    }

    public void CreateCloneOnDashArrival()
    {
        if (this.CloneOnDashArrivalUnlocked)
            SkillManager.Instance.CloneSkill.CreateClone(this.Player.transform);
    }
}
