using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.XR.iOS;
using VFG.Canvas;
using VFG.Core;
using VFG.Utils;
using VFG.LevelEditor;
using VFG.Models;
using VFG.Core.Audio;
using System.Collections.Generic;

namespace VFG.Managers
{
	public class GameManager : MonoBehaviour
	{
        public CanvasManager canvasManager;
        public GameObject container;
        public GameObject aquariums;
        public PlaceAquarium placeAquarium;
        public GameObject lineGuide;
        public GameObject compass;

        private CanvasManager CM;
        private List<GameObject> _aquariums = new List<GameObject>();
        private GameObject _aquarium;
		private Vector3 _fishschoolInitPosition;
        private GameObject _fishSchool;
        private GameObject _objective;
        private Texture2D screenCapture;
        private bool _showHelpLine = false;
		private GameObject _currentObjAux;
        private TypeOfItem _typeOfItem;
        private Coroutine hideObjectToFind;
        private Coroutine hideCompassArrow;
		private int _currentAquarium;

        void Awake()
		{
            CM = canvasManager;
            CM.CNV_Buttons.GetComponent<CanvasShowButtons>().SendTakePicture += TakePicture;
            placeAquarium.SendHitEvent += SpawnFishSchool;
        }

		void OnDestroy()
		{
			CM.CNV_Buttons.GetComponent<CanvasShowButtons> ().SendTakePicture -= TakePicture;
            placeAquarium.SendHitEvent -= SpawnFishSchool;
        }

        public void CreateObjective()
		{
            Objective objective = GameState.objectives.Find(c => c.Id == GameState.currentObjectiveName);
			string pathItem = string.Format("{0}/{1}/{2}", GameState.PATH_PREFABS, EditorState.GetFolderFromTypeOfItem(objective.TypeOfItem), GameState.currentObjectiveName);

            if (_objective != null)
            {
                Destroy(_objective);
                _objective = new GameObject();
            }

            _typeOfItem = objective.TypeOfItem;
            CM.CNV_Buttons.GetComponent<CanvasShowButtons>().SetCardButton(_typeOfItem);
			Debug.Log("======== " + pathItem);
			;
            if (_typeOfItem == TypeOfItem.Fish)
                CreteFishSchoolObjective(objective, pathItem);
            else
                CreateObjective(objective, GameState.currentObjectiveName);
        }

		private void CreteFishSchoolObjective(Objective objective, string pathItem)
		{
			GameObject currentItem = Resources.Load(pathItem) as GameObject;

            string pathFishSchool = string.Format("{0}/{1}{2}", GameState.PATH_PREFABS, GameState.FISHSCHOOL_OBJECTIVE, (GameState.Level)GameState.LevelOfDifficulty);

			_objective = Instantiate(Resources.Load(pathFishSchool)) as GameObject;
			_fishschoolInitPosition = _objective.transform.position;
			_objective.transform.parent = container.transform;
			_objective.transform.localPosition = Vector3.zero;
			_objective.transform.localPosition += _fishschoolInitPosition;
			_objective.SetActive(true);
			_objective.name = GameState.currentObjectiveName;
			_objective.GetComponent<SchoolController>()._childPrefab[0] = currentItem.GetComponent<SchoolChild>();
			_objective.GetComponent<SchoolController>()._posOffset = InitSchoolPosition(_objective.GetComponent<SchoolController>());

			_aquarium.GetComponent<AquariumManager>().HideAllObjectives();

			StartCoroutine (FollowFish (0));
		}

        private Vector3 InitSchoolPosition(SchoolController sc)
        {
            float w, d, h;
            float width = sc._positionSphere;
            float depth = sc._positionSphereDepth;
            float height = sc._positionSphereHeight;

            do 
            {
                w = Random.Range(-width, width);
                d = Random.Range(-depth, depth);
                h = Random.Range(-height, height);

                return new Vector3(w, h, d);
            }
            while ((Mathf.Abs(w) + Mathf.Abs(d) + Mathf.Abs(h)) > 1);
        }

