using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodes;

    public float MaxSize;
    public float GrowSpeed;
    public bool CanGrow;
    private List<Transform> Targets = new List<Transform>();

    private void Update()
    {
        if(CanGrow)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(this.MaxSize, this.MaxSize), Time.deltaTime * this.GrowSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodes.Count <= 0)
            return;

        var newHotKey = Instantiate(this.hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        var choosenKey = keyCodes[Random.Range(0, keyCodes.Count)];

        keyCodes.Remove(choosenKey);

        var newHotKeyController = newHotKey.GetComponent<BlackholeHotkeyController>();

        newHotKeyController.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform enemyTransfrom) => this.Targets.Add(enemyTransfrom);
}
