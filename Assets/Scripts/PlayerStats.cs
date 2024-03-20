using System.Collections;
using System.Collections.Generic;
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

        this._player.DamageEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
