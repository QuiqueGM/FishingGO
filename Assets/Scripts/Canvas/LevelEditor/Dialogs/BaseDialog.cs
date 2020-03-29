using TMPro;
using UnityEngine;

namespace VFG.Canvas.LevelEditor
{
    public delegate void DialogWindowHandler();

    public class BaseDialog : MonoBehaviour
    {
        public bool deactiveOnAwake;
        public event DialogWindowHandler windowStateEvent;

        private void Awake()
        {
            if (deactiveOnAwake) SetActive(false);
        }

        public virtual void Init()
        {
            SetActive(false);
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);

            if (windowStateEvent != null)
                windowStateEvent();
        }
    }
}