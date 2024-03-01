using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    private Animator _animator => GetComponent<Animator>();

    private CircleCollider2D _circleCollider2D => GetComponent<CircleCollider2D>();

    private float _crystalExistTimer;

    private bool _canExplode;
    private bool _canMove;
    private float _moveSpeed;

    private bool _canGrow;

    [SerializeField] private float _growSpeed = 5f;

    public void SetupCrystal(float crystalDuration, bool canExplode, bool canMove, float moveSpeed)
    {
        this._crystalExistTimer = crystalDuration;
        this._canExplode = canExplode;
        this._canMove = canMove;
        this._moveSpeed = moveSpeed;
    }

    private void Update()
    {
        this._crystalExistTimer -= Time.deltaTime;

        if (_crystalExistTimer < 0)
        {
            FinishCrystal();
        }

        if(this._canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), this._growSpeed * Time.deltaTime);
        }    

    }

    private void AnimationExplodeEvent()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, this._circleCollider2D.radius);

        foreach (var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() is not null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    public void FinishCrystal()
    {
        if (this._canExplode)
        {
            this._canGrow = true;

            _animator.SetTrigger("Explode");
        }
        else
        {
            SelfDestroy();
        }
    }

    public void SelfDestroy() => Destroy(gameObject);
}