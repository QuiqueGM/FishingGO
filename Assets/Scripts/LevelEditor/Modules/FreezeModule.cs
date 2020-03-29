using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRMiddlewareController;

namespace VFG.LevelEditor
{
    public class FreezeModule : MonoBehaviour
    {
        #region DEBUG LIST
        private DebugListObjects dlObj;
        private void Awake()
        {
            dlObj = gameObject.GetComponent<DebugListObjects>();
        }
        #endregion

        public void ShowFreezeMenu(Material mat)
        {
            foreach (KeyValuePair<GameObject, Material> obj in EditorState.freezedItems.ToList())
                EditorState.ResetMaterial(obj.Key, mat);
        }

        public void HideFreezeMenu()
        {
            foreach (KeyValuePair<GameObject, Material> obj in EditorState.freezedItems.ToList())
                EditorState.ResetMaterial(obj.Key, obj.Value);
        }

        public void FreezeObject(EventArguments e)
        {
            if (EditorState.currentAction != EditorState.TypeOfAction.Freeze) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems.ToList())
            {
                if ((int)obj.Key.GetComponent<ObjectHandle>().hand == (int)e.hand)
                {
                    EditorState.freezedItems.Add(obj.Key, obj.Value);
                    EditorState.currentItems.Remove(obj.Key);

                    #region DEBUG LIST
                    dlObj.freezedItems.Add(new ItemListStruct { go = obj.Key, ma = obj.Value });
                    dlObj.currentItems.Remove(dlObj.currentItems.Find(i => i.go == obj.Key));
                    #endregion
                }
            }
        }

        public void UnfreezeObject(EventArguments e)
        {
            if (EditorState.currentAction != EditorState.TypeOfAction.Unfreeze) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems.ToList())
            {
                if (EditorState.freezedItems.ContainsKey(obj.Key) && (int)obj.Key.GetComponent<ObjectHandle>().hand == (int)e.hand)
                {
                    EditorState.ResetMaterial(obj.Key, EditorState.freezedItems[obj.Key]);
                    EditorState.freezedItems.Remove(obj.Key);
                    EditorState.currentItems.Remove(obj.Key);

                    #region DEBUG LIST
                    dlObj.freezedItems.Remove(dlObj.freezedItems.Find(i => i.go == obj.Key));
                    dlObj.currentItems.Remove(dlObj.currentItems.Find(i => i.go == obj.Key));
                    #endregion
                }
            }
        }

        public void UnfreezeAllObject()
        {
            foreach (KeyValuePair<GameObject, Material> obj in EditorState.freezedItems)
                EditorState.ResetMaterial(obj.Key, obj.Value);

            EditorState.freezedItems.Clear();

            #region DEBUG LIST
            dlObj.freezedItems.Clear();
            #endregion
        }
    }
}