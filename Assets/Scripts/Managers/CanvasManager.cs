using UnityEngine;
using VFG.Core;
using DG.Tweening;
using VFG.Canvas;
using VFG.Core.Audio;
using TMPro;

namespace VFG.Managers
{
    public class CanvasManager : MonoBehaviour
    {
		//public TMP_Text aquarium;
		//public TMP_Text objective;

        public enum MenuState
        {
            Hide = 0,
            Show = 1
        }

        [Header("Managers")]
        public ARKitManager ARKitManager;
        public GameManager GameManager;
        [Space(10)]

        [Header("Canvas Main Menu")]
        public GameObject CNV_BackgroundMenu;
        public GameObject CNV_MainMenu;
        public GameObject CNV_Settings;
        public GameObject CNV_Reset;
        public GameObject CNV_Aquariums;
        public GameObject CNV_Objectives;
        public GameObject CNV_Cards;

        [Header("Canvas Card")]
        public GameObject CNV_BackgroundCard;
        public GameObject CNV_MainSide;
        public GameObject CNV_MoreSide;
        public GameObject CNV_Classification;
        public GameObject CNV_QuizSide;
        public GameObject CNV_Picture;
        public GameObject CNV_Share;

        [Header("Canvas ARKit")]
        public GameObject CNV_Scannig;
        public GameObject CNV_TimesUp;
        public GameObject CNV_PlaceAquarium;

        [Header("Canvas InGame")]
        public GameObject CNV_ObjectiveTofind;
        public GameObject CNV_TryAgain;
        public GameObject CNV_Buttons;
        public GameObject CNV_ObjectiveSolved;
        public GameObject CNV_ComparePictures;

        [Header("Canvas Information")]
        public GameObject CNV_NewAquarium;
        public GameObject CNV_AllObjectives;
        public GameObject CNV_EndGame;

        private GameObject _currentCanvas;
        private GameObject _formerCanvas;
        private GameObject _cardCanvas;
        private bool _canPress = true;

        void Awake()
        {
            AddListeners();
            InitializeMenus();

            GoToSelectedScreen(TypeOfAction.MainMenu);
        }

        private void AddListeners()
        {
            foreach (BaseButton baseButton in transform.GetComponentsInChildren<BaseButton>())
                baseButton.sendAction += GoToSelectedScreen;
        }

        private void InitializeMenus()
        {
            InitializeMenus(CNV_BackgroundMenu, CNV_Settings, CNV_Reset, CNV_Aquariums, CNV_Objectives, CNV_Cards);
            InitializeMenus(CNV_BackgroundCard, CNV_MainSide, CNV_MoreSide, CNV_Classification, CNV_Picture, CNV_Share);
            InitializeMenus(CNV_ObjectiveSolved, CNV_ComparePictures);
            InitializeMenus(CNV_NewAquarium, CNV_AllObjectives, CNV_EndGame);

            CNV_TryAgain.SetActive(false);
            CNV_ObjectiveTofind.SetActive(false);

            MenuVisibility(CNV_BackgroundMenu, MenuState.Show, 0.5f);

            _currentCanvas = CNV_MainMenu;
            _formerCanvas = _currentCanvas;
        }

        private void InitializeMenus(params GameObject[] menu)
        {
            foreach (GameObject m in menu)
                m.GetComponent<IMenusScreen>().Initialize();
        }

