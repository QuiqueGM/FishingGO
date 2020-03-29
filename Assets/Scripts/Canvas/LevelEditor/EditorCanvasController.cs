using TMPro;
using UnityEngine;
using VFG.LevelEditor;

namespace VFG.Canvas.LevelEditor
{
    public class EditorCanvasController : MonoBehaviour
    {
        #region DEBUG TEST
        [Header("Debug")]
        public TMP_Text DG_NameLevel;
        public TMP_Text DG_NameAuxLevel;
        public TMP_Text DG_CleanSceneAfterSave;
        public TMP_Text DG_OpenDialogAfterSave;
        public FileMenuModule fmm;
        #endregion

        public VFG.LevelEditor.LevelEditor levelEditor;
        public ButtonDetection buttonDetection;

        private FileMenuModule fileMenuModule;
        private GameObject containerItems;

        private FileDialog fileDialog;
        private KeyboardMenu keyboard;
        private WarningDialog replaceDialog;
        private BaseDialog levelModifiedDialog;
        private BaseDialog cantDeleteFileDialog;
        private WarningDialog fileNoExistDialog;
        private WarningDialog deleteDialog;
        private WarningDialog renameDialog;

        private bool isRenaming;

        void Awake()
        {
            Initialize();
            AddListeners();
        }

        #region DEBUG TEST
        private void Update()
        {
            DG_NameLevel.text = "Name Level: " + FileDialog.NameLevel;
            DG_NameAuxLevel.text = "Name Level AUX: " + FileDialog.NameAux;
            DG_CleanSceneAfterSave.text = "Clean Scene: " + fmm.cleanSceneAfterSave;
            DG_OpenDialogAfterSave.text = "Open Dialog: " + fmm.openDialogAfterSave;
        }
        #endregion

        private void Initialize()
        {
            fileMenuModule = levelEditor.GetComponent<FileMenuModule>();
            containerItems = levelEditor.transform.Find("ContainerItems").gameObject;
            fileDialog = transform.Find("FileDialog").GetComponent<FileDialog>();
            keyboard = transform.Find("Keyboard").GetComponent<KeyboardMenu>();
            replaceDialog = transform.Find("ReplaceDialog").GetComponent<WarningDialog>();
            levelModifiedDialog = transform.Find("LevelModifiedDialog").GetComponent<BaseDialog>();
            fileNoExistDialog = transform.Find("NoExistDialog").GetComponent<WarningDialog>();
            deleteDialog = transform.Find("DeleteDialog").GetComponent<WarningDialog>();
            cantDeleteFileDialog = transform.Find("CantDeleteFileDialog").GetComponent<BaseDialog>();
            renameDialog = transform.Find("RenameDialog").GetComponent<WarningDialog>();
        }

        private void AddListeners()
        {
            fileDialog.windowStateEvent += CheckLaserVisivility;
            levelModifiedDialog.windowStateEvent += CheckLaserVisivility;
        }

        private void OnDestroy()
        {
            fileDialog.windowStateEvent -= CheckLaserVisivility;
            levelModifiedDialog.windowStateEvent -= CheckLaserVisivility;
        }

        public void GoToSelectedMenu(LvlEditorAction action)
        {
            switch (action)
            {
                case LvlEditorAction.Save: Save(); break;
                case LvlEditorAction.Replace: Replace(); break;
                case LvlEditorAction.CloseReplace: CloseWarningDialog(replaceDialog); break;
                case LvlEditorAction.YESLevelModified: YESLevelModified(); break;
                case LvlEditorAction.NOLevelModified: NOLevelModified(); break;
                case LvlEditorAction.CancelevelModified: CANCELLevelModified(); break;
                case LvlEditorAction.Cancel: Cancel(); break;
                case LvlEditorAction.Load: Load(); break;
                case LvlEditorAction.Import: Import(); break;
                case LvlEditorAction.Select: fileDialog.Select(); break;
                case LvlEditorAction.ShowKeyboard: ShowKeyboard(false); break;
                case LvlEditorAction.CloseKeyboard: keyboard.SetActive(false); break;
                case LvlEditorAction.Intro: IntroKeyboard(); break;
                case LvlEditorAction.ArrowDown: fileDialog.MoveList(FileDialog.TypeOfArrow.Down); break;
                case LvlEditorAction.ArrowUp: fileDialog.MoveList(FileDialog.TypeOfArrow.Up); break;
                case LvlEditorAction.CloseFileNoExist: CloseWarningDialog(fileNoExistDialog); break;
                case LvlEditorAction.Delete: Delete(); break;
                case LvlEditorAction.DeleteNO: CloseWarningDialog(deleteDialog); break;
                case LvlEditorAction.DeleteYES: DeleteYES(); break;
                case LvlEditorAction.CantDelete: cantDeleteFileDialog.SetActive(false); break;
                case LvlEditorAction.Rename: ShowKeyboard(true); break;
                case LvlEditorAction.RenameClose: CloseWarningDialog(renameDialog); break;
            }
        }

