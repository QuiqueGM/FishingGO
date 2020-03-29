using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFG.Core;

namespace VFG.LevelEditor
{
    public class ObjectSelector : MonoBehaviour
    {
        public WheelSelector wheelActions;
        public ObjectSelector oppositeSelector;
        public EditorState.ActiveHand hand;

        [Space]
        public Material select;
        public Material scale;
        public Material duplicate;
        public Material delete;
        public Material size;
        public Material save;

        private Renderer mesh;
        private EditorState.ActiveHand otherHand;

        private void Awake()
        {
            otherHand = hand == EditorState.ActiveHand.Left ? EditorState.ActiveHand.Right : EditorState.ActiveHand.Left;
            mesh = GetComponent<MeshRenderer>();
            wheelActions.SendActionEvent += ChangeColor;
        }

        private void OnDestroy()
        {
            wheelActions.SendActionEvent -= ChangeColor;
        }

        public void ChangeColor(EditorState.ActiveHand hand)
        {
            mesh.material = GetMaterialFromAction(EditorState.currentAction);
        }

        public Material GetMaterialFromAction(EditorState.TypeOfAction action)
        {
            switch (action)
            {
                case EditorState.TypeOfAction.Select: return select;
                case EditorState.TypeOfAction.Scale: return scale;
                case EditorState.TypeOfAction.Duplicate: return duplicate;
                case EditorState.TypeOfAction.Delete: return delete;
                case EditorState.TypeOfAction.IncreaseSize: return size;
                case EditorState.TypeOfAction.DecreaseSize: return size;
                case EditorState.TypeOfAction.Save: return save;
                default: return select;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case GameState.ITEM_LIBRARY:  TriggerEnterInLibrary(other);   break;
                case GameState.ITEM_SCENE:    TriggerEnterInScene(other);     break;
            }
        }

        private void TriggerEnterInLibrary(Collider other)
        {
            EditorState.itemSelected.Add(other.gameObject, other.GetComponent<Renderer>().material);
            other.GetComponent<Renderer>().material = select;
        }

        private void TriggerEnterInScene(Collider other)
        {
            if (!EditorState.currentItems.ContainsKey(other.gameObject))
                EditorState.currentItems.Add(other.gameObject, other.GetComponent<Renderer>().material);

            if (other.GetComponent<ObjectHandle>().hand == otherHand)
            {
                other.GetComponent<ObjectHandle>().hand = EditorState.ActiveHand.Both;
                ToggleSelectScale(other, EditorState.TypeOfAction.Select, EditorState.TypeOfAction.Scale, scale);
            }
            else
            {
                other.GetComponent<ObjectHandle>().hand = hand;
                other.GetComponent<Renderer>().material = GetMaterialFromAction(EditorState.currentAction);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case GameState.ITEM_LIBRARY:  TriggerExitInLibrary(other);    break;
                case GameState.ITEM_SCENE:    TriggerExitInScene(other);      break;
            }
        }

        private void TriggerExitInLibrary(Collider other)
        {
            other.GetComponent<Renderer>().material = EditorState.itemSelected[other.gameObject];
            EditorState.itemSelected.Remove(other.gameObject);
        }

        private void TriggerExitInScene(Collider other)
        {
            if (!EditorState.currentItems.ContainsKey(other.gameObject)) return;

            if (other.GetComponent<ObjectHandle>().hand == EditorState.ActiveHand.Both)
            {
                ToggleSelectScale(other, EditorState.TypeOfAction.Scale, EditorState.TypeOfAction.Select, select);

                other.GetComponent<ObjectHandle>().hand = otherHand;
                other.GetComponent<Renderer>().material = GetMaterialFromAction(EditorState.currentAction);
            }
            else
            {
                other.GetComponent<ObjectHandle>().hand = EditorState.ActiveHand.None;
                other.GetComponent<Renderer>().material = EditorState.currentItems[other.gameObject];
                EditorState.currentItems.Remove(other.gameObject);
            }
        }

        private void ToggleSelectScale(Collider other, EditorState.TypeOfAction currentAction, EditorState.TypeOfAction newAction, Material newMaterial)
        {
            if (EditorState.currentAction == currentAction)
            {
                EditorState.currentAction = newAction;
                other.GetComponent<Renderer>().material = newMaterial;
                oppositeSelector.ChangeColor(EditorState.ActiveHand.Both);
                ChangeColor(EditorState.ActiveHand.Both);
            }
        }
    }
}