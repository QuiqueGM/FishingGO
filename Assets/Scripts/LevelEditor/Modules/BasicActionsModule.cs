using System.Collections.Generic;
using UnityEngine;
using VFG.Core;
using VRMiddlewareController;
using System.Linq;

namespace VFG.LevelEditor
{
    public class BasicActionsModule : MonoBehaviour
    {
        private float SCALE_FACTOR = 5;
        //private float MIN_IMPORT_SIZE = 0.25f;

        private GameObject containerItems;
        private UndoModule undoModule;
        private Transform leftSelector;
        private Transform rightSelector;

        private bool flagUndoScale = true;
        private float currentDistance, newCurrentDistance;

        public void Init(GameObject go, Transform ls, Transform rs, UndoModule um)
        {
            containerItems = go;
            leftSelector = ls;
            rightSelector = rs;
            undoModule = um;
        }

        public void CreateNewObject()
        {
            if (EditorState.itemSelected.Count == 0) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.itemSelected)
            {
                string path = string.Format("{0}/{1}/{2}", GameState.PATH_PREFABS, EditorState.GetFolderFromTypeOfItem((TypeOfItem)System.Enum.Parse(typeof(TypeOfItem), EditorState.groupOfItemsToShow)), obj.Key.name);

                GameObject go = Instantiate(Resources.Load(path), rightSelector) as GameObject;
                //go.transform.localScale *= (GameState.collectables.Find(n => n.Id == obj.Key.name).Features.CommonSize / rightSelector.localScale.x / SCALE_FACTOR);
                //if (go.transform.localScale.x <= 0.25f) go.transform.localScale = new Vector3(MIN_IMPORT_SIZE, MIN_IMPORT_SIZE, MIN_IMPORT_SIZE);
                go.transform.localScale *= SCALE_FACTOR;
                go.name = obj.Key.name;
                go.AddComponent<ObjectHandle>();
                go.tag = GameState.ITEM_SCENE;

                undoModule.AddDataAction(go, UndoModule.UndoState.Creating);
            }
        }

        public void TransformObjects(EventArguments e)
        {
            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if ((int)obj.Key.GetComponent<ObjectHandle>().hand == (int)e.hand && obj.Key.transform.parent == containerItems.transform && !EditorState.freezedItems.ContainsKey(obj.Key))
                {
                    undoModule.AddDataAction(obj.Key, UndoModule.UndoState.Transforming);
                    obj.Key.transform.parent = obj.Key.GetComponent<ObjectHandle>().hand == EditorState.ActiveHand.Left ? leftSelector : rightSelector;
                }
            }
        }

        public void SetCurrentDistance()
        {
            currentDistance = Vector3.Distance(leftSelector.position, rightSelector.position);
        }

        public void ScaleObject(bool leftTrigger, bool rightTrigger)
        {
            if (!leftTrigger || !rightTrigger || EditorState.currentAction != EditorState.TypeOfAction.Scale) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (obj.Key.GetComponent<ObjectHandle>().hand != EditorState.ActiveHand.Both || EditorState.freezedItems.ContainsKey(obj.Key)) continue;

                newCurrentDistance = Vector3.Distance(leftSelector.position, rightSelector.position);
                obj.Key.transform.localScale *= newCurrentDistance / currentDistance;
                currentDistance = newCurrentDistance;

                if (flagUndoScale)
                {
                    flagUndoScale = !flagUndoScale;
                    undoModule.AddDataAction(obj.Key, UndoModule.UndoState.Scaling);
                }
            }
        }

        public void DeleteObjects(EventArguments e)
        {
            if (EditorState.currentAction != EditorState.TypeOfAction.Delete) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (EditorState.freezedItems.ContainsKey(obj.Key)) continue;

                obj.Key.gameObject.SetActive(false);
                EditorState.ResetMaterial(obj.Key, obj.Value);

                undoModule.AddDataAction(obj.Key, UndoModule.UndoState.Deleting);
            }

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems.ToList())
            {
                if (EditorState.freezedItems.ContainsKey(obj.Key)) continue;

                EditorState.currentItems.Remove(obj.Key);
                undoModule.AddUndoAction();
            }
        }

        public void DuplicateObjects(EventArguments e)
        {
            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (EditorState.freezedItems.ContainsKey(obj.Key)) continue;

                if ((int)obj.Key.GetComponent<ObjectHandle>().hand == (int)e.hand)
                {
                    GameObject go = Instantiate(obj.Key.gameObject) as GameObject;
                    go.transform.localPosition = obj.Key.gameObject.transform.position;
                    go.transform.localRotation = obj.Key.gameObject.transform.localRotation;
                    go.transform.localScale = obj.Key.gameObject.transform.localScale;
                    go.transform.parent = obj.Key.GetComponent<ObjectHandle>().hand == EditorState.ActiveHand.Left ? leftSelector : rightSelector;
                    go.name = obj.Key.name;

                    EditorState.ResetMaterial(go, obj.Value);
                    undoModule.AddDataAction(go, UndoModule.UndoState.Duplicating);
                }
            }
        }

        public void ReleaseObject(Transform controller)
        {
            if (EditorState.currentItems.Count == 0) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (controller.Find(obj.Key.name))
                {
                    EditorState.ResetMaterial(obj.Key, obj.Value);
                    obj.Key.transform.parent = containerItems.transform;
                }
            }

            undoModule.AddUndoAction();
            flagUndoScale = true;
        }

        public void UndoAction()
        {
            undoModule.Undo();
        }
    }
}
