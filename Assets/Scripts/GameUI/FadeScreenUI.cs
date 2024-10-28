using UnityEngine;

namespace GameUI
{
    public class FadeScreenUI : MonoBehaviour
    {
        private Animator _animator;
        // Start is called before the first frame update
        private void Start()
        {
            _animator = this.GetComponent<Animator>();
        }

        public void FadeOut() => this._animator.SetTrigger("FadeOut");
        
        public void FadeIn() => this._animator.SetTrigger("FadeIn");
    }
}