        private void Save()
        {
            if (FileDialog.NameLevel == null || FileDialog.NameLevel == "" || FileDialog.NameLevel == string.Empty)
            {
                keyboard.SetActive(true);
                return;
            }

            if (fileDialog.IfFileExist(FileDialog.NameLevel))
                replaceDialog.Populate(FileDialog.NameLevel);
            else
            {
                fileDialog.SetActive(false);
                fileMenuModule.Save();
            }
        }

        private void ShowKeyboard(bool isRenaming)
        {
            this.isRenaming = isRenaming;
            keyboard.SetText();
            keyboard.SetActive(true);
        }

        private void Replace()
        {
            replaceDialog.ResetKey();
            fileMenuModule.Save();
            replaceDialog.SetActive(false);
            fileDialog.SetActive(false);
            
            if (fileMenuModule.openDialogAfterSave)
                fileDialog.OpenDialog(LvlEditorAction.Load);
        }

        private void CloseWarningDialog(WarningDialog wd)
        {
            wd.SetActive(false);
            wd.ResetKey();
        }

        private void YESLevelModified()
        {
            levelModifiedDialog.SetActive(false);
            fileMenuModule.Save();
        }

        private void NOLevelModified()
        {
            fileMenuModule.ResetScene();
            levelModifiedDialog.SetActive(false);

            if (fileMenuModule.openDialogAfterSave)
                fileDialog.OpenDialog(LvlEditorAction.Load);
        }

        private void CANCELLevelModified()
        {
            fileMenuModule.Cancel();
            levelModifiedDialog.SetActive(false);
        }

        private void Load()
        {
            if (FileDialog.NameLevel == null || FileDialog.NameLevel == "" || FileDialog.NameLevel == string.Empty) return;

            if (fileDialog.IfFileExist(FileDialog.NameLevel))
            {
                fileDialog.SetActive(false);
                fileMenuModule.OpenLevel();
            }
            else
                fileNoExistDialog.Populate(FileDialog.NameLevel);
        }

        private void Cancel()
        {
            fileMenuModule.Cancel();
            fileDialog.Cancel();
        }

        private void IntroKeyboard()
        {
            if (isRenaming)
            {
                if (fileDialog.IfFileExist(keyboard.NameScene))
                {
                    renameDialog.Populate(keyboard.NameScene);
                    return;
                }
                else
                {
                    fileDialog.Rename(keyboard.NameScene);
                    fileMenuModule.Rename(keyboard.NameScene);
                }
            }

            keyboard.Intro();
            fileDialog.Select();
        }

        private void Import()
        {
            string level = FileDialog.NameLevel;
            fileDialog.Cancel();
            fileMenuModule.LoadJSON(level);
        }

        private void CheckLaserVisivility()
        {
            float LASER_THICKNESS_ON = 0.002f;

            bool state = false;

            for (int n = 0; n < gameObject.transform.childCount; n++)
            {
                if (gameObject.transform.GetChild(n).gameObject.activeSelf)
                {
                    state = true;
                    break;
                }
                else state = false;
            }

            buttonDetection.thickness = state ? LASER_THICKNESS_ON : 0;
            containerItems.SetActive(!state);
            levelEditor.FileDialogIsOpen = state;
        }

        private void Delete()
        {
            fileDialog.Select();

            if (FileDialog.NameLevel == FileDialog.NameAux)
            {
                cantDeleteFileDialog.SetActive(true);
                return;
            }

            deleteDialog.Populate(FileDialog.NameLevel);
        }

        private void DeleteYES()
        {
            fileMenuModule.Delete();
            fileDialog.Delete();
            deleteDialog.SetActive(false);
            deleteDialog.ResetKey();
        }
    }
}