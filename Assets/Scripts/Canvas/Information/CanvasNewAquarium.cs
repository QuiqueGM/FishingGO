using UnityEngine;
using TMPro;
using VFG.Core;
using VFG.Core.Localization;

namespace VFG.Canvas
{
    public class CanvasNewAquarium : CanvasBase
    {
        public GameObject buttonAquarium;
        public GameObject buttonContinue;

        private TMP_Text _TXT_Aquarium;

        public void SetButtons(bool isInMenu)
        {
            buttonAquarium.SetActive(isInMenu);
            buttonContinue.SetActive(!isInMenu);
        }

        public override void Initialize()
        {
            _TXT_Aquarium = transform.Find("ImgBackground/TXT_Aquarium").GetComponent<TMP_Text>();
            base.Initialize();
        }

        public override void Populate()
        {
            _TXT_Aquarium.text = LoadLocalization.Instance.GetKey(GameState.newAquariumUnlockedName);
            base.Populate();
        }
    }
}
