using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodes;

    public float MaxSize;
    public float GrowSpeed;
    public bool CanGrow;
    private List<Transform> Targets = new();

    private bool cloneAttackReleased;
    public int amountOfAttacks = 4;
    public float cloneAttackCooldown = 0.3f;
    private float cloneAttackTimer;

    private List<GameObject> createdHotkey = new();

    public bool CanShrink;
    public float shrinkSpeed;

    private bool canCreateHotkey = true;

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            cloneAttackReleased = true;
            DestroyHotkeys();
            canCreateHotkey = false;
        }
            

        if (cloneAttackTimer < 0 && cloneAttackReleased)
        {
            cloneAttackTimer = cloneAttackCooldown;

            var randomIndex = Random.Range(0, Targets.Count);

            float xOffset;

            if(Random.Range(0,100) > 50)
            {
                xOffset = 1;
            }
            else
            {
                xOffset = -1;
            }

            SkillManager.Instance.CloneSkill.CreateClone(Targets[randomIndex], new Vector3(xOffset, 0));

            amountOfAttacks--;

            if (amountOfAttacks <= 0)
            {
                cloneAttackReleased = false;
                CanShrink = true;
            }
        }

        if (CanGrow && !CanShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(this.MaxSize, this.MaxSize), Time.deltaTime * this.GrowSpeed);
        }

        if(CanShrink)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(-1, -1), Time.deltaTime * this.shrinkSpeed);

            if(transform.localScale.x < 0)
                Destroy(gameObject);
        }
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

        if (cloneAttackReleased)
            return;

        if(!canCreateHotkey)
            return;

        var newHotKey = Instantiate(this.hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        this.createdHotkey.Add(newHotKey);

        var choosenKey = keyCodes[Random.Range(0, keyCodes.Count)];

        keyCodes.Remove(choosenKey);

        var newHotKeyController = newHotKey.GetComponent<BlackholeHotkeyController>();

        newHotKeyController.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform enemyTransfrom) => this.Targets.Add(enemyTransfrom);

    private void DestroyHotkeys()
    {
        if (this.createdHotkey.Count <= 0)
            return;

        for (int i = 0; i < this.createdHotkey.Count; i++)
        {
            Destroy(this.createdHotkey[i]);
        }
    }
}
