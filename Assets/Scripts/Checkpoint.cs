using System;
using Managers;
using PlayerStates;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private const string ActiveTriggerName = "Active";

    private Animator _animator;

    public  string checkpointId;
    
    public bool IsActive => this._animator.GetBool(ActiveTriggerName);

    private void Start()
    {
        this._animator = this.GetComponent<Animator>();

        this.GenerateId();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            this.ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        this._animator.SetBool(ActiveTriggerName, true);
        
        AudioManager.Instance.PlaySoundEffect(SoundEffect.Checkpoint, this.transform);
    }

    private void GenerateId()
    {
        this.checkpointId = this.gameObject.transform.position.ToString();
    }
}