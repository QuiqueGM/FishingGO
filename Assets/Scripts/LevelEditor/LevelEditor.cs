using System;
using System.Collections.Generic;
using UnityEngine;
using VRMiddlewareController;
using System.Linq;
using VFG.Core;
using VFG.Core.Audio;

namespace VFG.LevelEditor
{
    public class LevelEditor : MiddlewareController
    {
        [Space]
        [Header("Properties")]
        public string editorLevelName;
        [Space]
        public GameObject wheelActionSelector;
        public GameObject wheelItemsSelector;
        public GameObject menuItems;

        private Transform leftSelector;
        private Transform rightSelector;
        private WheelSelector wheelActions;
        private WheelSelector wheelItems;
        private MenuItems menuItemsScrips;
        private float currentDistanceBetweenControllers;
        private bool LTriggerIsPressed, RTriggerIsPressed;
        private bool wheelItemsIsEnabled = false;
        private string str = string.Empty;
        private List<EditorState.GameObjects> gameObjects = new List<EditorState.GameObjects>();

        #region LIFE CICLE

        public override void Awake()
        {
            base.Awake();
            leftSelector = leftController.transform.Find("Reference/ObjectSelector");
            rightSelector = rightController.transform.Find("Reference/ObjectSelector");
            wheelItems = wheelItemsSelector.GetComponent<WheelSelector>();
            wheelActions = wheelActionSelector.GetComponent<WheelSelector>();

            wheelActions.SendActionEvent += SetVibration;
            wheelItems.SendActionEvent += SetVibration;
        }

        private void OnDestroy()
        {
            wheelActions.SendActionEvent -= SetVibration;
            wheelItems.SendActionEvent -= SetVibration;
        }

        private void Update()
        {
            //Debug.Log("Action: " + EditorState.currentAction);
            //Debug.Log("RTriggerIsPressed: " + RTriggerIsPressed);
            //Debug.Log("LTriggerIsPressed: " + LTriggerIsPressed);

            if (Input.GetKeyDown(KeyCode.A))
            {
                ParseStringToStructData();
                InstantiateObjectsInScene();
            }
        }

        #endregion

        #region BUTTONS BEHAVIOUR

        public override void LPadTouched(SenderInfo sender, EventArguments e)
        {
            wheelItemsSelector.SetActive(true);
            menuItemsScrips = menuItems.GetComponent<MenuItems>();
            AudioManager.Instance.PlayEffect(AudiosData.WHEEL_SELECTOR);
            
        }

        public override void LPadTouchedHolded(SenderInfo sender, EventArguments e)
        {
            wheelItems.SetTexture(GetAngle(e));
            wheelItemsIsEnabled = true;
            ActionsWheelVisibility(false);
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
            SaveEditor();
        }

        public override void RPadTouched(SenderInfo sender, EventArguments e)
        {
            if (!wheelItemsIsEnabled)
            {
                ActionsWheelVisibility(true);
                AudioManager.Instance.PlayEffect(AudiosData.WHEEL_SELECTOR);
            }
        }

        public override void RPadTouchedHolded(SenderInfo sender, EventArguments e)
        {
            if (!wheelItemsIsEnabled)
                wheelActions.SetTexture(GetAngle(e));
        }

        public override void RPadUntouched(SenderInfo sender, EventArguments e)
        {
            ActionsWheelVisibility(false);
        }

        public override void RPadHolded(SenderInfo sender, EventArguments e)
        {
            ChangeSize();
        }

        public override void RTriggerPressed(SenderInfo sender, EventArguments e)
        {
            RTriggerIsPressed = true;

            if (EditorState.itemSelected.Count == 0) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.itemSelected)
            {
                string path = string.Format
                    (
                        "{0}/{1}/{2}", 
                        GameState.PATH_PREFABS,
                        EditorState.GetFolderFromTypeOfItem((TypeOfItem)System.Enum.Parse(typeof(TypeOfItem), EditorState.groupOfItemsToShow)), 
                        obj.Key.name
                    );

                GameObject o = Instantiate(Resources.Load(path), rightSelector) as GameObject;
                // OJO!!! Les noves mides estan en centímetres!!!
                o.transform.localScale *= (GameState.collectables.Find(n => n.Id == obj.Key.name).Features.CommonSize / rightSelector.localScale.x);
                o.name = obj.Key.name;
                o.AddComponent<ObjectHandle>();
                o.tag = GameState.ITEM_SCENE;
            }
        }

