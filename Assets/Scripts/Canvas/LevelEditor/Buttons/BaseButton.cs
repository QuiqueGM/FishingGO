using UnityEngine;

namespace VFG.Canvas.LevelEditor
{
    public class BaseButton : MonoBehaviour
    {
        public bool isBlocked;
        public bool canBeHolded;

        protected Animator _animator;
        protected Color colorDisabled = new Color(0, 0, 0, 0.5f);

        public virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ToggleAnimation(bool state)
        {
            if (!isBlocked)
                _animator.SetBool("onFocus", state);
        }

        public virtual void TriggerIsPressed() { }

        public virtual void Action() { }
    }
}