using UnityEngine;

namespace VFG.Canvas.LevelEditor
{
    public class ButtonLevel : MonoBehaviour
    {
        public GameObject BTN_Level;
        public TMPro.TMP_Text TXT_Level;

        public void SetButton(string name)
        {
            gameObject.name = name;
            BTN_Level.name = name;
            TXT_Level.text = name;
        }
    }
}