		IEnumerator FollowFish(int n)
		{
			yield return new WaitForSeconds (0.5f);

			if (_objective.transform.childCount > 0)
				_currentObjAux = _objective.transform.GetChild (0).gameObject;
			else
				StartCoroutine (FollowFish (n));
		}

        private void CreateObjective(Objective objective, string pathItem)
        {
            _aquarium.GetComponent<AquariumManager>().ActiveObjective(pathItem);
            _currentObjAux = _aquarium.GetComponent<AquariumManager>().GetTransform(pathItem);
        }

        public void SpawnFishSchool()
        {
            if (_fishSchool != null) Destroy(_fishSchool);

            string pathFishSchool = string.Format("{0}{1}", GameState.PATH_FIHSSCHOOL, GameState.currentAquariumName);
            _fishSchool = Instantiate(Resources.Load(pathFishSchool), container.transform) as GameObject;

			Debug.Log ("======== Fishschool: " + _fishSchool.name);
        }

        private void HideCanvas(params GameObject[] canvas)
		{
			foreach (GameObject c in canvas)
				c.SetActive(false);
		}

		public void ShowCardWithObjective()
		{
            CM.CNV_ObjectiveTofind.SetActive(true);
            if (hideObjectToFind != null) StopCoroutine(hideObjectToFind);
        }

        public void HideCardWithObjective()
        {
            const float TIME_TO_DISSAPEAR = 1;
            hideObjectToFind = StartCoroutine(HideCardWithObjective(TIME_TO_DISSAPEAR));
        }

        public IEnumerator HideCardWithObjective(float timeToDissapear)
        {
            yield return new WaitForSeconds(timeToDissapear);
            CM.CNV_ObjectiveTofind.SetActive(false);
        }

        public void ShowCompass()
        {
            if (_currentObjAux == null) return;

//            lineGuide.SetActive(true);
//            lineGuide.GetComponent<HelpObjective>().SetObjectivePosition(_currentObjAux);

            compass.SetActive(true);
            compass.GetComponent<ArrowCompass>().SetObjectivePosition(_currentObjAux);

            if (hideCompassArrow != null) StopCoroutine(hideCompassArrow);
        }

        public void HideCompass()
        {
            const float TIME_TO_DISSAPEAR = 1;
            hideCompassArrow = StartCoroutine(HideCompass(TIME_TO_DISSAPEAR));
        }

        public IEnumerator HideCompass(float timeToDissapear)
        {
            yield return new WaitForSeconds(timeToDissapear);
            lineGuide.SetActive(false);
            compass.SetActive(false);
        }

        public void TakePicture(GameState.ObjectDetection typeOfDetection)
		{
			if (typeOfDetection != GameState.ObjectDetection.IsDetected)
			{
                CM.CNV_TryAgain.SetActive(true);
                CM.CNV_TryAgain.GetComponent<CanvasTryHarder>().ShowCanvas(typeOfDetection);
                CM.CNV_Buttons.GetComponent<CanvasShowButtons>().ShowButtons();
            }
			else
			{
				compass.SetActive(false);
				HideCanvas(CM.CNV_Buttons, CM.CNV_TryAgain, CM.CNV_ObjectiveTofind);
                AudioManager.Instance.PlayEffect(AudiosData.TAKE_PICTURE);

                if (GameState.objectivesState[GameState.currentObjectiveName] == (int)GameState.State.Unlocked)
				{
					LoadUserPreferences.Instance.MarkObjectiveAsSolved(GameState.currentAquarium);
					StartCoroutine(OpenObjectiveSolved(GameState.currentObjectiveName));
				}
				else
					StartCoroutine(OpenObjectiveRepeated(GameState.currentObjectiveName + GameState.TEMPORARY));
			}
		}

