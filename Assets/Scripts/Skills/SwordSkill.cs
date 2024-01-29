using System;
using UnityEngine;

public class SwordSkill : Skill
{
    public SwordType SwordType = SwordType.Regular;

    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDirection;

    [Header("Aim Dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float dotSpacing;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    [Header("Bounce Info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;


    [Header("Pierce Info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        this.GenerateDots();

        this.SetupGravity();
    }

    private void SetupGravity()
    {
        switch(this.SwordType)
        {
            case SwordType.Bounce:
                this.swordGravity = this.bounceGravity;
                break;
            case SwordType.Pierce:
                this.swordGravity = this.pierceGravity;
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
            for (int i = 0; i < this.dots.Length; i++)
            {
                dots[i].transform.position = this.DotsPosition(i * this.dotSpacing);
            }
        }

    }

    public void CreateSword()
    {
        var newSword = Instantiate(swordPrefab, Player.transform.position, transform.rotation);

        var swordSkillController = newSword.GetComponent<SwordSkillController>();

        if(this.SwordType == SwordType.Bounce)
            swordSkillController.SetupBounce(true, this.bounceAmount);
        else if(this.SwordType == SwordType.Pierce)
            swordSkillController.SetupPierce(this.pierceAmount);      

        swordSkillController.SetupSword(finalDirection, swordGravity, this.Player);

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
        for (int i = 0; i < this.dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }

    private void GenerateDots()
    {
        this.dots = new GameObject[this.numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
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
}
