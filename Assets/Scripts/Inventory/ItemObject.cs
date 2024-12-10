using Managers;
using UnityEngine;

namespace Inventory
{
    public class ItemObject : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private Rigidbody2D _rigidbody;

        private void SetupVisuals()
        {
            if (this._itemData == null)
                return;

            GetComponent<SpriteRenderer>().sprite = _itemData.ItemIcon;

            gameObject.name = "Item Object - " + _itemData.name;
        }

        public void PickUpItem()
        {
            if (!Inventory.Instance.CanAddEquipmentItem() && this._itemData.ItemType == ItemType.Equipment)
            {
                this._rigidbody.velocity = new Vector2(0, 7);
                PlayerManager.Instance.Player.FX.CreatePopUpText("Inventory Full");
                return;
            }


            Inventory.Instance.AddItem(this._itemData);
            
            AudioManager.Instance.PlaySoundEffect(SoundEffect.ItemPickUp, this.transform, true);

            Destroy(gameObject);
        }

        public void SetupItem(ItemData itemData, Vector2 velocity)
        {
            this._itemData = itemData;
            this._rigidbody.velocity = velocity;

            this.SetupVisuals();
        }
    }
}
