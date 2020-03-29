using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using VFG.Models;
using VFG.Core.Localization;

namespace VFG.Canvas
{
    public class CanvasNewName : CanvasBase
    {
        private Image _image;
        private TMP_Text _TXT_ScientificName;
        private TMP_Text _TXT_CommonName;
        private Image _IMG_Objective;

        public override void Initialize()
        {
            _image = GetComponent<Image>();

            _TXT_ScientificName = transform.Find("TXT_ScientificName").GetComponent<TMP_Text>();
            _TXT_CommonName = transform.Find("TXT_CommonName").GetComponent<TMP_Text>();
            _IMG_Objective = transform.Find("IMG_Objective").GetComponent<Image>();

            base.Initialize();
        }

        public override void Populate()
        {
            Collectable coll = GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName);

            _image.color = GameState.GetColorFromObjective(coll.Color, GameState.State.Solved);
            _TXT_ScientificName.text = coll.ScientificName;
            _TXT_CommonName.text = LoadLocalization.Instance.GetKey(GameState.currentObjectiveName);
            _IMG_Objective.sprite = GameState.spritesObjectives.Find(i => i.name == GameState.currentObjectiveName);

            base.Populate();
        }

        public override void ShowCard(GameObject card)
        {
            ShowPopUp(card);
            gameObject.SetActive(false);
        }
    }
}
