using TMPro;

namespace VFG.Canvas.LevelEditor
{
    public class KeyButton : BaseButton
    {
        private TMP_InputField input;

        public override void Awake()
        {
            transform.Find("Parent/Text").GetComponent<TMP_Text>().text = gameObject.name;
            input = transform.Find("../../InputField").GetComponent<TMP_InputField>();
            base.Awake();
        }

        public override void TriggerIsPressed()
        {
            if (base._animator.GetBool("onFocus"))
            {
                if (gameObject.name != "<")
                    input.text += gameObject.name;
                else if (input.text.Length > 0)
                    input.text = input.text.Substring(0, input.text.Length - 1);
            }
        }
    }
}
