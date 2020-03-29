using UnityEngine;
using TMPro;
using VFG.Core;

namespace VFG.Canvas
{
    public class PopulateAquariumButtons : MonoBehaviour
    {
        private TMP_Text _textAquarium;
        private GameObject _btnPlayAquarium;
        private GameObject _btnShowAquarium;
        private TMP_Text _textNumberObjectives;
        private GameObject _TXT_ComingSoon;

        public void Initialize()
        {
            _textAquarium = transform.Find("TXT_Aquarium").GetComponent<TMP_Text>();
            _btnPlayAquarium = transform.Find("BTN_PlayAquarium").gameObject;
            _btnShowAquarium = transform.Find("BTN_ShowAquarium").gameObject;
            _textNumberObjectives = transform.Find("TXT_ShowProgress").GetComponent<TMP_Text>();
            _TXT_ComingSoon = transform.Find("TXT_ComingSoon").gameObject;
        }

        public void FillButton(string pId, bool pComingSoon, string pName, Sprite pBackground, bool pIsUnlocked, string pNumOfObjectives)
        {
            gameObject.name = pId;

            _textAquarium.text = pName;
            _textAquarium.color = pIsUnlocked ? Color.white : GameState.textColorDisabled;
            _textNumberObjectives.text = pNumOfObjectives;
            _textNumberObjectives.color = pIsUnlocked ? Color.white : GameState.textColorDisabled;
            _textNumberObjectives.gameObject.SetActive(pComingSoon ? false : true);
            //_textNumberObjectives.GetComponent<LocalizeTextWithParameters>().AddParameter(pNumOfObjectives);

            _TXT_ComingSoon.SetActive(pComingSoon ? true : false);
            _btnPlayAquarium.GetComponent<AquariumButton>().SetButtonProperties(pBackground, pIsUnlocked);
            _btnShowAquarium.GetComponent<AquariumButton>().SetSecondaryButton(pIsUnlocked);
        }
    }
}