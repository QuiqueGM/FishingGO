using System;

namespace VFG.LevelEditor
{
    public class WheelActions : WheelSelector
    {
        public override void Action(int numTexture)
        {
            base.Action(numTexture);
            EditorState.currentAction = (EditorState.TypeOfAction)Enum.Parse(typeof(EditorState.TypeOfAction), wheelButtons[numTexture].name);
        }
    }
}
