using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player _player;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _player = GetComponent<Player>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();

        this._player.Die();

        var playerDrops = GetComponent<PlayerItemDrop>();

        if (playerDrops != null)
            playerDrops.GenerateDrops();
    }

    protected override void DecreaseHealthBy(int damage)
    {
        base.DecreaseHealthBy(damage);

        var currentArmor = Inventory.Instance.GetEquipmentByType(EquipmentType.Armor);

        if (currentArmor != null)
            currentArmor.ExecuteItemEffect(this._player.transform);
    }

    public override void OnEvasion()
    {
        base.OnEvasion();

        this._player.SkillManager.DodgeSkill.CreateMirageOnDodge();
    }

    public void CloneDoDamage(CharacterStats targetStats, float attackMultiplier)
    {
        if (this.TargetCanAvoidAttack(targetStats))
            return;

        var totalDamage = Damage.GetValue() + Strength.GetValue();

        totalDamage = this.CheckTargetArmor(targetStats, totalDamage);
        
        if(attackMultiplier > 0)
            totalDamage = Mathf.RoundToInt(totalDamage * attackMultiplier);

        targetStats.TakeDamage(totalDamage);

        this.DoMagicalDamage(targetStats);
    }
}