		public IEnumerator OpenObjectiveSolved(string name)
		{
			yield return new WaitForEndOfFrame();
			SaveScreenshot(name, out screenCapture);
			CM.CNV_ObjectiveSolved.GetComponent<CanvasObjectiveSolved>().Init(screenCapture, false);
            CM.CNV_ObjectiveSolved.GetComponent<IMenusScreen>().ShowPopUp(CM.CNV_ObjectiveSolved);
            CM.SetCurrentCanvas(CM.CNV_ObjectiveSolved);
            CM.CNV_BackgroundMenu.SetActive(true);
		}

		public IEnumerator OpenObjectiveRepeated(string name)
		{
			yield return new WaitForEndOfFrame();
			SaveScreenshot(name, out screenCapture);
			CM.CNV_ComparePictures.GetComponent<CanvasComparePictures>().Init();
            CM.SetCurrentCanvas(CM.CNV_ComparePictures);
            CM.CNV_BackgroundMenu.SetActive(true);
        }

		private void SaveScreenshot(string name, out Texture2D screenCap)
		{
			screenCap = new Texture2D(GameState.resWidth, GameState.resHeight, TextureFormat.RGB24, false);
			screenCap.ReadPixels(new Rect(0, 0, GameState.resWidth, GameState.resHeight), 0, 0);
			screenCap.Apply();

			byte[] bytes = screenCap.EncodeToPNG();
			File.WriteAllBytes(Path.Combine(Application.persistentDataPath, name + GameState.EXTENSION), bytes);

            CM.CNV_Buttons.SetActive(true);
		}

		public void ReplacePictures()
		{
			string oldPicture = Path.Combine(Application.persistentDataPath, GameState.currentObjectiveName + GameState.EXTENSION);
			string newPicture = Path.Combine(Application.persistentDataPath, GameState.currentObjectiveName + GameState.TEMPORARY + GameState.EXTENSION);

            CM.CNV_ObjectiveSolved.GetComponent<CanvasObjectiveSolved>().ReplacePicture(oldPicture, newPicture);
            CM.GoToSelectedScreen(TypeOfAction.ReplacePictures);
        }

        public void CloseObjectiveSolved(bool backToMenu)
        {
            if (GameState.newAquariumIsUnlocked)
            {
                ShowPopup(TypeOfAction.NewAquarium, ref GameState.newAquariumIsUnlocked, AudiosData.NEW_AQUARIUM);
                CM.CNV_NewAquarium.GetComponent<CanvasNewAquarium>().SetButtons(backToMenu);
            }
            else if (GameState.allObjectivesUnlockedInAquarium)
            {
                ShowPopup(TypeOfAction.AllObjectives, ref GameState.allObjectivesUnlockedInAquarium, AudiosData.ALL_OBJECTIVES);
                CM.CNV_AllObjectives.GetComponent<CanvasAllObjectives>().SetButtons(backToMenu);
            }
            else if (GameState.allObjectivesUnlockedInGame)
                ShowPopup(TypeOfAction.EndGame, ref GameState.allObjectivesUnlockedInGame, AudiosData.ALL_OBJECTIVES);
            else
            {
                if (backToMenu) BackToMenu();
                else Continue(null);
            }
        }

        public void BackToMenu()
        {
            CM.CNV_BackgroundMenu.SetActive(true);
            CM.GoToSelectedScreen(TypeOfAction.BackToMainMenu);
            AudioManager.Instance.ChangeMusicWithFade(AudiosData.MUSIC_MENUS, 1, 1);
        }

        public void Continue(GameObject canvas)
        {
            if (canvas != null) canvas.SetActive(false);

			if (GameState.playFromNewObjective)
			{
				Vector2 newObj = GetNewObjective ();
				GameState.currentAquarium = (int)newObj.x;
				GameState.currentObjective = (int)newObj.y;

				Debug.Log (string.Format ("Aqu: {0}, Obj: {1}", GameState.currentAquarium, GameState.currentObjective));

				if (_currentAquarium != GameState.currentAquarium)
					ChangeToNextAquarium ();
			}
			else
            {
                GameState.currentObjective++;
                CheckToChangeAquarium();
            }

            GameState.currentObjectiveName = GameState.aquariums[GameState.currentAquarium].Objectives[GameState.currentObjective].Id;

            CreateObjective();

            CM.CNV_BackgroundMenu.SetActive(false);
            CM.CNV_ObjectiveSolved.SetActive(false);
            CM.CNV_ObjectiveTofind.GetComponent<CanvasObjectiveToFind>().ShowObjectToFind();
            CM.CNV_Buttons.SetActive(true);
        }

