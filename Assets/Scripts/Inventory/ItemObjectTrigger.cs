using PlayerStates;
using Stats;
using UnityEngine;

namespace Inventory
{
    public class ItemObjectTrigger : MonoBehaviour
    {
        private ItemObject _itemObject => this.GetComponentInParent<ItemObject>();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null && !collision.GetComponent<CharacterStats>().IsDead)
            {
                Debug.Log("Player picked up item");

                this._itemObject.PickUpItem();
            }

        }
    }
}
