namespace VFG.Canvas.LevelEditor
{
    public class EditorButton : BaseButton
    {
        public LvlEditorAction action;

        public override void TriggerIsPressed()
        {
            if (base._animator.GetBool("onFocus"))
            {
                gameObject.GetComponentInParent<EditorCanvasController>().GoToSelectedMenu(action);
                if (!canBeHolded) ToggleAnimation(false);
            }
        }
    }
}