		private void CheckToChangeAquarium()
        {
			if (GameState.currentObjective >= GameState.aquariums[GameState.currentAquarium].Objectives.Count)
            {
                GameState.currentObjective = 0;
                GameState.currentAquarium++;

				if (GameState.currentAquarium >= GameState.aquariums.Count - GameState.NUMBER_OF_COMING_SOON_AQUARIUMS)
					GameState.currentAquarium = 0;

				ChangeToNextAquarium ();
            }
        }

		private void ChangeToNextAquarium()
		{
			GameState.currentAquariumName = GameState.aquariums[GameState.currentAquarium].Id;

			ShowAquarium();
			SpawnFishSchool();

			Debug.Log(string.Format("Changing to next aquarium -> <color=orange>{0}</color>", GameState.currentAquariumName));
		}

        public void Continue()
        {
            CM.CNV_BackgroundMenu.SetActive(false);
            CM.CNV_ComparePictures.SetActive(false);
        }

        private void ShowPopup(TypeOfAction action, ref bool flag, string sound)
        {
            CM.CNV_Buttons.SetActive(false);
            AudioManager.Instance.PlayEffect(sound);
            CM.GoToSelectedScreen(action);
            flag = false;
        }

        public void ShowAquarium()
        {
            HideAllAquariums();

            if (_aquariums.Find(n => n.name == GameState.currentAquariumName))
            {
                _aquarium = _aquariums.Find(n => n.name == GameState.currentAquariumName);
                _aquarium.SetActive(true);
            }
            else
            {
                string pathAquarium = string.Format("{0}/{1}", GameState.PATH_AQUARIUMS, GameState.currentAquariumName);
                GameObject newAquarium = Instantiate(Resources.Load(pathAquarium), aquariums.transform) as GameObject;
                newAquarium.name = GameState.currentAquariumName;
                _aquariums.Add(newAquarium);
                _aquarium = newAquarium;
                _aquarium.GetComponent<AquariumManager>().Init();
            }

			_currentAquarium = GameState.currentAquarium;
			Debug.Log ("======== " + GameState.currentAquariumName);
        }

        private void HideAllAquariums()
        {
            foreach (GameObject o in _aquariums)
                o.SetActive(false);
        }

		public Vector2 GetNewObjective()
		{
			Vector2 newObj = Vector2.zero;

			for (int a = 0; a < GameState.aquariums.Count - GameState.NUMBER_OF_COMING_SOON_AQUARIUMS; a++)
			{
				for (int o = 0; o < GameState.aquariums[a].NumberOfSolvedObjectivesToUnlockNextAquarium; o++)
				{
					if (GameState.objectivesState[GameState.aquariums[a].Objectives[o].Id] == (int)GameState.State.Unlocked)
					{
						newObj = new Vector2(a, o);
						return newObj;
					}
				}
			}

			for (int a = 0; a < GameState.aquariums.Count - GameState.NUMBER_OF_COMING_SOON_AQUARIUMS; a++)
			{
				for (int o = GameState.aquariums[a].NumberOfSolvedObjectivesToUnlockNextAquarium; o < GameState.aquariums[a].Objectives.Count; o++)
				{
					if (GameState.objectivesState[GameState.aquariums[a].Objectives[o].Id] == (int)GameState.State.Unlocked)
					{
						newObj = new Vector2(a, o);
						return newObj;
					}
				}
			}

			return newObj;
		}
    }
}