namespace VFG.LevelEditor
{
    public class WheelItems : WheelSelector
    {
        public override void Action(int numTexture)
        {
            base.Action(numTexture);
            EditorState.groupOfItemsToShow = wheelButtons[numTexture].name;
        }
    }
}
