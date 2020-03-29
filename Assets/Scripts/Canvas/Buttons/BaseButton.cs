using UnityEngine;
using UnityEngine.UI;

namespace VFG.Canvas
{
    public delegate void SendActionEvent(TypeOfAction action);

    public class BaseButton : MonoBehaviour
    {
        public event SendActionEvent sendAction;
        public bool isUnlocked = true;
        public TypeOfAction action;

        protected float alphaDisabled = 0.3f;
        protected Color colorMainButtonDisabled = new Color32(104, 170, 191, 178);
        protected Color colorSecondaryButtonDisabledWhite = new Color(1,1,1,0.3f);
        protected Color colorSecondaryButtonDisabledBlack = new Color(0, 0, 0, 0.3f);

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => SendAction(action));
        }

        public virtual void SendAction(TypeOfAction action)
        {
            if (!isUnlocked) return;
            if (sendAction != null) sendAction(action);
        }
    }
}