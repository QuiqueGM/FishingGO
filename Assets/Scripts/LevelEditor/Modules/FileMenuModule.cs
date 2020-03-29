using SimpleJSON;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VFG.Canvas;
using VFG.Canvas.LevelEditor;
using VFG.Core;
using VFG.Models;

namespace VFG.LevelEditor
{
    public class FileMenuModule : MonoBehaviour
    {
        #region DEBUG LIST
        private DebugListObjects dlObj;
        #endregion

        public FileDialog fileDialog;
        public KeyboardMenu keyboard;
        public WarningDialog replaceDialog;
        public WarningDialog noExistDialog;
        public BaseDialog newLevelDialog;

        private GameObject containerItems;
        [HideInInspector]
        public bool cleanSceneAfterSave; // Has to be private 
        [HideInInspector]
        public bool openDialogAfterSave; // Has to be private 
        private List<ItemEditor> itemsLevel = new List<ItemEditor>();
        private string str = string.Empty;

        public void Init(GameObject go)
        {
            #region DEBUG LIST
            dlObj = gameObject.GetComponent<DebugListObjects>();
            #endregion

            fileDialog.Init();
            keyboard.Init();
            //replaceDialog.Init();
            noExistDialog.Init();
            containerItems = go;
        }

        public void Save()
        {
            if (FileDialog.NameLevel == null || FileDialog.NameLevel == "" || FileDialog.NameLevel == string.Empty)
            {
                fileDialog.OpenDialog(LvlEditorAction.Save);
                keyboard.SetActive(true);
            }
            else
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath, FileDialog.NameLevel + EditorState.LEVEL_EXTENSION), WriteStringToJSON());
                Debug.Log(string.Format("<color=#1bc71b>[Saving...]</color> Level <color=cyan>{0}</color> succesfully saved!", FileDialog.NameLevel));

