using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;

namespace VFG.Canvas
{
    public class PopulateObjectivesButtons : MonoBehaviour
    {
        private Image _imgButton;
        private GameObject _btnPlay;
        private GameObject _btnShowCard;

        public void Initialize()
        {
            _imgButton = GetComponent<Image>();
            _btnPlay = transform.Find("BTN_Play").gameObject;
            _btnShowCard = transform.Find("BTN_ShowCard").gameObject;
        }

        public void FillButton(string pId, string pColor, Sprite pBackground, GameState.State pState)
        {
            gameObject.name = pId;
            _imgButton.color = GameState.GetColorFromObjective(pColor, pState);
			_btnPlay.GetComponent<ObjectiveButton>().SetButtonProperties(pBackground, (int)pState);
			_btnShowCard.GetComponent<ShowCardButton>().SetButtonProperties((int)pState);
        }
    }
}