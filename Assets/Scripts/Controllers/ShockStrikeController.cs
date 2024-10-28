using Stats;
using UnityEngine;

namespace Controllers
{
    public class ShockStrikeController : MonoBehaviour
    {
        [SerializeField] private CharacterStats _targetStats;
        [SerializeField] private float _speed;

        private Animator _animator;

        private bool _triggered;

        private int _damage = 1;

        public void Setup(int damage, CharacterStats targetStats)
        {
            _damage = damage;
            this._targetStats = targetStats;
        }

        // Start is called before the first frame update
        private void Start()
        {
            this._animator = this.GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_triggered)
                return;

            if (!this._targetStats)
                return;

            transform.position = Vector2.MoveTowards(transform.position, _targetStats.transform.position, _speed * Time.deltaTime);

            transform.right = transform.position - _targetStats.transform.position;

            if (Vector2.Distance(transform.position, _targetStats.transform.position) < 0.1f)
            {
                _animator.transform.localPosition = new Vector3(0, 0.5f);
                this._animator.transform.localRotation = Quaternion.identity;

                transform.localRotation = Quaternion.identity;
                transform.localScale = new Vector3(3, 3);


                _triggered = true;

                Invoke(nameof(DamageAndSelfDestroy), 0.2f);

                this._animator.SetTrigger("Hit");
            }
        }

        private void DamageAndSelfDestroy()
        {
            _targetStats.TakeDamage(this._damage);



            Destroy(this.gameObject, .4f);
        }
    }
}
