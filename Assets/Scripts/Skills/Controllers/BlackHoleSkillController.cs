using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodes;

    private float _maxSize;
    private float _growSpeed;
    private float _blackHoleTimer;
    private bool _canGrow = true;
    private List<Transform> _targets = new();

    private bool _cloneAttackReleased;
    private int _amountOfAttacks = 4;
    private float _cloneAttackCooldown = 0.3f;
    private float _cloneAttackTimer;
    private bool _playerCanDissapear = true;

    private List<GameObject> _createdHotkey = new();

    private bool _canShrink;
    private float _shrinkSpeed;

    private bool _canCreateHotkey = true;

    public bool PlayerCanExitState { get; private set; }

    public void SetupBlackHole(float maxSize, float growSpeed, float shrinkSpeed, int amountOfAttacks, float cloneAttackCooldown, float blackHoleDuration)
    {
        _maxSize = maxSize;
        _growSpeed = growSpeed;
        _shrinkSpeed = shrinkSpeed;
        _amountOfAttacks = amountOfAttacks;
        _cloneAttackCooldown = cloneAttackCooldown;
        _blackHoleTimer = blackHoleDuration;
    }

    private void Update()
    {
        _cloneAttackTimer -= Time.deltaTime;

        _blackHoleTimer -= Time.deltaTime;

        if (_blackHoleTimer < 0)
        {
            this._blackHoleTimer = Mathf.Infinity;

            if (this._targets.Count > 0)
            {
                this.ReleaseCloneAttack();
            }
            else
            {
                this.FinishBlackholeHability();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }

        CloneAttackLogic();

        if (_canGrow && !_canShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(this._maxSize, this._maxSize), Time.deltaTime * this._growSpeed);
        }

        if (_canShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(-1, -1), Time.deltaTime * this._shrinkSpeed);

            if (transform.localScale.x < 0)
                Destroy(gameObject);
        }
    }

    private void ReleaseCloneAttack()
    {
        if (this._targets.Count <= 0)
            return;

        _cloneAttackReleased = true;
        DestroyHotkeys();
        _canCreateHotkey = false;


        if(this._playerCanDissapear)
        {
            PlayerManager.Instance.Player.ToogleTransparent(true);
            this._playerCanDissapear = false;
        }

    }

    private void CloneAttackLogic()
    {
        if (_cloneAttackTimer < 0 && _cloneAttackReleased && this._amountOfAttacks > 0)
        {
            _cloneAttackTimer = _cloneAttackCooldown;

            var randomIndex = Random.Range(0, _targets.Count);

            float xOffset;

            if (Random.Range(0, 100) > 50)
            {
                xOffset = 1;
            }
            else
            {
                xOffset = -1;
            }

            SkillManager.Instance.CloneSkill.CreateClone(_targets[randomIndex], new Vector3(xOffset, 0));

            _amountOfAttacks--;

            if (_amountOfAttacks <= 0)
            {
                Invoke("FinishBlackholeHability", 1f);
            }
        }
    }

    private void FinishBlackholeHability()
    {
        _cloneAttackReleased = false;
        _canShrink = true;

        this.PlayerCanExitState = true;

        this.DestroyHotkeys();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodes.Count <= 0)
            return;

        if (_cloneAttackReleased)
            return;

        if (!_canCreateHotkey)
            return;

        var newHotKey = Instantiate(this.hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        this._createdHotkey.Add(newHotKey);

        var choosenKey = keyCodes[Random.Range(0, keyCodes.Count)];

        keyCodes.Remove(choosenKey);

        var newHotKeyController = newHotKey.GetComponent<BlackholeHotkeyController>();

        newHotKeyController.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform enemyTransfrom) => this._targets.Add(enemyTransfrom);

    private void DestroyHotkeys()
    {
        if (this._createdHotkey.Count <= 0)
            return;

        for (int i = 0; i < this._createdHotkey.Count; i++)
        {
            Destroy(this._createdHotkey[i]);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() == null)
            return;

        collision.GetComponent<Enemy>().FreezeTime(false);
    }
}
