using System;
using System.Collections.Generic;
using UnityEngine;
using VFG.Canvas.LevelEditor;
using VFG.Core.Audio;
using VRMiddlewareController;

namespace VFG.LevelEditor
{
    public class LevelEditor : MiddlewareController
    {
        [Space]
        [Header("Properties")]
        public GameObject wheelMenuActions;
        public GameObject wheelMenuFreeze;
        public GameObject wheelMenuChangeSize;
        public GameObject wheelMenuFile;
        public GameObject wheelItemsSelector;
        public GameObject menuItems;
        [Space]
        public GameObject containerItems;

        private Transform leftSelector;
        private Transform rightSelector;

        private WheelSelector wheelActions;
        private WheelSelector wheelFrezze;
        private WheelSelector wheelChangeSize;
        private WheelSelector wheelFile;
        private WheelSelector wheelItems;
        private GameObject currentWheel;
        private MenuItems menuItemsScrips;
        
        private bool LTriggerIsPressed, RTriggerIsPressed;
        private bool wheelItemsIsEnabled = false;
        private bool wheelFileIsEnabled = false;
        public bool FileDialogIsOpen { get; set; }

        private BasicActionsModule basicActionsModule;
        private UndoModule undoModule;
        private FreezeModule freezeModule;
        private SizeModule sizeModule;
        private FlyingModule flyingModule;
        private MaterialsModule matModule;
        private FileMenuModule fileModule;

        #region LIFE CICLE

        public override void Awake()
        {
            base.Awake();

            InitControllers();
            InitModules();
            InitWheels();
            AddListeners();
        }

        private void InitControllers()
        {
            leftSelector = leftController.transform.Find("Reference/ObjectSelector");
            rightSelector = rightController.transform.Find("Reference/ObjectSelector");
        }

        private void InitModules()
        {
            basicActionsModule = GetComponent<BasicActionsModule>();
            undoModule = GetComponent<UndoModule>();
            sizeModule = GetComponent<SizeModule>();
            freezeModule = GetComponent<FreezeModule>();
            flyingModule = GetComponent<FlyingModule>();
            matModule = GetComponent<MaterialsModule>();
            fileModule = GetComponent<FileMenuModule>();

            basicActionsModule.Init(containerItems, leftSelector, rightSelector, undoModule);
            fileModule.Init(containerItems);
        }

        private void InitWheels()
        {
            wheelItems = wheelItemsSelector.GetComponent<WheelSelector>();
            wheelFrezze = wheelMenuFreeze.GetComponent<WheelSelector>();
            wheelActions = wheelMenuActions.GetComponent<WheelSelector>();
            wheelChangeSize = wheelMenuChangeSize.GetComponent<WheelSelector>();
            wheelFile = wheelMenuFile.GetComponent<WheelSelector>();

            currentWheel = wheelMenuActions;
        }

        private void AddListeners()
        {
            wheelActions.SendActionEvent += SetVibration;
            wheelFrezze.SendActionEvent += SetVibration;
            wheelItems.SendActionEvent += SetVibration;
            wheelFile.SendActionEvent += SetVibration;
            wheelChangeSize.SendActionEvent += SetVibration;
        }

        private void OnDestroy()
        {
            wheelActions.SendActionEvent -= SetVibration;
            wheelFrezze.SendActionEvent -= SetVibration;
            wheelItems.SendActionEvent -= SetVibration;
            wheelFile.SendActionEvent -= SetVibration;
            wheelChangeSize.SendActionEvent -= SetVibration;
        }

        private void Update()
        {
            //Debug.Log("Action: " + EditorState.currentAction);
            //Debug.Log("RTriggerIsPressed: " + RTriggerIsPressed);
            //Debug.Log("LTriggerIsPressed: " + LTriggerIsPressed);
        }

        #endregion

        #region CONTROLLER BEHAVIOUR

        public override void LPadTouched(SenderInfo sender, EventArguments e)
        {
            if (wheelFileIsEnabled) return;

            wheelItemsSelector.SetActive(true);
            menuItemsScrips = menuItems.GetComponent<MenuItems>();
            AudioManager.Instance.PlayEffect(AudiosData.WHEEL_SELECTOR);
        }

