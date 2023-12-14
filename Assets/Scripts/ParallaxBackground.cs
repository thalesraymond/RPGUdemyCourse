using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject mainCamera;

    [SerializeField]
    private float parallaxEffect;

    private float xPosition;
    void Start()
    {
        this.mainCamera = GameObject.Find("Main Camera");

        this.xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToMove = this.mainCamera.transform.position.x * this.parallaxEffect;

        transform.position = new Vector3(this.xPosition + distanceToMove, transform.position.y, transform.position.z);
    }
}
