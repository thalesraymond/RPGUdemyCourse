using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected Player Player;

    public virtual void Start()
    {
        this.Player = PlayerManager.Instance.Player;
    }
}
