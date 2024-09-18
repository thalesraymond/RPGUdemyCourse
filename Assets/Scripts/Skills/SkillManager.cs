using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    public DashSkill DashSkill { get; private set; }

    public CloneSkill CloneSkill { get; private set; }

    public SwordSkill SwordSkill { get; private set; }

    public BlackholeSkill BlackholeSkill { get; private set; }

    public CrystalSkill CrystalSkill { get; private set; }

    public ParrySkill ParrySkill { get; private set; }

    public DodgeSkill DodgeSkill { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);

            return;
        }

        Instance = this;
    }

    private void Start()
    {
        this.DashSkill = GetComponent<DashSkill>();

        this.CloneSkill = GetComponent<CloneSkill>();

        this.SwordSkill = GetComponent<SwordSkill>();

        this.BlackholeSkill = GetComponent<BlackholeSkill>();

        this.CrystalSkill = GetComponent<CrystalSkill>();

        this.ParrySkill = GetComponent<ParrySkill>();

        this.DodgeSkill = GetComponent<DodgeSkill>();
    }
}
