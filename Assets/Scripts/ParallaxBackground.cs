using System;
using UnityEngine;

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
        
        this.CalculateImageLength();
    }

    private void Update()
    {
        var currentCameraPositionX = this._mainCamera.transform.position.x;
        var distanceToMove = currentCameraPositionX - this._lastMainCameraPositionX;
        this._lastMainCameraPositionX = currentCameraPositionX;
        
        var cameraRightEdge = currentCameraPositionX + this._cameraHalfWidth;
        var cameraLeftEdge = currentCameraPositionX - this._cameraHalfWidth;
        
        foreach (var layer in this.layers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge, distanceToMove);
        }
    }

    private void CalculateImageLength()
    {
        foreach (var layer in this.layers)
        {
            layer.CalculateImageWitdh();
        }
    }
}

