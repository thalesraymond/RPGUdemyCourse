using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() == null)
        {
            Destroy(collision.gameObject);
        }
        
        collision.GetComponent<Entity>().Die();
    }
}
