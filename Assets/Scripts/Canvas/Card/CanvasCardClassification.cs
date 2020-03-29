using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using VFG.Core.Localization;

namespace VFG.Canvas
{
	public class CanvasCardClassification : CanvasBase
    {
        private Image _image;
        private TMP_Text _TXT_ScientificName;
        private TMP_Text _TXT_CommonName;
        private TMP_Text _TXT_Kingdom;
        private TMP_Text _TXT_Phylum;
        private TMP_Text _TXT_Class;
        private TMP_Text _TXT_Order;
        private TMP_Text _TXT_Family;
        private TMP_Text _TXT_Genus;
        private TMP_Text _TXT_Specie;

        public override void Initialize()
        {
            InitializeVariables();

            base.Initialize();
        }

        public void InitializeVariables()
        {
            _image = GetComponent<Image>();
            _TXT_ScientificName = transform.Find("TXT_ScientificName").GetComponent<TMP_Text>();
            _TXT_CommonName = transform.Find("TXT_CommonName").GetComponent<TMP_Text>();
            _TXT_Kingdom = transform.Find("TXT_Kingdom").GetComponent<TMP_Text>();
            _TXT_Phylum = transform.Find("TXT_Phylum").GetComponent<TMP_Text>();
            _TXT_Class = transform.Find("TXT_Class").GetComponent<TMP_Text>();
            _TXT_Order = transform.Find("TXT_Order").GetComponent<TMP_Text>();
            _TXT_Family = transform.Find("TXT_Family").GetComponent<TMP_Text>();
            _TXT_Genus = transform.Find("TXT_Genus").GetComponent<TMP_Text>();
            _TXT_Specie = transform.Find("TXT_Specie").GetComponent<TMP_Text>();
        }

        public override void Populate()
        {
            int index = GameState.collectables.FindIndex(i => i.Id == GameState.currentObjectiveName);

            _image.color = GameState.GetColorFromObjective(GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName).Color, GameState.State.Solved);

            _TXT_ScientificName.text = GameState.collectables[index].ScientificName;
            _TXT_CommonName.text = LoadLocalization.Instance.GetKey(GameState.currentObjectiveName);
            _TXT_Kingdom.text = GameState.collectables[index].Classification.Kingdom;
            _TXT_Phylum.text = GameState.collectables[index].Classification.Phylum;
            _TXT_Class.text = GameState.collectables[index].Classification.Class;
            _TXT_Order.text = GameState.collectables[index].Classification.Order;
            _TXT_Family.text = GameState.collectables[index].Classification.Family;
            _TXT_Genus.text = GameState.collectables[index].Classification.Genus;
            _TXT_Specie.text = GameState.collectables[index].Classification.Specie;

            base.Populate();
        }

		public override void ShowCard(GameObject card)
		{
			ShowPopUp (card);
			gameObject.SetActive (false);
		}
    }
}
