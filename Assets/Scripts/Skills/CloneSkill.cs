using System.Collections;
using Controllers.SkillsControllers;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class CloneSkill : Skill
    {
        [Header("Clone Info")]
        [SerializeField] private float _attackMultiplier;
        [SerializeField] private GameObject ClonePrefab;
        [SerializeField] private float cloneDurantion;
        [Space]

        [Header("Clone Attack")]
        [SerializeField] private float _cloneAttackMultiplier;
        [SerializeField] private SkillTreeSlotUI _cloneAttackSlotUnlockButton;
        public bool CloneAttackUnlocked { get; private set; }

        [Header("Aggressive Clone")]
        [SerializeField] private SkillTreeSlotUI _aggressiveCloneSlotUnlockButton;
        public bool AggressiveCloneUnlocked { get; private set; }
        [SerializeField] private float _aggressiveCloneMultiplier;
        [SerializeField] private float cloneOnCounterAttackDelay;

        [Header("Multiple Clones")]
        [SerializeField] private SkillTreeSlotUI _multipleClonesSlotUnlockButton;
        public bool MultipleClonesUnlocked { get; private set; }
        [SerializeField] private float _cloneDuplicationPercentageChance;
        [SerializeField] private float _multiCloneAttackMultiplier;

        [Header("Crystal instead of clone")]
        [SerializeField] private SkillTreeSlotUI _crystalInsteadOfCloneSlotUnlockButton;
        public bool CrystalInsteadOfCloneUnlocked { get; private set; }

        protected override void Start()
        {
            base.Start();

            this._cloneAttackSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneAttack);
            this._aggressiveCloneSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockAggressiveClone);
            this._multipleClonesSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockMultipleClones);
            this._crystalInsteadOfCloneSlotUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCrystalInsteadOfClone);

            this.UnlockCloneAttack();
            this.UnlockAggressiveClone();
            this.UnlockMultipleClones();
            this.UnlockCrystalInsteadOfClone();
        }

        private void UnlockCloneAttack()
        {
            if (this._cloneAttackSlotUnlockButton.Unlocked)
            {
                this.CloneAttackUnlocked = true;
                this._attackMultiplier = this._cloneAttackMultiplier;
            }
        }

        private void UnlockAggressiveClone()
        {
            if (this._aggressiveCloneSlotUnlockButton.Unlocked)
            {
                this.AggressiveCloneUnlocked = true;
                this._attackMultiplier = this._cloneAttackMultiplier;
            }
        }

        private void UnlockMultipleClones()
        {
            if (this._multipleClonesSlotUnlockButton.Unlocked)
                this.MultipleClonesUnlocked = true;
        }

        private void UnlockCrystalInsteadOfClone()
        {
            if (this._crystalInsteadOfCloneSlotUnlockButton.Unlocked)
            {
                this.CrystalInsteadOfCloneUnlocked = true;
                this._attackMultiplier = this._multiCloneAttackMultiplier;
            }
        }

        public void CreateClone(Transform clonePosition, Vector3? offset = null)
        {
            if (this.CrystalInsteadOfCloneUnlocked)
            {
                SkillManager.Instance.CrystalSkill.CreateCrystal();

                return;
            }

            var newClone = Instantiate(ClonePrefab, this.Player.transform.position, Quaternion.identity);

            if (offset == null)
                newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDurantion, this.CloneAttackUnlocked, this.FindClosestEnemy(newClone.transform), this.MultipleClonesUnlocked, _cloneDuplicationPercentageChance, this._attackMultiplier);
            else
                newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDurantion, this.CloneAttackUnlocked, this.FindClosestEnemy(newClone.transform), this.MultipleClonesUnlocked, _cloneDuplicationPercentageChance, this._attackMultiplier, offset.Value);
        }
        public void CreateCloneWithDelay(Transform enemyTransform)
        {
            if (this.Player.SkillManager.ParrySkill.ParryWithMirageUnlocked)
                StartCoroutine(CreateCloneWithDelayCoroutine(enemyTransform, new Vector3(1.5f * this.Player.FacingDirection, 0)));

        }

        private IEnumerator CreateCloneWithDelayCoroutine(Transform transform, Vector3 offSet)
        {
            yield return new WaitForSeconds(cloneOnCounterAttackDelay);

            CreateClone(transform, offSet);
        }
    }
}
