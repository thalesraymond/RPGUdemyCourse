using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject mainCamera;

    [SerializeField]
    private float parallaxEffect;

    private float xPosition;

    private float length;
    void Start()
    {
        this.mainCamera = GameObject.Find("Main Camera");

        this.xPosition = transform.position.x;

        this.length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = this.mainCamera.transform.position.x * (1 - this.parallaxEffect);

        float distanceToMove = this.mainCamera.transform.position.x * this.parallaxEffect;

        transform.position = new Vector3(this.xPosition + distanceToMove, transform.position.y, transform.position.z);

        if (distanceMoved > this.xPosition + this.length)
            xPosition = xPosition + length;
        else if (distanceMoved < this.xPosition - this.length)
            xPosition = xPosition - length;

    }
}
