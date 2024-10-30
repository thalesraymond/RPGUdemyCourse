using Controllers.SkillsControllers;
using GameUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Skills
{
    public class SwordSkill : Skill
    {
        public SwordType SwordType = SwordType.Regular;

        [Header("Skill info")]
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private Vector2 launchForce;
        [SerializeField] private float swordGravity;
        [SerializeField] private float freezeTimeDuration;
        [SerializeField] private SkillTreeSlotUI _swordThrowSkillUnlockButton;
        public bool SwordThrowUnlocked { get; private set; }

        private Vector2 finalDirection;

        [Header("Aim Dots")]
        [SerializeField] private int numberOfDots;
        [SerializeField] private float dotSpacing;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Transform dotsParent;

        [Header("Bounce Info")]
        [SerializeField] private int bounceAmount;
        [SerializeField] private float bounceGravity;
        [SerializeField] private float bounceSpeed;
        [SerializeField] private SkillTreeSlotUI _swordBounceSkillUnlockButton;
        public bool SwordBounceUnlocked { get; private set; }


        [Header("Pierce Info")]
        [SerializeField] private int pierceAmount;
        [SerializeField] private float pierceGravity;
        [SerializeField] private SkillTreeSlotUI _swordPierceSkillUnlockButton;
        public bool SwordPierceUnlocked { get; private set; }

        [Header("Spin Info")]
        [SerializeField] private float maxTravelDistance = 7;
        [SerializeField] private float spinDuration = 2;
        [SerializeField] private float spinGravity = 1;
        [SerializeField] private float hitCooldown = .35f;
        [SerializeField] private float returnSpeed;
        [SerializeField] private SkillTreeSlotUI _swordSpinSkillUnlockButton;
        public bool SwordSpinUnlocked { get; private set; }

        [Header("Passive Skills")]
        [SerializeField] private SkillTreeSlotUI _swordTimeStopSkillUnlockButton;
        public bool SwordTimeStopUnlocked { get; private set; }
        [SerializeField] private SkillTreeSlotUI _vulnerableSkillUnlockButton;
        public bool SwordVulnerableUnlocked { get; private set; }

        private GameObject[] dots;

        protected override void Start()
        {
            base.Start();

            this.GenerateDots();

            this.SetupGravity();

            this._swordThrowSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordThrow);
            this._swordBounceSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordBounce);
            this._swordPierceSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordPierce);
            this._swordSpinSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordSpin);
            this._swordTimeStopSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordTimeStop);
            this._vulnerableSkillUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockSwordVulnerable);

            TryUnlockAll();
        }

        public void TryUnlockAll()
        {
            this.UnlockSwordThrow();
            this.UnlockSwordBounce();
            this.UnlockSwordPierce();
            this.UnlockSwordSpin();
            this.UnlockSwordTimeStop();
            this.UnlockSwordVulnerable();
        }

        private void SetupGravity()
        {
            switch (this.SwordType)
            {
                case SwordType.Bounce:
                    this.swordGravity = this.bounceGravity;
                    break;
                case SwordType.Pierce:
                    this.swordGravity = this.pierceGravity;
                    break;
                case SwordType.Spin:
                    this.swordGravity = this.spinGravity;
                    break;
            }
        }

        protected override void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                var aimDirection = this.GetAimDirection();

                this.finalDirection = new Vector2(aimDirection.normalized.x * launchForce.x, aimDirection.normalized.y * launchForce.y);
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                for (var i = 0; i < this.dots.Length; i++)
                {
                    dots[i].transform.position = this.DotsPosition(i * this.dotSpacing);
                }
            }

        }

        public void CreateSword()
        {
            var newSword = Instantiate(swordPrefab, Player.transform.position, transform.rotation);

            var swordSkillController = newSword.GetComponent<SwordSkillController>();

            if (this.SwordType == SwordType.Bounce)
                swordSkillController.SetupBounce(true, this.bounceAmount, this.bounceSpeed);
            else if (this.SwordType == SwordType.Pierce)
                swordSkillController.SetupPierce(this.pierceAmount);
            else if (this.SwordType == SwordType.Spin)
                swordSkillController.SetupSpin(true, this.maxTravelDistance, this.spinDuration, this.hitCooldown);

            this.SetupGravity();
            swordSkillController.SetupSword(finalDirection, this.swordGravity, this.freezeTimeDuration, this.returnSpeed);

            this.Player.AssignNewSword(newSword);

            this.DotsActive(false);
        }

        public Vector2 GetAimDirection()
        {
            var playerPosition = Player.transform.position;

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var direction = mousePosition - playerPosition;

            return direction;
        }

        public void DotsActive(bool isActive)
        {
            for (var i = 0; i < this.dots.Length; i++)
            {
                dots[i].SetActive(isActive);
            }
        }

        private void GenerateDots()
        {
            this.dots = new GameObject[this.numberOfDots];

            for (var i = 0; i < numberOfDots; i++)
            {
                this.dots[i] = Instantiate(dotPrefab, this.Player.transform.position, Quaternion.identity, this.dotsParent);

                this.dots[i].SetActive(false);
            }
        }

        private Vector2 DotsPosition(float t)
        {
            var position = (Vector2)Player.transform.position + new Vector2(
                this.GetAimDirection().normalized.x * this.launchForce.x,
                this.GetAimDirection().normalized.y * this.launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity * (t * t));

            return position;
        }

        private void UnlockSwordThrow()
        {
            if (this._swordThrowSkillUnlockButton.Unlocked)
            {
                this.SwordType = SwordType.Regular;
                this.SwordThrowUnlocked = true;
            }
        }

        private void UnlockSwordBounce()
        {
            if (this._swordBounceSkillUnlockButton.Unlocked)
            {
                this.SwordType = SwordType.Bounce;
                this.SwordBounceUnlocked = true;
            }
        }

        private void UnlockSwordSpin()
        {
            if (this._swordSpinSkillUnlockButton.Unlocked)
            {
                this.SwordType = SwordType.Spin;
                this.SwordSpinUnlocked = true;
            }
        }

        private void UnlockSwordPierce()
        {
            if (this._swordPierceSkillUnlockButton.Unlocked)
            {
                this.SwordType = SwordType.Pierce;
                this.SwordPierceUnlocked = true;
            }
        }

        private void UnlockSwordTimeStop()
        {
            if (this._swordTimeStopSkillUnlockButton.Unlocked)
                this.SwordTimeStopUnlocked = true;
        }
        private void UnlockSwordVulnerable()
        {
            if (this._vulnerableSkillUnlockButton.Unlocked)
                this.SwordVulnerableUnlocked = true;
        }
    }
}
