using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public ItemToolTipUI ItemToolTipUI;

    // Start is called before the first frame update
    void Start()
    {
        this.ItemToolTipUI = GetComponentInChildren<ItemToolTipUI>(true);
    }
     
    // Update is called once per frame
    void Update()
    {
        
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
}
