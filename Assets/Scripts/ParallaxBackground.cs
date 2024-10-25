using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject _mainCamera;

    [SerializeField]
    private float parallaxEffect;

    private float _xPosition;

    private float _length;

    private void Start()
    {
        this._mainCamera = GameObject.Find("Main Camera");

        this._xPosition = transform.position.x;

        this._length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceMoved = this._mainCamera.transform.position.x * (1 - this.parallaxEffect);

        var distanceToMove = this._mainCamera.transform.position.x * this.parallaxEffect;

        transform.position = new Vector3(this._xPosition + distanceToMove, transform.position.y, transform.position.z);

        if (distanceMoved > this._xPosition + this._length)
            _xPosition = _xPosition + _length;
        else if (distanceMoved < this._xPosition - this._length)
            _xPosition = _xPosition - _length;

    }
}