        public void GoToSelectedScreen(TypeOfAction action)
        {
            if (GameState.canPressAButton)
            {
                GameState.canPressAButton = false;

                switch (action)
                {
                    case TypeOfAction.MainMenu:
                        MenuVisibility(CNV_MainMenu, MenuState.Show, 0.5f);
                        break;
                    case TypeOfAction.BackToMainMenu:
                        CNV_Buttons.SetActive(false);
                        MenuVisibility(_currentCanvas, CNV_MainMenu, 0.5f);
                        break;
                    case TypeOfAction.CloseARCanvas:
                        AudioManager.Instance.ChangeMusicWithFade(AudiosData.MUSIC_MENUS, 1, 1);
                        MenuVisibility(CNV_BackgroundMenu, MenuState.Show, 0.45f);
                        MenuVisibility(CNV_MainMenu, MenuState.Show, 0.5f);
                        break;
                    case TypeOfAction.Settings:
                        MenuVisibility(CNV_Settings, MenuState.Show, 0.2f);
                        break;
                    case TypeOfAction.Reset:
                        SetFormerCanvas();
                        MenuVisibility(CNV_Reset, MenuState.Show, 0.2f);
                        break;
                    case TypeOfAction.Aquariums:
                        MenuVisibility(CNV_MainMenu, CNV_Aquariums);
                        PopulateCanvas(CNV_Aquariums);
                        break;
                    case TypeOfAction.Objectives:
                        MenuVisibility(CNV_Aquariums, CNV_Objectives);
                        PopulateCanvas(CNV_Objectives);
                        AudioManager.Instance.PlayEffect(AudiosData.BUTTON_UP);
                        break;
                    case TypeOfAction.Cards:
                        MenuVisibility(CNV_MainMenu, CNV_Cards);
                        PopulateCanvas(CNV_Cards);
                        break;

                    case TypeOfAction.BackButtonAquariums:
                        MenuVisibility(CNV_Aquariums, CNV_MainMenu, 0.5f);
                        break;
                    case TypeOfAction.BackButtonObjectives:
                        MenuVisibility(CNV_Objectives, CNV_Aquariums, 0.5f);
                        break;
                    case TypeOfAction.BackButtonCards:
                        MenuVisibility(CNV_Cards, CNV_MainMenu, 0.5f);
                        break;

                    case TypeOfAction.NewObjective:
                        GameState.playFromNewObjective = true;
                        OpenNewObjective();
                        break;

                    case TypeOfAction.ShowCard:
                        SetBackGroundCard(true);
                        SetFormerCanvas();
                        MenuVisibility(CNV_MainSide, MenuState.Show, 0.1f);
                        PopulateCanvas(CNV_MainSide);
                        break;
                    case TypeOfAction.ShowMainCard:
                        MenuVisibility(CNV_Classification, CNV_MainSide, 0.1f);
                        PopulateCanvas(CNV_MainSide);
                        break;
                    case TypeOfAction.ShowMoreCard:
                        SetBackGroundCard(true);
                        MenuVisibility(CNV_MainSide, CNV_MoreSide, 0.1f);
                        PopulateCanvas(CNV_MoreSide);
                        break;
				case TypeOfAction.ShowMoreCardFromInGame:
						SetFormerCanvas ();
						SetBackGroundCard(true);
						MenuVisibility(CNV_MainSide, CNV_MoreSide, 0.1f);
						PopulateCanvas(CNV_MoreSide);
						break;
                    case TypeOfAction.ShowClassCard:
                        MenuVisibility(CNV_MoreSide, CNV_Classification, 0.1f);
                        PopulateCanvas(CNV_Classification);
                        break;
                    case TypeOfAction.ShowPicture:
                        SetCardCanvas();
                        MenuVisibility(CNV_Picture, MenuState.Show, 0.1f);
                        PopulateCanvas(CNV_Picture);
                        break;
                    case TypeOfAction.CloseCard:
                        MenuVisibility(_formerCanvas, 0.2f);
                        SetBackGroundCard(false);
                        break;
                    case TypeOfAction.ClosePicture:
                        MenuVisibility(_cardCanvas, 0.2f);
                        break;

                    case TypeOfAction.Play:
                        CNV_BackgroundMenu.SetActive(false);
                        MenuVisibility(CNV_Objectives, MenuState.Hide, 0.5f);
                        GameState.playFromNewObjective = false;
                        Play();
                        break;

                    case TypeOfAction.Continue:
                        MenuVisibility(CNV_BackgroundMenu, MenuState.Hide, 0.5f);
                        break;

                    case TypeOfAction.NewAquarium:
                        MenuVisibility(_currentCanvas, CNV_NewAquarium, 0.5f);
						PopulateCanvas(CNV_NewAquarium);
                        break;
                    case TypeOfAction.GoToMyNewAquarium:
                        GoToMyNewAquarium();
                        break;
                    case TypeOfAction.AllObjectives:
                        MenuVisibility(_currentCanvas, CNV_AllObjectives, 0.2f);
                        break;
                    case TypeOfAction.GoToAquariumFinished:
                        GoToAquariumFinished();
                        break;

                    case TypeOfAction.EndGame:
                        MenuVisibility(_currentCanvas, CNV_EndGame, 0.2f);
                        break;

                    case TypeOfAction.ReplacePictures:
                        MenuVisibility(CNV_ComparePictures, CNV_ObjectiveSolved, 0.3f);
                        break;

                    case TypeOfAction.SharePicture:
                        Share();
                        break;

                    case TypeOfAction.None:
                        CanPressAButton();
                        break;
                }
            }
        }

        private void GoToMyNewAquarium()
        {
            MenuVisibility(CNV_BackgroundMenu, MenuState.Show, 0.45f);
            AudioManager.Instance.ChangeMusicWithFade(AudiosData.MUSIC_MENUS, 1, 1);
            MenuVisibility(CNV_NewAquarium, CNV_Objectives, 0.5f);
            GameState.currentAquarium = GameState.newAquariumUnlocked;
            GameState.currentAquariumName = GameState.newAquariumUnlockedName;
            PopulateCanvas(CNV_Objectives);
            PopulateCanvas(CNV_Aquariums);
        }

        private void GoToAquariumFinished()
        {
            MenuVisibility(CNV_BackgroundMenu, MenuState.Show, 0.45f);
            AudioManager.Instance.ChangeMusicWithFade(AudiosData.MUSIC_MENUS, 1, 1);
            MenuVisibility(CNV_AllObjectives, CNV_Objectives, 0.5f);
            PopulateCanvas(CNV_Objectives);
            PopulateCanvas(CNV_Aquariums);
        }

