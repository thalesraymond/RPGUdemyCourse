using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameUI
{
    public class CraftListUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Transform _craftSlotParent;
        [SerializeField] private GameObject _craftSlotPrefab;
        [SerializeField] private List<EquipmentItemData> _craftEquipments;

        void OnEnable()
        {
            transform.parent.GetChild(0).GetComponent<CraftListUI>().SetupCraftList();

            this.SetupDefaultCraftWindow();
        }


        public void SetupCraftList()
        {
            for (var i = 0; i < this._craftSlotParent.childCount; i++)
            {
                Destroy(this._craftSlotParent.GetChild(i).gameObject);
            }

            for (var i = 0; i < this._craftEquipments.Count; i++)
            {
                var slot = Instantiate(this._craftSlotPrefab, this._craftSlotParent);

                slot.GetComponent<CraftSlotUI>().SetupCraftSlot(this._craftEquipments[i]);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.SetupCraftList();
        }
        public void SetupDefaultCraftWindow()
        {
            var ui = GetComponentInParent<UI>();

            ui.CraftWindowUI.SetupCraftWindow(this._craftEquipments[0]);
        }
    }
}
