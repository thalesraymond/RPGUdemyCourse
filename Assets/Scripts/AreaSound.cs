using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using PlayerStates;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    [SerializeField] private SoundEffect areaSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;
        
        AudioManager.Instance.PlaySoundEffect(this.areaSoundEffect);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;
        
        AudioManager.Instance.GraduallyStopSoundEffect(this.areaSoundEffect);
    }
}