        public override void LPadTouchedHolded(SenderInfo sender, EventArguments e)
        {
            wheelItems.SetTexture(GetAngle(e));
            wheelItemsIsEnabled = true;
            CurrentWheelVisibility(false);
            wheelActions.SetTexture(90);
        }

        public override void LPadUntouched(SenderInfo sender, EventArguments e)
        {
            wheelItemsSelector.SetActive(false);
            menuItems.SetActive(false);
            wheelItemsIsEnabled = false;
        }

        public override void LPadClicked(SenderInfo sender, EventArguments e)
        {
            wheelItemsSelector.SetActive(false);
            menuItemsScrips.Populate();
        }

        public override void RPadClicked(SenderInfo sender, EventArguments e)
        {
            switch (EditorState.currentAction)
            {
                case EditorState.TypeOfAction.Undo: basicActionsModule.UndoAction(); break;
                case EditorState.TypeOfAction.ShowFreezeMenu: ShowFreezeMenu(); break;
                case EditorState.TypeOfAction.HideFreezeMenu: HideFreezeMenu(); break;
                case EditorState.TypeOfAction.UnfreezeAll: freezeModule.UnfreezeAllObject(); break;
                case EditorState.TypeOfAction.ShowChangeSizeMenu: ShowChangeSizeMenu(); break;
                case EditorState.TypeOfAction.HideChangeSizeMenu: HideChangeSizeMenu(); break;
                case EditorState.TypeOfAction.ShowFileMenu: ShowFileMenu(); break;
                case EditorState.TypeOfAction.HideFileMenu: HideFileMenu(); break;
                case EditorState.TypeOfAction.NewLevel: if (!FileDialogIsOpen) fileModule.NewLevel(); break;
                case EditorState.TypeOfAction.Save: if (!FileDialogIsOpen) fileModule.Save(); break;
                case EditorState.TypeOfAction.SaveAs: if (!FileDialogIsOpen) fileModule.SaveAs(); break;
                case EditorState.TypeOfAction.Load: if (!FileDialogIsOpen) fileModule.Load(); break;
                case EditorState.TypeOfAction.Import: if (!FileDialogIsOpen) fileModule.Import(); break;
            }
        }

        public override void RPadTouched(SenderInfo sender, EventArguments e)
        {
            if (!wheelItemsIsEnabled)
            {
                CurrentWheelVisibility(true);
                AudioManager.Instance.PlayEffect(AudiosData.WHEEL_SELECTOR);
            }
        }

        public override void RPadTouchedHolded(SenderInfo sender, EventArguments e)
        {
            if (!wheelItemsIsEnabled)
                currentWheel.GetComponent<WheelActions>().SetTexture(GetAngle(e));
        }

        public override void RPadUntouched(SenderInfo sender, EventArguments e)
        {
            CurrentWheelVisibility(false);

            //if (EditorState.currentAction == EditorState.TypeOfAction.Undo || EditorState.currentAction == EditorState.TypeOfAction.Save)
            //    wheelActions.SetTexture(90);
        }

        public override void RPadHolded(SenderInfo sender, EventArguments e)
        {
            sizeModule.ChangeSize(leftSelector, rightSelector);
        }

        public override void RTriggerPressed(SenderInfo sender, EventArguments e)
        {
            RTriggerIsPressed = true;

            switch (EditorState.currentAction)
            {
                case EditorState.TypeOfAction.Save:
                case EditorState.TypeOfAction.SaveAs:
                case EditorState.TypeOfAction.Load:
                case EditorState.TypeOfAction.NewLevel:
                case EditorState.TypeOfAction.Import:
                case EditorState.TypeOfAction.HideFileMenu: fileModule.SendTriggerAction(rightController.GetComponent<ButtonDetection>().objectInFocus); break;
                case EditorState.TypeOfAction.Select: basicActionsModule.CreateNewObject(); break;
            }
        }

        public override void LTriggerPressed(SenderInfo sender, EventArguments e)
        {
            LTriggerIsPressed = true;
        }

