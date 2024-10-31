using Managers;
using Skills;
using Stats;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class InGameUI : MonoBehaviour
    {

        [SerializeField] private Slider _slider;
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private Image _dashImage;
        [SerializeField] private Image _parryImage;
        [SerializeField] private Image _crystalImage;
        [SerializeField] private Image _swordImage;
        [SerializeField] private Image _blackHoleImage;
        
        [Header("Souls Info")]
        [SerializeField] private TextMeshProUGUI _currentSouls;
        [SerializeField] private float soulsAmount;
        [SerializeField] private float increaseRate = 100;
        
        
        private SkillManager _skillManager => SkillManager.Instance;

        // Start is called before the first frame update
        private void Start()
        {
            if (this._playerStats != null)
            {
                this._playerStats.OnHealthChanged += UpdateHealthUI;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            this.UpdateSoulsUI();

            if (Input.GetKeyDown(KeyCode.LeftShift) && _skillManager.DashSkill.DashUnlocked)
                SetCooldown(this._dashImage);

            if(Input.GetKeyDown(KeyCode.Q) && _skillManager.ParrySkill.ParrySkillUnlocked)
                SetCooldown(this._parryImage);

            if (Input.GetKeyDown(KeyCode.F) && _skillManager.CrystalSkill.SimpleCrystalUnlocked)
                SetCooldown(this._crystalImage);

            if (Input.GetKeyDown(KeyCode.Mouse1) && _skillManager.SwordSkill.SwordThrowUnlocked)
                SetCooldown(this._swordImage);

            if (Input.GetKeyDown(KeyCode.R) && _skillManager.BlackholeSkill.BackholeUnlocked)
                SetCooldown(this._blackHoleImage);

        
            CheckCooldownOf(this._dashImage, this._skillManager.DashSkill.Cooldown);
            CheckCooldownOf(this._parryImage, this._skillManager.ParrySkill.Cooldown);
            CheckCooldownOf(this._crystalImage, this._skillManager.CrystalSkill.Cooldown);
            CheckCooldownOf(this._swordImage, this._skillManager.SwordSkill.Cooldown);
            CheckCooldownOf(this._blackHoleImage, this._skillManager.BlackholeSkill.Cooldown);
        }

        private void UpdateSoulsUI()
        {
            if (this.soulsAmount < PlayerManager.Instance.GetCurrencyAmount())
            {
                this.soulsAmount += Time.deltaTime * this.increaseRate;
            }
            else
            {
                this.soulsAmount = PlayerManager.Instance.GetCurrencyAmount();
            }

            this._currentSouls.text = ((int)this.soulsAmount).ToString();
        }

        private void UpdateHealthUI()
        {
            _slider.maxValue = _playerStats.GetMaxHealthValue();
            _slider.value = _playerStats.CurrentHealthPoints;
        }

        private void SetCooldown(Image image)
        {
            if (image.fillAmount == 0)
            {
                image.fillAmount = 1;
            }
        }

        private void CheckCooldownOf(Image image, float coolDown)
        {
            if (image.fillAmount > 0)
            {
                image.fillAmount -= 1 / coolDown * Time.deltaTime;
            }
        }
    }
}
