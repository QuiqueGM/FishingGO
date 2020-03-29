using TMPro;
using UnityEngine;

namespace VFG.Canvas.LevelEditor
{
    public class KeyboardMenu : BaseDialog
    {
        private TMP_InputField input;

        public override void Init()
        {
            input = transform.Find("InputField").GetComponent<TMP_InputField>();
            SetActive(false);
        }

        public string NameScene
        {
            get
            {
                if (input.text == string.Empty || input.text == "" || input.text == "0" || input.text == null)
                    return null;
                else
                    return input.text;
            }
            set
            {
                input.text = value;
            }
        }

        public void Intro()
        {
            FileDialog.NameLevel = NameScene;
            SetActive(false);
        }

        public void SetText()
        {
            NameScene = FileDialog.NameLevel;
        }
    }
}