using UnityEngine;

namespace ParallaxBackground
{
    [System.Serializable]
    public class ParallaxLayer
    {
        [SerializeField] private Transform background;
        [SerializeField] private float parallaxMultiplier;

        private float _tileWidth;
        private float _tileHalfWidth;

        public void CalculateTileWidth()
        {
            this._tileWidth = this.background.GetComponent<SpriteRenderer>().bounds.size.x;
            this._tileHalfWidth = this._tileWidth / 2;
        }

        public void Move(float cameraMovementX)
        {
            this.background.position += new Vector3(cameraMovementX * this.parallaxMultiplier, 0, 0);
        }
    
        public void LoopBackground(float cameraLeftEdge, float cameraRightEdge, float cameraMovement)
        {
            var tileRightEdge = this.background.position.x + this._tileHalfWidth;
            var tileLeftEdge = this.background.position.x - this._tileHalfWidth;
            // Negative means the tile moves left relative to the camera.
            var movementRelativeToCamera = cameraMovement * (this.parallaxMultiplier - 1f);
        
            // Each layer has three adjacent tiles; recycle an offscreen tile to the opposite end.
            switch (movementRelativeToCamera)
            {
                case < 0 when tileRightEdge < cameraLeftEdge:
                    this.background.position += Vector3.right * (this._tileWidth * 3);
                    break;
                case > 0 when tileLeftEdge > cameraRightEdge:
                    this.background.position -= Vector3.right * (this._tileWidth * 3);
                    break;
            }
        }
    }
}
