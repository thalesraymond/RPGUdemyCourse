using GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class DashSkill : Skill
    {
        public bool DashUnlocked { get; private set; }
        [Header("Vanilla Dash")]
        [SerializeField] private SkillTreeSlotUI _dashSlotUnlockButton;

    
        public bool CloneOnDashUnlocked { get; private set; }
        [Header("Clone on Dash")]
        [SerializeField] private SkillTreeSlotUI _cloneOnDashSlotUnlockButton;
    
        public bool CloneOnDashArrivalUnlocked { get; private set; }
        [Header("Clone on Dash Arrival")]
        [SerializeField] private SkillTreeSlotUI _cloneOnDashArrivalSlotUnlockButton;

        protected override void Start()
        {
            base.Start();

            this._dashSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
            this._cloneOnDashSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
            this._cloneOnDashArrivalSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDashArrival);

            this.UnlockDash();
            this.UnlockCloneOnDash();
            this.UnlockCloneOnDashArrival();
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
}
