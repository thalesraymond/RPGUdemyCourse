using System;
using PlayerStates;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static readonly int ActiveTriggerName = Animator.StringToHash("Active");
    
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
    }

    private void GenerateId()
    {
        this.checkpointId = this.gameObject.transform.position.ToString();
    }
}