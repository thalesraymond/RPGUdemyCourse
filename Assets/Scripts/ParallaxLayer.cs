using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier;

    private float _imageWidth;
    private float _imageHalfWidth;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        this._spriteRenderer = this.background.GetComponent<SpriteRenderer>();
    }

    public void CalculateImageWitdh()
    {
        this._imageWidth = this.background.GetComponent<SpriteRenderer>().bounds.size.x;
        this._imageHalfWidth = this._imageWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        this.background.position += new Vector3(distanceToMove * this.parallaxMultiplier, 0, 0);
    }
    
    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge, float cameraMovement)
    {
        var imageRightEdge = this.background.position.x + this._imageHalfWidth;
        var imageLeftEdge = this.background.position.x - this._imageHalfWidth;
        var relativeMovement = cameraMovement * (this.parallaxMultiplier - 1f);
        
        if (relativeMovement < 0 && imageRightEdge < cameraLeftEdge)
        {
            this.background.position += Vector3.right * (this._imageWidth * 3);
        }
        else if (relativeMovement > 0 && imageLeftEdge > cameraRightEdge)
        {
            this.background.position -= Vector3.right * (this._imageWidth * 3);
        }
    }
}