                if (cleanSceneAfterSave) ResetScene();
                if (openDialogAfterSave) Load();
            }
        }

        public void SaveAs()
        {
            fileDialog.OpenDialog(LvlEditorAction.Save);
        }

        public void NewLevel()
        {
            if (GetCurrentObjectsInScene().Count == 0)
            {
                FileDialog.NameLevel = string.Empty;
                return;
            }

            cleanSceneAfterSave = true;
            newLevelDialog.SetActive(true);
        }

        public void CancelLevelModified()
        {
            cleanSceneAfterSave = false;
            openDialogAfterSave = false;
        }

        public void Cancel()
        {
            CancelLevelModified();
        }

        public void Load()
        {
            if (GetCurrentObjectsInScene().Count > 0)
            {
                cleanSceneAfterSave = true;
                openDialogAfterSave = true;
                newLevelDialog.SetActive(true);
            }
            else
                fileDialog.OpenDialog(LvlEditorAction.Load);
        }

        public void OpenLevel()
        {
            openDialogAfterSave = false;
            LoadJSON(FileDialog.NameLevel);
        }

        public void LoadJSON(string levelToLoad)
        {
            string strTmp = File.ReadAllText(Path.Combine(Application.persistentDataPath, levelToLoad + EditorState.LEVEL_EXTENSION));
            JSONNode nodoJSON = JSON.Parse(strTmp);

            if (nodoJSON == null) return;

            itemsLevel.Clear();

            for (int i = 0; i < nodoJSON.Count; i++)
                itemsLevel.Add(GetItemFromJSON(nodoJSON, i));

            for (int l = 0; l < itemsLevel.Count; l++)
                CreateItem(itemsLevel[l]);
        }

        private void CreateItem(ItemEditor item)
        {
            string path = string.Format("{0}/{1}/{2}", GameState.PATH_PREFABS, EditorState.GetFolderFromTypeOfItem(item.TypeOfItem), item.Id);

            GameObject go = Instantiate(Resources.Load(path), containerItems.transform) as GameObject;
            go.transform.position = item.Position;
            go.transform.rotation = item.Rotation;
            go.transform.localScale = item.Scale;
            go.name = item.Id;
            go.AddComponent<ObjectHandle>();
            go.tag = GameState.ITEM_SCENE;

            EditorState.collider = go.GetComponent<Collider>();

            if (item.IsFreezed)
            { 
                EditorState.freezedItems.Add(go, EditorState.CurrentMaterial);
                #region DEBUG LIST
                dlObj.freezedItems.Add(new ItemListStruct { go = go, ma = EditorState.CurrentMaterial });
                #endregion
            }
        }

        public void Import()
        {
            fileDialog.OpenDialog(LvlEditorAction.Import);
        }

        public void SendTriggerAction(GameObject objInFocus)
        {
            if (objInFocus != null && objInFocus.tag == "Button")
                objInFocus.GetComponent<VFG.Canvas.LevelEditor.BaseButton>().TriggerIsPressed();
        }

        private string WriteStringToJSON()
        {
            string str = string.Empty;

            List<GameObject> go = GetCurrentObjectsInScene();

            if (go.Count > 0)
            { 
                str = "[";

                for (int n = 0; n < go.Count; n++)
                {
                    string item = string.Format
                    (
                        "{{\"IdItem\":\"{0}\",\"TypeOf\":\"{11}\",\"IsFreezed\":{12},\"Transform\":[{{\"Position\":[{{\"x\":{1},\"y\":{2},\"z\":{3}}}],\"Rotation\":[{{\"x\":{4},\"y\":{5},\"z\":{6},\"w\":{7}}}],\"Scale\":[{{\"x\":{8},\"y\":{9},\"z\":{10}}}]}}]}}",
                        go[n].name,
                        go[n].transform.position.x,
                        go[n].transform.position.y,
                        go[n].transform.position.z,
                        go[n].transform.rotation.x,
                        go[n].transform.rotation.y,
                        go[n].transform.rotation.z,
                        go[n].transform.rotation.w,
                        go[n].transform.localScale.x,
                        go[n].transform.localScale.y,
                        go[n].transform.localScale.z,
                        go[n].GetComponent<ItemClassification>().typeOfItem,
                        EditorState.freezedItems.ContainsKey(go[n]) ? "true" : "false"
                    );

                    str = string.Format("{0}{1},", str, item);
                }

                str = string.Format("{0}]", str.Substring(0, str.Length - 1));
            }

            return str;
        }

        private ItemEditor GetItemFromJSON(JSONNode node, int n)
        {
            ItemEditor objective = new ItemEditor();

            objective.Id = node[n]["IdItem"].Value;
            objective.TypeOfItem = (TypeOfItem)Enum.Parse(typeof(TypeOfItem), node[n]["TypeOf"].Value);
            objective.IsFreezed = node[n]["IsFreezed"].AsBool;
            objective.Position = new Vector3 (node[n]["Transform"][0]["Position"][0]["x"].AsFloat, node[n]["Transform"][0]["Position"][0]["y"].AsFloat, node[n]["Transform"][0]["Position"][0]["z"].AsFloat);
            objective.Rotation = new Quaternion (node[n]["Transform"][0]["Rotation"][0]["x"].AsFloat, node[n]["Transform"][0]["Rotation"][0]["y"].AsFloat, node[n]["Transform"][0]["Rotation"][0]["z"].AsFloat, node[n]["Transform"][0]["Rotation"][0]["w"].AsFloat);
            objective.Scale = new Vector3 (node[n]["Transform"][0]["Scale"][0]["x"].AsFloat, node[n]["Transform"][0]["Scale"][0]["y"].AsFloat, node[n]["Transform"][0]["Scale"][0]["z"].AsFloat );
            
            return objective;
        }

        public void ResetScene()
        {
            cleanSceneAfterSave = false;

            EditorState.itemSelected.Clear();
            EditorState.currentItems.Clear();
            EditorState.freezedItems.Clear();

            fileDialog.ClearName();
            keyboard.NameScene = string.Empty;

            GameObject go = new GameObject();
            int numChilds = containerItems.transform.childCount;

            for (int g = 0; g < numChilds; g++)
                containerItems.transform.GetChild(0).gameObject.transform.parent = go.transform;

            Destroy(go);
        }

        private List<GameObject> GetCurrentObjectsInScene()
        {
            List<GameObject> go = new List<GameObject>();

            for (int g = 0; g < containerItems.transform.childCount; g++)
                if (containerItems.transform.GetChild(g).gameObject.activeSelf)
                    go.Add(containerItems.transform.GetChild(g).gameObject);

            return go;
        }

        public void Delete()
        {
            string strTmp = Path.Combine(Application.persistentDataPath, FileDialog.NameLevel + EditorState.LEVEL_EXTENSION);
            File.Delete(strTmp);
        }

        public void Rename(string lvl)
        {
            string oldLvl = Path.Combine(Application.persistentDataPath, FileDialog.NameLevel + EditorState.LEVEL_EXTENSION);
            string newLvl = Path.Combine(Application.persistentDataPath, lvl + EditorState.LEVEL_EXTENSION);
            File.Move(oldLvl, newLvl);
        }
    }
}
