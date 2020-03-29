namespace VFG.Canvas.LevelEditor
{
    public class LevelButton : EditorButton
    {
        public override void TriggerIsPressed()
        {
            if (base._animator.GetBool("onFocus"))
            {
                FileDialog.NameLevel = gameObject.transform.parent.name;
                base.TriggerIsPressed();
            }
        }
    }
}