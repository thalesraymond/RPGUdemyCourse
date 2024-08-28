using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public ItemToolTipUI ItemToolTipUI;
    public StatToolTipUI StatToolTipUI;

    [SerializeField] private GameObject _characterUI;
    [SerializeField] private GameObject _skillTreeUI;
    [SerializeField] private GameObject _craftUI;
    [SerializeField] private GameObject _optionsUI;

    // Start is called before the first frame update
    void Start()
    {
        this.ItemToolTipUI = GetComponentInChildren<ItemToolTipUI>(true);

        SwitchTo(null);

        this.ItemToolTipUI.HideTooltip();

        this.StatToolTipUI.HideTooltip();
    }
     
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SwitchWithKeyTo(this._characterUI);
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            SwitchWithKeyTo(this._craftUI);
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            SwitchWithKeyTo(this._skillTreeUI);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            SwitchWithKeyTo(this._optionsUI);
        }
    }

    public void SwitchTo(GameObject menu)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if(menu == null)
            return;
            
        menu.SetActive(true);
    }

    public void SwitchWithKeyTo(GameObject menu)
    {
        if(menu != null && menu.activeSelf)
        {
            menu.SetActive(false);
            return;
        }

        SwitchTo(menu);
    }
}