        void OpenNewObjective()
        {
            CanPressAButton();

            if (GameState.AllObjectivesSolved == (int)GameState.Toggle.On)
            {
                GoToSelectedScreen(TypeOfAction.EndGame);
                return;
            }

            CNV_BackgroundMenu.SetActive(false);
			MenuVisibility (CNV_MainMenu, MenuState.Hide, 0.3f);
            
			Vector2 newObj = GameManager.GetNewObjective ();
			GameState.currentAquarium = (int)newObj.x;
			GameState.currentObjective = (int)newObj.y;
			GameState.currentAquariumName = GameState.aquariums[GameState.currentAquarium].Id;
			GameState.currentObjectiveName = GameState.aquariums[GameState.currentAquarium].Objectives[GameState.currentObjective].Id;

            Play();
        }

        void Play()
        {
            GameManager.ShowAquarium();
            GameManager.CreateObjective();
			if (!GameState.isFirstUseApp)
				GameManager.SpawnFishSchool ();
			
            ARKitManager.ShowScanningSurfaces();
            AudioManager.Instance.PlayEffect(AudiosData.BUTTON_UP);
            AudioManager.Instance.ChangeMusicWithFade(AudiosData.MUSIC_INGAME, 1, 1);
        }

        void MenuVisibility(GameObject desireCurrentCanvas, float duration = 0.5f)
        {
            CanvasGroup c = _currentCanvas.GetComponent<CanvasGroup>();
            c.alpha = (int)MenuState.Show;

            Sequence menuFades = DOTween.Sequence();

            menuFades
                .Append(DOTween.To(() => c.alpha, a => c.alpha = a, (int)MenuState.Hide, duration))
                .AppendCallback(() => SetVisibility(desireCurrentCanvas))
                .AppendCallback(() => CanPressAButton());
        }

        void MenuVisibility(GameObject canvas, MenuState finalState, float duration = 0.5f)
        {
            CanvasGroup c = canvas.GetComponent<CanvasGroup>();
            c.alpha = Mathf.Abs((int)finalState - (int)MenuState.Show);
            canvas.SetActive(true);

            Sequence menuFades = DOTween.Sequence();

            menuFades
                .Append(DOTween.To(() => c.alpha, a => c.alpha = a, (int)finalState, duration))
                .AppendCallback(() => SetVisibility(canvas, finalState == MenuState.Hide ? false : true))
                .AppendCallback(() => CanPressAButton());
        }

        void MenuVisibility(GameObject menuOff, GameObject menuOn, float duration = 0.5f)
        {
            CanvasGroup canvasOff = menuOff.GetComponent<CanvasGroup>();
            CanvasGroup canvasOn = menuOn.GetComponent<CanvasGroup>();
            Sequence menuFades = DOTween.Sequence();

            menuFades
                .Append(DOTween.To(() => canvasOff.alpha, a => canvasOff.alpha = a, (int)MenuState.Hide, duration))
                .AppendCallback(() => SetVisibility(menuOff, false))
                .AppendCallback(() => SetVisibility(menuOn, true))
                .Append(DOTween.To(() => canvasOn.alpha, a => canvasOn.alpha = a, (int)MenuState.Show, duration))
                .AppendCallback(() => SetCurrentCanvas(menuOn))
                .AppendCallback(() => CanPressAButton());
        }

        void SetVisibility(GameObject menu, bool state)
        {
            menu.SetActive(state);
            _currentCanvas = menu;
        }

        void SetVisibility(GameObject desireCurrentCanvas)
        {
            _currentCanvas.SetActive(false);
            _currentCanvas = desireCurrentCanvas;
            GameState.canPressAButton = true;
        }

        private void PopulateCanvas(GameObject menu)
        {
            menu.GetComponent<IMenusScreen>().Populate();
        }

        public void CanPressAButton()
        {
            GameState.canPressAButton = true;
        }

        public void Share()
        {
            CanPressAButton();
            CNV_Share.GetComponent<CanvasShareCard>().Share();
        }

        private void SetBackGroundCard(bool state)
        {
            if (state)
                CNV_BackgroundCard.GetComponent<IMenusScreen>().Populate();
            else
                CNV_BackgroundCard.GetComponent<IMenusScreen>().Initialize();
        }

        public void SetCurrentCanvas(GameObject canvas)
        {
            _currentCanvas = canvas;
        }

        public void SetFormerCanvas()
        {
            _formerCanvas = _currentCanvas;
        }

        public void SetCardCanvas()
        {
            _cardCanvas = _currentCanvas;
        }

        private void Update()
        {
//            VFG.Utils.Console.Clear();
//            Debug.Log(string.Format("Former CNV: {0} | Current CNV: {1}", _formerCanvas.name, _currentCanvas.name));
//            Debug.Log("CanPressAButton: " + GameState.canPressAButton);
            //WriteButtonState(_canPress);
			//aquarium.text = string.Format("Aquarium: {0}", (int)GameManager.GetNewObjective().x);
			//objective.text = string.Format("Objective: {0}", (int)GameManager.GetNewObjective().y);
        }

        //private void WriteButtonState(bool state)
        //{
        //    if (GameState.canPressAButton != state)
        //    {
        //        VFG.Utils.Console.Clear();
        //        _canPress = GameState.canPressAButton;
        //        Debug.Log(state ? "<color=red>FALSE</color>" : "<color=green>TRUE</color>");
        //    }
        //}
    }
}