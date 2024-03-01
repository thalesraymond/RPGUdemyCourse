using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    private float _crystalExistTimer;

    public void SetupCrystal(float crystalDuration)
    {
        this._crystalExistTimer = crystalDuration;
    }

    private void Update()
    {
        this._crystalExistTimer -= Time.deltaTime;

        if (_crystalExistTimer < 0)
            SelfDestroy();

    }

    public void SelfDestroy() => Destroy(gameObject);
}
