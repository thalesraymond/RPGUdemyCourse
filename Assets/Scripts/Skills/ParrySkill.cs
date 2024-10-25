using GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class ParrySkill : Skill
    {
        [Header("Parry")]
        [SerializeField] private SkillTreeSlotUI _parrySlotUnlockButton;
        public bool ParrySkillUnlocked { get; private set; }

        [Header("Parry Restore")]
        [SerializeField] private SkillTreeSlotUI _parrySlotRestoreUnlockButton;
        [SerializeField][Range(0f, 1f)] private float _parryHealthRestorePercentageAmount;
        public bool ParryRestoreUnlocked { get; private set; }

        [Header("Parry With Mirage")]
        [SerializeField] private SkillTreeSlotUI _parrySlotWithMirageUnlockButton;
        public bool ParryWithMirageUnlocked { get; private set; }

        protected override void Start()
        {
            base.Start();

            this._parrySlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParrySkill);
            this._parrySlotRestoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
            this._parrySlotWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);

            this.UnlockParrySkill();
            this.UnlockParryRestore();
            this.UnlockParryWithMirage();
        }

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }


        private void UnlockParrySkill()
        {
            if (this._parrySlotUnlockButton.Unlocked)
                this.ParrySkillUnlocked = true;
        }

        private void UnlockParryRestore()
        {
            if (this._parrySlotRestoreUnlockButton.Unlocked)
                this.ParryRestoreUnlocked = true;
        }

        private void UnlockParryWithMirage()
        {
            if (this._parrySlotWithMirageUnlockButton.Unlocked)
                this.ParryWithMirageUnlocked = true;
        }

        public void MakeMirageOnParry(Transform enemyTransform)
        {
            if (!this.ParryWithMirageUnlocked) return;

            this.Player.SkillManager.CloneSkill.CreateCloneWithDelay(enemyTransform);
        }

        public override void UseSkill()
        {
            base.UseSkill();

            if(!this.ParryRestoreUnlocked) return;

            var restoreAmount = this.Player.Stats.GetMaxHealthValue() * this._parryHealthRestorePercentageAmount;

            this.Player.Stats.IncreaseHealthBy(Mathf.RoundToInt(restoreAmount));


        }

    }
}
