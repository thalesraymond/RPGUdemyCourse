using UnityEngine;

namespace ParallaxBackground
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Camera _mainCamera;
        private float _lastMainCameraPositionX;
        private float _cameraHalfWidth;
    
        [SerializeField] private ParallaxLayer[] layers;
    
        private void Awake()
        {
            this._mainCamera = Camera.main;
            this._lastMainCameraPositionX = this._mainCamera.transform.position.x;
            this._cameraHalfWidth = this._mainCamera.orthographicSize * this._mainCamera.aspect;
        
            this.CalculateTileWidths();
        }

        private void Update()
        {
            var currentCameraPositionX = this._mainCamera.transform.position.x;
            var cameraMovementX = currentCameraPositionX - this._lastMainCameraPositionX;
            this._lastMainCameraPositionX = currentCameraPositionX;
        
            var cameraRightEdge = currentCameraPositionX + this._cameraHalfWidth;
            var cameraLeftEdge = currentCameraPositionX - this._cameraHalfWidth;
        
            foreach (var layer in this.layers)
            {
                layer.Move(cameraMovementX);
                layer.LoopBackground(cameraLeftEdge, cameraRightEdge, cameraMovementX);
            }
        }

        private void CalculateTileWidths()
        {
            foreach (var layer in this.layers)
            {
                layer.CalculateTileWidth();
            }
        }
    }
}