        public override void TriggerPressed(SenderInfo sender, EventArguments e)
        {
            if (EditorState.currentItems.Count == 0) return;

            switch (EditorState.currentAction)
            {
                case EditorState.TypeOfAction.Select: basicActionsModule.TransformObjects(e); break;
                case EditorState.TypeOfAction.Scale: basicActionsModule.SetCurrentDistance(); break;
                case EditorState.TypeOfAction.Delete: basicActionsModule.DeleteObjects(e); break;
                case EditorState.TypeOfAction.Duplicate: basicActionsModule.DuplicateObjects(e); break;
                case EditorState.TypeOfAction.Freeze: freezeModule.FreezeObject(e); break;
                case EditorState.TypeOfAction.Unfreeze: freezeModule.UnfreezeObject(e); break;
            }
        }

        public override void TriggerTouched(SenderInfo sender, EventArguments e)
        {
            basicActionsModule.DeleteObjects(e);
            basicActionsModule.ScaleObject(LTriggerIsPressed, RTriggerIsPressed);
        }

        public override void RTriggerUnpressed(SenderInfo sender, EventArguments e)
        {
            basicActionsModule.ReleaseObject(rightSelector);
            RTriggerIsPressed = false;
        }

        public override void LTriggerUnpressed(SenderInfo sender, EventArguments e)
        {
            basicActionsModule.ReleaseObject(leftSelector);
            LTriggerIsPressed = false;
        }

        public override void LGripButtonClicked(SenderInfo sender, EventArguments e)
        {
            flyingModule.leftControllerIsPressed = true;
        }

        public override void RGripButtonClicked(SenderInfo sender, EventArguments e)
        {
            flyingModule.rightControllerIsPressed = true;
            flyingModule.CreateReferences(sender.senderObject);
        }

        public override void LGripButtonHolded(SenderInfo sender, EventArguments e)
        {
            flyingModule.Fly(sender.senderObject);
        }

        public override void RGripButtonHolded(SenderInfo sender, EventArguments e)
        {
            flyingModule.GetAcceleration(sender.senderObject);
        }

        public override void RGripButtonUnclicked(SenderInfo sender, EventArguments e)
        {
            flyingModule.Stop();
        }

        public override void LGripButtonUnclicked(SenderInfo sender, EventArguments e)
        {
            flyingModule.leftControllerIsPressed = false;
        }

        private void ShowChangeSizeMenu()
        {
            ShowWheelMenu(wheelMenuActions, wheelMenuChangeSize);
        }

        private void HideChangeSizeMenu()
        {
            ShowWheelMenu(wheelMenuChangeSize, wheelMenuActions);
        }

        public void ShowFreezeMenu()
        {
            ShowWheelMenu(wheelMenuActions, wheelMenuFreeze);
            freezeModule.ShowFreezeMenu(matModule.freeze);
        }

        public void HideFreezeMenu()
        {
            ShowWheelMenu(wheelMenuFreeze, wheelMenuActions);
            freezeModule.HideFreezeMenu();
        }

        private void ShowFileMenu()
        {
            ShowWheelMenu(wheelMenuActions, wheelMenuFile);
            wheelFileIsEnabled = true;
        }

        private void HideFileMenu()
        {
            if (FileDialogIsOpen) return;

            ShowWheelMenu(wheelMenuFile, wheelMenuActions);
            wheelFileIsEnabled = false;
        }

        #endregion

        private void SetVibration(EditorState.ActiveHand hand)
        {
            float PULSE = 500;
            SetVibration((Hand)hand, PULSE);
            AudioManager.Instance.PlayEffect(AudiosData.WHEEL_SELECTOR);
        }

        private void ShowWheelMenu(GameObject menuToHide, GameObject menuToShow)
        {
            menuToHide.SetActive(false);
            currentWheel = menuToShow;
            CurrentWheelVisibility(true);
        }

        private void CurrentWheelVisibility(bool state)
        {
            currentWheel.SetActive(state);
        }

        private double GetAngle(EventArguments e)
        {
            double radians = Mathf.Atan2(e.padY, e.padX) * (180 / Math.PI);
            if (radians <= 0) radians += 360;

            return radians;
        }
    }
}