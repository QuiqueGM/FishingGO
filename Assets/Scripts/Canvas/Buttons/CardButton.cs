using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VFG.Core;

namespace VFG.Canvas
{
    public class CardButton : BaseButton
    {
        private Image _imgButton;
        private TMP_Text _TXT_Card;
        private TMP_Text _TXT_FilterName;
        private TMP_Text _TXT_FilterResult;
        private Image _image;
        private Text _textNumberOfItems;

        public void Initialize()
        {
            _imgButton = transform.Find("IMG_Back").GetComponent<Image>();
            _TXT_FilterName = transform.Find("TXT_FilterName").GetComponent<TMP_Text>();
            _TXT_FilterResult = transform.Find("TXT_FilterResult").GetComponent<TMP_Text>();
            _TXT_Card = transform.Find("TXT_Card").GetComponent<TMP_Text>();
            _image = transform.Find("IMG_Card").GetComponent<Image>();
        }

        public void FillButton(string pId, string pColor, string pFilterName, string pFilterResult, string pScientificName, Sprite picture, GameState.State pState)
        {
            _imgButton.color = GameState.GetColorFromObjective(pColor, pState);
            gameObject.name = pId;
            isUnlocked = pState == GameState.State.Solved ? true : false;
            GetComponent<Button>().interactable = isUnlocked;

            _TXT_FilterName.text = pFilterName;
            _TXT_FilterResult.text = pFilterResult;
            _TXT_Card.text = pScientificName;
            _image.sprite = picture;
            _image.color = isUnlocked ? Color.white : colorSecondaryButtonDisabledBlack;
        }

        public override void SendAction(TypeOfAction action)
        {
            GameState.currentObjectiveName = gameObject.name;
            base.SendAction(action);
        }
    }
}