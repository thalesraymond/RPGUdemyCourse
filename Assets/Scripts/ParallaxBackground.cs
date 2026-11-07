using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera _mainCamera;
    private float _lastMainCameraPositionX;
    [SerializeField] private ParallaxLayer[] layers;
    
    private void Awake()
    {
        this._mainCamera = Camera.main;
    }

    private void Update()
    {
        var currentCameraPositionX = this._mainCamera.transform.position.x;
        var distanceToMove = currentCameraPositionX - this._lastMainCameraPositionX;
        this._lastMainCameraPositionX = currentCameraPositionX;
        
        foreach (var layer in this.layers)
        {
            layer.Move(distanceToMove);
        }
    }
}
