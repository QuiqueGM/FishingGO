using UnityEngine;
using VFG.Core;

namespace VFG.LevelEditor
{
    public class ObjectSelector : MonoBehaviour
    {

        [Header("Level Edtor Controller")]
        public LevelEditor levelEditor;

        [Header("Properties")]
        public WheelSelector wheelActions;
        public WheelSelector wheelFreeze;
        public ObjectSelector oppositeSelector;
        public EditorState.ActiveHand hand;
        public MaterialsModule mat;

        private Renderer mesh;
        private EditorState.ActiveHand otherHand;

        #region DEBUG LIST
        private DebugListObjects dlObj;
        #endregion

        private void Awake()
        {
            #region DEBUG LIST
            dlObj = levelEditor.GetComponent<DebugListObjects>();
            #endregion

            otherHand = hand == EditorState.ActiveHand.Left ? EditorState.ActiveHand.Right : EditorState.ActiveHand.Left;
            mesh = GetComponent<MeshRenderer>();
            mat = levelEditor.GetComponent<MaterialsModule>();

            wheelActions.SendActionEvent += ChangeColor;
            wheelFreeze.SendActionEvent += ChangeColor;
        }

        private void OnDestroy()
        {
            wheelActions.SendActionEvent -= ChangeColor;
            wheelFreeze.SendActionEvent -= ChangeColor;
        }

        public void ChangeColor(EditorState.ActiveHand hand)
        {
            mesh.material = mat.GetMaterialFromAction(EditorState.currentAction);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.GetComponent<Renderer>().material.name == "Save (Instance)") return;

            EditorState.collider = other;
            switch (other.gameObject.tag)
            {
                case GameState.ITEM_LIBRARY:  TriggerEnterInLibrary(other);   break;
                case GameState.ITEM_SCENE:    TriggerEnterInScene(other);     break;
            }
        }

        private void TriggerEnterInLibrary(Collider other)
        {
            EditorState.itemSelected.Add(other.gameObject, EditorState.CurrentMaterial);
            EditorState.CurrentMaterial = mat.select;

            #region DEBUG LIST
            dlObj.itemSelected.Add(new ItemListStruct { go = other.gameObject, ma = EditorState.CurrentMaterial });
            #endregion
        }

        private void TriggerEnterInScene(Collider other)
        {
            if (!EditorState.currentItems.ContainsKey(other.gameObject))
            {
                EditorState.currentItems.Add(other.gameObject, EditorState.CurrentMaterial);

                #region DEBUG LIST
                dlObj.currentItems.Add(new ItemListStruct { go = other.gameObject, ma = EditorState.CurrentMaterial });
                #endregion
            }

            if (other.GetComponent<ObjectHandle>().hand == otherHand)
            {
                other.GetComponent<ObjectHandle>().hand = EditorState.ActiveHand.Both;
                ToggleSelectScale(other, EditorState.TypeOfAction.Select, EditorState.TypeOfAction.Scale, mat.scale);
            }
            else
            {
                other.GetComponent<ObjectHandle>().hand = hand;
              
                if (EditorState.freezedItems.ContainsKey(other.gameObject))
                    EditorState.CurrentMaterial = EditorState.currentAction == EditorState.TypeOfAction.Unfreeze ? mat.GetMaterialFromAction(EditorState.TypeOfAction.Unfreeze) : mat.GetMaterialFromAction(EditorState.TypeOfAction.Freeze);
                else if (EditorState.currentAction != EditorState.TypeOfAction.Unfreeze)
                    EditorState.CurrentMaterial = mat.GetMaterialFromAction(EditorState.currentAction);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            EditorState.collider = other;
            switch (other.gameObject.tag)
            {
                case GameState.ITEM_LIBRARY:  TriggerExitInLibrary(other);    break;
                case GameState.ITEM_SCENE:    TriggerExitInScene(other);      break;
            }
        }

        private void TriggerExitInLibrary(Collider other)
        {
            EditorState.CurrentMaterial = EditorState.itemSelected[other.gameObject];
            EditorState.itemSelected.Remove(other.gameObject);

            #region DEBUG LIST
            dlObj.itemSelected.Remove(dlObj.itemSelected.Find(i => i.go == other.gameObject));
            #endregion
        }

        private void TriggerExitInScene(Collider other)
        {
            if (!EditorState.currentItems.ContainsKey(other.gameObject)) return;

            if (other.GetComponent<ObjectHandle>().hand == EditorState.ActiveHand.Both)
            {
                ToggleSelectScale(other, EditorState.TypeOfAction.Scale, EditorState.TypeOfAction.Select, mat.select);

                other.GetComponent<ObjectHandle>().hand = otherHand;
                EditorState.CurrentMaterial = mat.GetMaterialFromAction(EditorState.currentAction);
            }
            else
            {
                other.GetComponent<ObjectHandle>().hand = EditorState.ActiveHand.None;
                EditorState.CurrentMaterial = EditorState.currentItems[other.gameObject];
                EditorState.currentItems.Remove(other.gameObject);

                #region DEBUG LIST
                dlObj.currentItems.Remove(dlObj.currentItems.Find(i => i.go == other.gameObject));
                #endregion
            }
        }

        private void ToggleSelectScale(Collider other, EditorState.TypeOfAction currentAction, EditorState.TypeOfAction newAction, Material newMaterial)
        {
            if (EditorState.currentAction == currentAction)
            {
                EditorState.currentAction = newAction;
                EditorState.CurrentMaterial = newMaterial;
                oppositeSelector.ChangeColor(EditorState.ActiveHand.Both);
                ChangeColor(EditorState.ActiveHand.Both);
            }
        }
    }
}