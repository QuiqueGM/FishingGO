using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VFG.LevelEditor
{
    public class UndoModule : MonoBehaviour
    {
        public enum UndoState
        {
            Creating,
            Transforming,
            Scaling,
            Duplicating,
            Freezing,
            Deleting
        }

        public class UndoAction
        {
            public List<UndoData> undoData;
        }

        public class UndoData
        {
            public GameObject name;
            public UndoState state;
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
        }

        public bool showLog;

        private List<UndoAction> listUndos = new List<UndoAction>();
        private List<UndoData> dataAction = new List<UndoData>();
        
        public void AddDataAction(GameObject name, UndoState state)
        {
            Vector3 pos = name.transform.position;
            Quaternion rot = name.transform.rotation;
            Vector3 scl = name.transform.localScale;

            UndoData undoData = new UndoData
            {
                name = name,
                state = state,
                position = pos,
                rotation = rot,
                scale = scl
            };

            dataAction.Add(undoData);

            if (showLog) Debug.Log(string.Format("<color=orange>{0}</color> Object <color=blue>{1}</color> in Pos > {2}, Rot > {3}, Scl > {4} ", name.name, state, pos, rot, scl));
        }

        public void AddUndoAction()
        {
            UndoAction undoAction = new UndoAction() { undoData = dataAction.ToList() };

            listUndos.Add(undoAction);
            dataAction.Clear();
        }

        public void Undo()
        {
            if (listUndos.Count > 0)
            {
                if (showLog) Debug.Log(string.Format("Run UNDO ACTION nº <b>{0}</b>", listUndos.Count));

                List<UndoData> undoData = listUndos.Last().undoData;

                for (int n = 0; n < undoData.Count; n++)
                {
                    if (showLog)  Debug.Log(GetInfoUndo(undoData[n].state, undoData[n].name.gameObject));

                    switch (undoData[n].state)
                    {
                        case UndoState.Creating:
                        case UndoState.Duplicating:
                            undoData[n].name.SetActive(false);
                            break;
                        case UndoState.Transforming:
                        case UndoState.Scaling:
                            undoData[n].name.transform.position = undoData[n].position;
                            undoData[n].name.transform.rotation = undoData[n].rotation;
                            undoData[n].name.transform.localScale = undoData[n].scale;
                            break;
                        case UndoState.Deleting:
                            undoData[n].name.SetActive(true);
                            break;
                    }
                }

                listUndos.Remove(listUndos.Last());
            }
            else
            {
                if (showLog)  Debug.LogError("<color=red>[UNDO MANAGER]</color> There is no more undos!");
            }
        }

        string GetInfoUndo(UndoState state, GameObject go)
        {
            switch (state)
            {
                case UndoState.Creating: return string.Format("<color=blue>DELETING</color> Object <color=orange>{0}</color>", go.name);
                case UndoState.Transforming: return string.Format("<color=blue>MOVING</color> Object <color=orange>{0}</color> to Pos > {1} , Rot > {2}", go.name, go.transform.position, go.transform.rotation);
                case UndoState.Scaling: return string.Format("<color=blue>SCALING</color> Object <color=orange>{0}</color> to Scl > {1}", go.name, go.transform.localScale);
                case UndoState.Deleting: return string.Format("<color=blue>RECOVERING</color> Object <color=orange>{0}</color>", go.name);
                default: return null;
            }
        }
    }
}