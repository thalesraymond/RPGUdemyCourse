using TMPro;
using UnityEngine;

namespace Effects
{
    public class PopUpText : MonoBehaviour
    {
        private TextMeshPro _text;
        [SerializeField] private float speed;
        [SerializeField] private float disappearSpeed;
        [SerializeField] private float colorDisappearSpeed;

        [SerializeField] private float lifeTime;

        private float _textTimer;
        // Start is called before the first frame update
        void Start()
        {
            _text = GetComponent<TextMeshPro>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        
            _textTimer -= Time.deltaTime;

            if (_textTimer >= 0) return;
        
            var alpha = _text.color.a - colorDisappearSpeed * Time.deltaTime;
            
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);

            if (_text.color.a < 50) speed = disappearSpeed;
        
            if(_text.color.a <= 0) Destroy(gameObject);
        }
    }
}
