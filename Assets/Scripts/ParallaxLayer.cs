using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;

    public void Move(float distanceToMove)
    {
        this.background.position += new Vector3(distanceToMove * this.parallaxMultiplier, 0, 0);
    }
}
