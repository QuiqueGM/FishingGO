using UnityEngine;

namespace VFG.LevelEditor
{
    public class MaterialsModule : MonoBehaviour
    {
        public Material select;
        public Material scale;
        public Material duplicate;
        public Material delete;
        public Material freeze;
        public Material unfreeze;
        public Material size;
        public Material save;
        public Material undo;
        public Material @null;

        public Material GetMaterialFromAction(EditorState.TypeOfAction action)
        {
            switch (action)
            {
                case EditorState.TypeOfAction.Select: return select;
                case EditorState.TypeOfAction.Scale: return scale;
                case EditorState.TypeOfAction.Duplicate: return duplicate;
                case EditorState.TypeOfAction.Delete: return delete;
                case EditorState.TypeOfAction.ShowFreezeMenu:
                case EditorState.TypeOfAction.HideFreezeMenu:
                case EditorState.TypeOfAction.Freeze: return freeze;
                case EditorState.TypeOfAction.UnfreezeAll:
                case EditorState.TypeOfAction.Unfreeze: return unfreeze;
                case EditorState.TypeOfAction.ShowChangeSizeMenu:
                case EditorState.TypeOfAction.HideChangeSizeMenu:
                case EditorState.TypeOfAction.IncreaseSize:
                case EditorState.TypeOfAction.DecreaseSize: return size;
                case EditorState.TypeOfAction.ShowFileMenu:
                case EditorState.TypeOfAction.HideFileMenu:
                case EditorState.TypeOfAction.Save:
                case EditorState.TypeOfAction.SaveAs:
                case EditorState.TypeOfAction.NewLevel:
                case EditorState.TypeOfAction.Load:
                case EditorState.TypeOfAction.Import: return save;
                case EditorState.TypeOfAction.Undo: return undo;
                case EditorState.TypeOfAction.Null: return @null;
                default: return select;
            }
        }
    }
}
