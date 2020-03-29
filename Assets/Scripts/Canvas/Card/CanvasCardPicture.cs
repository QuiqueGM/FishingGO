using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using System.IO;
using VFG.Models;

namespace VFG.Canvas
{
    public class CanvasCardPicture : CanvasBase
    {
        public GameObject button;

        private Image _image;
        private TMP_Text _TXT_ScientificName;
        private RawImage _RAW_Picture;
        private Texture2D screenCapture;

        public override void Initialize()
        {
			_image = GetComponent<Image> ();
            _TXT_ScientificName = transform.Find("TXT_ScientificName").GetComponent<TMP_Text>();
            _RAW_Picture = transform.Find("RAW_Picture").GetComponent<RawImage>();

            base.Initialize();
        }

        public override void Populate()
        {
            int index = GameState.objectives.FindIndex(i => i.Id == GameState.currentObjectiveName);
            Collectable coll = GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName);

            _image.color = GameState.GetColorFromObjective (GameState.collectables.Find (i => i.Id == GameState.currentObjectiveName).Color, GameState.State.Solved);
            _TXT_ScientificName.text = coll.ScientificName;

            Texture2D picture = new Texture2D(GameState.resWidth, GameState.resHeight);
            byte[] bytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, GameState.currentObjectiveName + GameState.EXTENSION));
            picture.LoadImage(bytes);
            _RAW_Picture.texture = picture;

            base.Populate();
        }

		public override void ShowCard(GameObject card)
		{
			ShowPopUp (card);
			gameObject.SetActive (false);
		}
    }
}
