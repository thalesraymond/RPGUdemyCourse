using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillTreeSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public bool Unlocked;

    [SerializeField] private SkillTreeSlotUI[] _shouldBeUnlocked;
    [SerializeField] private SkillTreeSlotUI[] _shouldBeLocked;
    [SerializeField] private Image _skillImage;

    [SerializeField] private string _skillName;
    [SerializeField][TextArea] private string _skillDescription;

    [SerializeField] private Color _lockedSkillColor;
    [SerializeField] private Color _unlockedSkillColor;

    protected UI UI;

    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlotUI - " + this._skillName;
    }

    public void Start()
    {
        this._skillImage = this.GetComponent<Image>();

        this._skillImage.color = this.Unlocked ? this._unlockedSkillColor : this._lockedSkillColor;

        GetComponent<Button>().onClick.AddListener(UnlockSkillSlot);

        this.UI = GetComponentInParent<UI>();
    }

    public void UnlockSkillSlot()
    {
        foreach (SkillTreeSlotUI skillTreeSlot in this._shouldBeUnlocked)
        {
            if (!skillTreeSlot.Unlocked)
            {
                Debug.Log("Requirements not unlocked");
                return;
            }
        }

        foreach (SkillTreeSlotUI skillTreeSlot in this._shouldBeLocked)
        {
            if (skillTreeSlot.Unlocked)
            {
                Debug.Log("Incompatible skill already unlocked");
                return;
            }
        }

        this.Unlocked = true;
        this._skillImage.color = this._unlockedSkillColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.UI.SkillToolTipUI.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.UI.SkillToolTipUI.ShowTooltip(this._skillName, this._skillDescription);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        this.UI.SkillToolTipUI.ShowTooltip(this._skillName, this._skillDescription);
    }

}
