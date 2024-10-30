using GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class DodgeSkill : Skill
    {
        [Header("Dodge")]
        [SerializeField] private SkillTreeSlotUI _dodgeSkillUnlockButton;
        public bool DodgeUnlocked;
        [SerializeField] private int _evasionAmount;

        [Header("Mirage Dodge")]
        [SerializeField] private SkillTreeSlotUI _mirageDodgeSkillUnlockButton;
        public bool MirageDodgeUnlocked;

        protected override void Start()
        {
            base.Start();

            this._dodgeSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
            this._mirageDodgeSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMirage);

            TryUnlockAll();
        }

        public void TryUnlockAll()
        {
            this.UnlockDodge();
            this.UnlockMirage();
        }

        private void UnlockDodge()
        {
            if(this._dodgeSkillUnlockButton.Unlocked)
            {
                this.Player.Stats.Evasion.AddModifier(this._evasionAmount);
                this.DodgeUnlocked = true;
            }
        }

        private void UnlockMirage()
        {
            if (this._mirageDodgeSkillUnlockButton.Unlocked)
                this.MirageDodgeUnlocked = true;
        }

        public void CreateMirageOnDodge()
        {
            if (!this.MirageDodgeUnlocked)
                return;

            SkillManager.Instance.CloneSkill.CreateClone(this.Player.transform, Vector3.zero);
        }
    }
}