        public override void LTriggerPressed(SenderInfo sender, EventArguments e)
        {
            LTriggerIsPressed = true;
        }

        public override void TriggerPressed(SenderInfo sender, EventArguments e)
        {
            if (EditorState.currentItems.Count == 0) return;
            currentDistanceBetweenControllers = Vector3.Distance(leftSelector.position, rightSelector.position);

            TransformObjects(e);
            DeleteObjects(e);
            DuplicateObjects(e);
            ScaleObject();
        }

        public override void TriggerTouched(SenderInfo sender, EventArguments e)
        {
            DeleteObjects(e);
            ScaleObject();
        }

        public override void RTriggerUnpressed(SenderInfo sender, EventArguments e)
        {
            ReleaseObject(rightSelector);
            RTriggerIsPressed = false;
        }

        public override void LTriggerUnpressed(SenderInfo sender, EventArguments e)
        {
            ReleaseObject(leftSelector);
            LTriggerIsPressed = false;
        }

        #endregion

        #region ACTIONS

        private void TransformObjects(EventArguments e)
        {
            if (EditorState.currentItems.Count == 0) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (EditorState.currentAction == EditorState.TypeOfAction.Select && 
                    obj.Key.transform.parent == gameObject.transform &&
                    (int)obj.Key.GetComponent<ObjectHandle>().hand == (int)e.hand)
                    obj.Key.transform.parent = obj.Key.GetComponent<ObjectHandle>().hand == EditorState.ActiveHand.Left ? leftSelector : rightSelector;
            }
        }

        private void ScaleObject()
        {
            if (!LTriggerIsPressed || !RTriggerIsPressed || EditorState.currentAction != EditorState.TypeOfAction.Scale) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (obj.Key.GetComponent<ObjectHandle>().hand != EditorState.ActiveHand.Both) return;

                float newDistanceBetweenControllers = Vector3.Distance(leftSelector.position, rightSelector.position);
                obj.Key.transform.localScale *= newDistanceBetweenControllers / currentDistanceBetweenControllers;
                currentDistanceBetweenControllers = newDistanceBetweenControllers;
            }
        }

        private void DeleteObjects(EventArguments e)
        {
            if (EditorState.currentItems.Count == 0) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems.ToList())
            {
                if (EditorState.currentAction == EditorState.TypeOfAction.Delete && (int)obj.Key.GetComponent<ObjectHandle>().hand == (int)e.hand)
                {
                    EditorState.currentItems.Remove(obj.Key);
                    DestroyImmediate(obj.Key.gameObject);
                }
            }
        }

        private void DuplicateObjects(EventArguments e)
        {
            if (EditorState.currentItems.Count == 0) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (EditorState.currentAction == EditorState.TypeOfAction.Duplicate && (int)obj.Key.GetComponent<ObjectHandle>().hand == (int)e.hand)
                {
                    GameObject newObject = Instantiate(obj.Key.gameObject) as GameObject;
                    newObject.transform.localPosition = obj.Key.gameObject.transform.position;
                    newObject.transform.localRotation = obj.Key.gameObject.transform.localRotation;
                    newObject.transform.localScale = obj.Key.gameObject.transform.localScale;
                    newObject.GetComponent<Renderer>().material = obj.Value;
                    newObject.transform.parent = obj.Key.GetComponent<ObjectHandle>().hand == EditorState.ActiveHand.Left ? leftSelector : rightSelector;
                    newObject.name = obj.Key.name;
                }
            }
        }

        private void ChangeSize()
        {
            float INIT_POSITION = 0.135f;
            float SCALE_SENSITIVITY = 0.15f;
            float MIN_SIZE = 0.03f;
            float MAX_SIZE = 2.0f;
            float scaleFactor = SCALE_SENSITIVITY * Time.deltaTime;

            if (EditorState.currentAction == EditorState.TypeOfAction.DecreaseSize)
            {
                leftSelector.localScale = rightSelector.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
                if (leftSelector.localScale.x < MIN_SIZE) leftSelector.localScale = rightSelector.localScale = new Vector3(MIN_SIZE, MIN_SIZE, MIN_SIZE);
            }

            if (EditorState.currentAction == EditorState.TypeOfAction.IncreaseSize)
            {
                leftSelector.localScale = rightSelector.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);
                if (leftSelector.localScale.x > MAX_SIZE) leftSelector.localScale = rightSelector.localScale = new Vector3(MAX_SIZE, MAX_SIZE, MAX_SIZE);
            }

            leftSelector.localPosition = rightSelector.localPosition = new Vector3(0, 0, (INIT_POSITION + ((leftSelector.localScale.x - MIN_SIZE) / 2)));
        }

        public void SaveEditor()
        {
            if (EditorState.currentAction != EditorState.TypeOfAction.Save) return;

            string str = string.Empty;
            int childCount = transform.childCount;

            for (int n = 0; n < childCount; n++)
                str = string.Format("{0}{1}", str, GetDataChild(gameObject, n));

            PlayerPrefs.SetString(editorLevelName, str);

            Debug.Log(str);
        }

        public void ParseStringToStructData()
        {
            if (PlayerPrefs.HasKey(editorLevelName) && (PlayerPrefs.GetString(editorLevelName) != "" || PlayerPrefs.GetString(editorLevelName) != null || PlayerPrefs.GetString(editorLevelName) != string.Empty))
                str = PlayerPrefs.GetString(editorLevelName);
            else
            {
                Debug.LogError(string.Format("{0} not found!", editorLevelName));
                return;
            }
                
            string[] strSplit = str.Split(new[] { ',' });

            for (int n = 0; n < strSplit.Length - 1; n = n + 11)
            {
                Debug.Log(strSplit[n]);

                TypeOfItem typeOfItem = GameState.collectables.Find(c => c.Id == strSplit[n]).TypeOfItem;
                string path = string.Format("{0}/{1}/{2}", GameState.PATH_PREFABS, EditorState.GetFolderFromTypeOfItem(typeOfItem), strSplit[n]);

                GameObject prefab = Resources.Load(path) as GameObject;
                Vector3 pos = new Vector3(float.Parse(strSplit[n + 1]), float.Parse(strSplit[n + 2]), float.Parse(strSplit[n + 3]));
                Quaternion rot = new Quaternion(float.Parse(strSplit[n + 4]), float.Parse(strSplit[n + 5]), float.Parse(strSplit[n + 6]), float.Parse(strSplit[n + 7]));
                Vector3 scl = new Vector3(float.Parse(strSplit[n + 8]), float.Parse(strSplit[n + 9]), float.Parse(strSplit[n + 10]));

                EditorState.GameObjects objectData = new EditorState.GameObjects()
                {
                    prefab = prefab,
                    pos = pos,
                    rot = rot,
                    scl = scl
                };

                gameObjects.Add(objectData);
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void SetVibration(EditorState.ActiveHand hand)
        {
            float PULSE = 500;
            SetVibration((Hand)hand, PULSE);
            AudioManager.Instance.PlayEffect(AudiosData.WHEEL_SELECTOR);
        }

        private void ActionsWheelVisibility(bool state)
        {
            wheelActionSelector.SetActive(state);
        }

        private double GetAngle(EventArguments e)
        {
            double radians = Mathf.Atan2(e.padY, e.padX) * (180 / Math.PI);
            if (radians <= 0) radians += 360;

            return radians;
        }

        private void ReleaseObject(Transform controller)
        {
            if (EditorState.currentItems.Count == 0) return;

            foreach (KeyValuePair<GameObject, Material> obj in EditorState.currentItems)
            {
                if (controller.Find(obj.Key.name))
                {
                    obj.Key.GetComponent<Renderer>().material = obj.Value;
                    obj.Key.transform.parent = gameObject.transform;
                }
            }
        }

        private string GetDataChild(GameObject container, int n)
        {
            GameObject obj = container.transform.GetChild(n).gameObject;

            string name = obj.name;
            string pos = string.Format("{0},{1},{2}", obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            string rot = string.Format("{0},{1},{2},{3}", obj.transform.rotation.x, obj.transform.rotation.y, obj.transform.rotation.z, obj.transform.rotation.w);
            string scl = string.Format("{0},{1},{2}", obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);

            return string.Format("{0},{1},{2},{3},", name, pos, rot, scl);
        }

        public void InstantiateObjectsInScene()
        {
            for (int n = 0; n < gameObjects.Count; n++)
            {
                GameObject newObject = Instantiate(gameObjects[n].prefab, gameObjects[n].pos, gameObjects[n].rot, gameObject.transform) as GameObject;
                newObject.name = gameObjects[n].prefab.name;
                newObject.transform.localScale = gameObjects[n].scl;
                newObject.AddComponent<ObjectHandle>();
                newObject.tag = GameState.ITEM_SCENE;
            }
        }

        #endregion
    }
}