using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private float colorLosingSpeed;
    [SerializeField] private float cloneDurantion;
    private float cloneTimer;

    private SpriteRenderer spriteRenderer;
    public void SetupClone(Transform newTransform)
    {
        transform.position = newTransform.position + new Vector3(0,-0.45f,0);

        cloneTimer = cloneDurantion;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            this.spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime * colorLosingSpeed));
        }
    }
}
