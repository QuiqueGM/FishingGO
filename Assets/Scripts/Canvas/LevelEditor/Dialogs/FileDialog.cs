using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VFG.LevelEditor;

namespace VFG.Canvas.LevelEditor
{
    public class FileDialog : BaseDialog
    {
        public enum TypeOfArrow { Up, Down }
        public static string NameLevel = string.Empty;
        public static string NameAux;

        public GameObject buttonLevel;
        public GameObject levelsContainer;
        public TMP_Text TXT_Scene;
        [Space]
        public GameObject BTN_Save;
        public GameObject BTN_Load;
        public GameObject BTN_Import;
        public Scrollbar scroll;
        
        private float SBIncrement;
        private TMP_InputField input;
        public List<GameObject> buttonsLevel = new List<GameObject>();
        public List<string> levels = new List<string>();

        public void OpenDialog(LvlEditorAction action)
        {
            LoadLevels();
            
            switch (action)
            {
                case LvlEditorAction.Save: SetButtonsBehaviour(BTN_Save, BTN_Load, BTN_Import); break;
                case LvlEditorAction.Load: SetButtonsBehaviour(BTN_Load, BTN_Save, BTN_Import); break;
                case LvlEditorAction.Import: SetButtonsBehaviour(BTN_Import, BTN_Save, BTN_Load); break;
            }

            SetActive(true);
            NameAux = NameLevel;
            ClearName();
        }

        private void SetButtonsBehaviour(params GameObject[] btn)
        {
            foreach (GameObject go in btn)
                SetButtonProperties(go, true, 0.5f);

            SetButtonProperties(btn[0], false, 1);
        }

        private void SetButtonProperties(GameObject go, bool state, float alpha)
        {
            go.GetComponent<BaseButton>().isBlocked = state;
            go.GetComponent<CanvasGroup>().alpha = alpha;
        }

        private void LoadLevels()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
            List<FileInfo> lvls = dir.GetFiles(EditorState.ALL_FILES + EditorState.LEVEL_EXTENSION).ToList();

            levels.Clear();
            buttonsLevel.Clear();

            foreach (Transform t in levelsContainer.transform)
                Destroy(t.gameObject);

            foreach (var l in lvls)
            {
                GameObject newLevel = Instantiate(buttonLevel, levelsContainer.transform);
                newLevel.transform.localPosition = Vector3.zero;
                newLevel.transform.localScale = Vector3.one;
                buttonsLevel.Add(newLevel);

                string name = l.Name.Substring(0, l.Name.Length - 5);
                newLevel.GetComponent<ButtonLevel>().SetButton(name);
                levels.Add(name);
            }

            SBIncrement = GetScrollbarIncrement();
        }

        public void Select()
        {
            TXT_Scene.text = NameLevel;
        }

        public void ClearName()
        {
            NameLevel = string.Empty;
            Select();
        }

        public bool IfFileExist(string lvl)
        {
            return levels.Contains(lvl);
        }

        public void MoveList(TypeOfArrow direction)
        {
            scroll.value += direction == TypeOfArrow.Up ? SBIncrement : -SBIncrement;
        }

        private float GetScrollbarIncrement()
        {
            int NUM_VISIBLE_LEVELS_IN_FILE_DIALOG_LIST = 7;
            return levels.Count > NUM_VISIBLE_LEVELS_IN_FILE_DIALOG_LIST ? 1.0f / (levels.Count - NUM_VISIBLE_LEVELS_IN_FILE_DIALOG_LIST) : 0;
        }

        public void Cancel()
        {
            NameLevel = NameAux;
            SetActive(false);
        }

        public void Delete()
        {
            foreach (Transform t in levelsContainer.transform)
            {
                if (t.gameObject.name == NameLevel)
                {
                    Destroy(t.gameObject);
                    break;
                }
            }

            levels.Remove(NameLevel);
            buttonsLevel.Remove((buttonsLevel.Find(i => i.name == NameLevel)));
        }

        public void Rename(string name)
        {
            levels.Remove(NameLevel);
            levels.Add(name);
            buttonsLevel.Find(i => i.name == NameLevel).GetComponent<ButtonLevel>().SetButton(name);
        }
    }
}