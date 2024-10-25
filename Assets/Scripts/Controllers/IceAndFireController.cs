using UnityEngine;

namespace Controllers
{
    public class IceAndFireController : ThunderStrikeController
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }
    }
}
