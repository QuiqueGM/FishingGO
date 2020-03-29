using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using VFG.Core.Localization;

namespace VFG.Canvas
{
	public class CanvasCardQuiz : CanvasBase
    {
        public Sprite question;
        public Sprite answer;

        private Image _quizButton;
        private Image _image;
        private Image _IMG_Family;
        private TMP_Text _TXT_ScientificName;
        private TMP_Text _TXT_CommonName;
        private TMP_Text _TXT_Quiz;
        private bool _toggleQuiz = false;

        public override void Initialize()
        {
            _image = GetComponent<Image>();
            _quizButton = transform.Find("BTN_Answer").GetComponent<Image>();
            _IMG_Family = transform.Find("IMG_Family").GetComponent<Image>();
            _TXT_ScientificName = transform.Find("TXT_ScientificName").GetComponent<TMP_Text>();
            _TXT_CommonName = transform.Find("TXT_CommonName").GetComponent<TMP_Text>();
            _TXT_Quiz = transform.Find("TXT_Quiz").GetComponent<TMP_Text>();

            base.Initialize();
        }

        public override void Populate()
        {
            ToggleAnswer();

            int index = GameState.objectives.FindIndex(i => i.Id == GameState.currentObjectiveName);

            _image.color = GameState.GetColorFromObjective(GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName).Color, GameState.State.Solved);
            _IMG_Family.sprite = GameState.spritesPhylum.Find(i => i.name == GameState.GetSpriteFromTypeOfItem(GameState.collectables.Find(it => it.Id == GameState.currentObjectiveName).TypeOfItem));
            _TXT_ScientificName.text = GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName).ScientificName;
            _TXT_CommonName.text = LoadLocalization.Instance.GetKey(GameState.currentObjectiveName);

            gameObject.SetActive(true);
        }

		public override void ShowCard(GameObject card)
		{
			ShowPopUp (card);
			gameObject.SetActive (false);
		}

        public void ToggleAnswer()
        {
            _toggleQuiz = !_toggleQuiz;
            _TXT_Quiz.text = _toggleQuiz ? "Far far away, behind the word mountains, far from the countries Vokalia?" : "Anwerrrrrrr rrrrrrrrrrr   rrrrr rrrrrr  r rrrrrrrrrr r r rr rrrrrrrrr";
            _quizButton.sprite = _toggleQuiz ?  answer : question;
        }
    }
}
