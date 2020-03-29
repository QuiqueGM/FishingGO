using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using VFG.Core.Localization;
using VFG.Models;

namespace VFG.Canvas
{
	public class CanvasCardMoreSide : CanvasBase
    {
        private Image _image;
        private Image _IMG_Family;
        private TMP_Text _TXT_ScientificName;
        private TMP_Text _TXT_CommonName;
        private TMP_Text _TXT_Family;
        private TMP_Text _TXT_Description;
        private TMP_Text _TXT_Size;
        private TMP_Text _TXT_Depth;
        private TMP_Text _TXT_Place;


        public override void Initialize()
        {
            _image = GetComponent<Image>();

            _IMG_Family = transform.Find("IMG_Family").GetComponent<Image>();
            _TXT_ScientificName = transform.Find("TXT_ScientificName").GetComponent<TMP_Text>();
            _TXT_CommonName = transform.Find("TXT_CommonName").GetComponent<TMP_Text>();
            _TXT_Family = transform.Find("TXT_Family").GetComponent<TMP_Text>();
            _TXT_Description = transform.Find("TXT_Description").GetComponent<TMP_Text>();
            _TXT_Size = transform.Find("TXT_Size").GetComponent<TMP_Text>();
            _TXT_Depth = transform.Find("TXT_Depth").GetComponent<TMP_Text>();
            _TXT_Place = transform.Find("TXT_Place").GetComponent<TMP_Text>();

            base.Initialize();
        }

        public override void Populate()
        {
            Collectable c = GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName);

            _image.color = GameState.GetColorFromObjective(GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName).Color, GameState.State.Solved);

            _IMG_Family.sprite = GameState.spritesPhylum.Find(i => i.name == GameState.GetSpriteFromTypeOfItem(GameState.collectables.Find(it => it.Id == GameState.currentObjectiveName).TypeOfItem));
            _TXT_Family.text = _IMG_Family.sprite.name;
            _TXT_ScientificName.text = c.ScientificName;
            _TXT_CommonName.text = LoadLocalization.Instance.GetKey(GameState.currentObjectiveName);
            _TXT_Description.text = LoadLocalization.Instance.GetKey(string.Format("DSC_{0}", GameState.currentObjectiveName));
            _TXT_Size.text = GetSize(c);
            _TXT_Depth.text = GetDepth(c);
            _TXT_Place.text = c.Place;

            base.Populate();
        }

		public override void ShowCard(GameObject card)
		{
			ShowPopUp (card);
			gameObject.SetActive (false);
		}


    }
}
