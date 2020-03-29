using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFG.Core.Audio;

namespace VFG.LevelEditor
{
    public delegate void SendActionEvent(EditorState.ActiveHand hand);

    public class WheelSelector : MonoBehaviour
    {
        public event SendActionEvent SendActionEvent;
        public float startOffset = 0;
        public EditorState.ActiveHand hand;
        public List<Texture2D> wheelButtons;

        private int numButtons;
        private string currentTexture = string.Empty;

        private void Awake()
        {
            gameObject.SetActive(false);
            numButtons = wheelButtons.Count;
        }

        public void SetTexture(double angle)
        {
            const float CIRCLE = 360;
            float divisor = CIRCLE / numButtons;
            int numTexture = Mathf.FloorToInt(((float)angle + startOffset) / divisor);

            if (numTexture >= numButtons) numTexture = 0;

            if (currentTexture != wheelButtons[numTexture].name)
            {
                currentTexture = wheelButtons[numTexture].name;
                GetComponent<MeshRenderer>().material.SetTexture("_MainTex", wheelButtons[numTexture]);
                Action(numTexture);

                if (SendActionEvent != null) SendActionEvent(hand);
            }
        }

        public virtual void Action(int numTexture) { }
    }
}
