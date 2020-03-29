using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using VFG.Core;
using System.Collections.Generic;

namespace VFG.Canvas
{
	public delegate void TakePictureRequest(GameState.ObjectDetection typeOfDetection);

    public class CanvasShowButtons : MonoBehaviour
    {
        const float TIME_TO_SHOW_BUTTONS = 0.2f;

        [System.Serializable]
        public struct ButtonFacade
        {
            public Sprite idle;
            public SpriteState spriteState;
        }

		public event TakePictureRequest SendTakePicture;

        public GameObject buttonShowCard;
		public GameObject[] help;
        public ButtonFacade[] buttonCard;

        private CanvasGroup _canvas;
        private Sequence sequence;
		private float _counter;
		private bool _startCounting = false;
		private GameState.ObjectDetection _typeOfDetection = GameState.ObjectDetection.NoDetected;
		private List<Button> _buttons = new List<Button>();
		private int _numberOfTimeToShowHelp = 5;
		private bool _canShowHelp = true;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();

			SetHelpVisibility (false);

			for (int n = 0; n < transform.childCount; n++)
				_buttons.Add (transform.GetChild (n).GetComponent<Button> ());
        }
        
		public void StartCounting()
		{
			_startCounting = true;

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 2, 1 << 9 | 1 << 8) && hit.collider.gameObject.name == GameState.currentObjectiveName)
            {
                if (Vector3.Distance(hit.collider.gameObject.transform.position, Camera.main.transform.position) > GetMaxDistance())
                    _typeOfDetection = GameState.ObjectDetection.DetectedButTooFar;
                else
                    _typeOfDetection = GameState.ObjectDetection.IsDetected;
            }
            else
                _typeOfDetection = GameState.ObjectDetection.NoDetected;
        }

		private void Update()
		{
			if (_startCounting) 
			{
				_counter += Time.deltaTime;

				if (_counter >= TIME_TO_SHOW_BUTTONS) 
					ShowButtons ();
			}
		}

		public void ShowButtons()
        {
			ShowHelp ();

            const float DURATION = 2;
            const float DELAY = 3f;

            _canvas.alpha = 1;

            if (sequence != null) sequence.Kill();

			foreach (Button btn in _buttons)
				btn.interactable = true;
			
            sequence = DOTween.Sequence();
			sequence.Append(DOTween.To(() => _canvas.alpha, a => _canvas.alpha = a, 0, DURATION).SetDelay(DELAY).OnComplete(OnComplete));
        }

		private void ShowHelp()
		{
			if (GameState.ShowHelpAtTheBeginning == (int)GameState.Toggle.Off) return;

			if (_canShowHelp) 
			{
				_canShowHelp = false;
				_numberOfTimeToShowHelp--;
				SetHelpVisibility (_numberOfTimeToShowHelp > 0 ? true : false);
			}
		}

		private void SetHelpVisibility(bool state)
		{
			foreach (GameObject o in help)
				o.SetActive (state);
		}

		private void OnComplete()
		{
			foreach (Button btn in _buttons)
				btn.interactable = false;
		}

		public void PointerUp()
		{
			if (_counter < TIME_TO_SHOW_BUTTONS) 
				SendTakePicture (_typeOfDetection);

			_startCounting = false;
			_canShowHelp = true;
			_counter = 0;
		}

        private float GetMaxDistance()
        {
            #if UNITY_EDITOR
            return 100;
            #endif

            switch (GameState.LevelOfDifficulty)
            {
                case (int)GameState.Level.VeryEasy: return 3;
                case (int)GameState.Level.Easy: return 1.5f;
                case (int)GameState.Level.Normal: return 0.7f;
                default: return 50;
            }
        }

        public void SetCardButton(TypeOfItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case TypeOfItem.Fish: SetCardButtonFacade(0); break;
                case TypeOfItem.Coral:
                case TypeOfItem.Anemone: SetCardButtonFacade(1); break;
                case TypeOfItem.Crustacean: SetCardButtonFacade(2); break;
                case TypeOfItem.Starfish:
                case TypeOfItem.Urchin: SetCardButtonFacade(3); break;
                case TypeOfItem.Mollusc: SetCardButtonFacade(4); break;
            }
        }

        private void SetCardButtonFacade(int n)
        {
            buttonShowCard.GetComponent<Image>().sprite = buttonCard[n].idle;
            buttonShowCard.GetComponent<Button>().spriteState = buttonCard[n].spriteState;
        }
    }
}
