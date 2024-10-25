using TMPro;
using UnityEngine;

namespace Controllers.SkillsControllers
{
    public class BlackholeHotkeyController : SkillController
    {
        private SpriteRenderer spriteRenderer;
        private KeyCode blackholeHotkey;
        private TextMeshProUGUI hotKeyText;

        private Transform enemy;
        private BlackHoleSkillController blackHoleSkillController;

        public void SetupHotKey(KeyCode hotkey, Transform enemy, BlackHoleSkillController blackHoleSkillController)
        {
            hotKeyText = GetComponentInChildren<TextMeshProUGUI>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            this.enemy = enemy;
            this.blackHoleSkillController = blackHoleSkillController;

            blackholeHotkey = hotkey;
            hotKeyText.text = hotkey.ToString();
        }

        private void Update()
        {
            if (Input.GetKeyDown(blackholeHotkey))
            {
                blackHoleSkillController.AddEnemyToList(this.enemy);

                hotKeyText.color = Color.clear;

                spriteRenderer.color = Color.clear;
            }
        }

    }
}
