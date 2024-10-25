using UnityEngine;

namespace Controllers.SkillsControllers
{
    public class SkillController : MonoBehaviour
    {
        protected Player.Player Player;

        public virtual void Start()
        {
            this.Player = PlayerManager.Instance.Player;
        }
    }
}
