using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using System.IO;
using VFG.Models;

namespace VFG.Canvas
{
    public class CanvasShareCard : CanvasBase
    {
        public GameObject button;

        private Image _image;
        private Image _IMG_BackgroundCircle;
        private TMP_Text _TXT_ScientificName;
        private RawImage _RAW_Picture;
        private Image _IMG_Family;
        private TMP_Text _TXT_Size;
        private TMP_Text _TXT_Depth;
        private TMP_Text _TXT_Place;
        private Texture2D screenCapture;

        public override void Initialize()
        {
			_image = GetComponent<Image> ();
			_IMG_BackgroundCircle = transform.Find("Container/IMG_BackgroundCircle").GetComponent<Image>();
			_TXT_ScientificName = transform.Find("Container/TXT_ScientificName").GetComponent<TMP_Text>();
			_RAW_Picture = transform.Find("Container/RAW_Picture").GetComponent<RawImage>();
			_IMG_Family = transform.Find("Container/IMG_Family").GetComponent<Image>();
			_TXT_Size = transform.Find("Container/TXT_Size").GetComponent<TMP_Text>();
			_TXT_Depth = transform.Find("Container/TXT_Depth").GetComponent<TMP_Text>();
			_TXT_Place = transform.Find("Container/TXT_Place").GetComponent<TMP_Text>();

            base.Initialize();
        }

        public override void Populate()
        {
            int index = GameState.objectives.FindIndex(i => i.Id == GameState.currentObjectiveName);
            Collectable c = GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName);

            _image.color = _IMG_BackgroundCircle.color = GameState.GetColorFromObjective (GameState.collectables.Find (i => i.Id == GameState.currentObjectiveName).Color, GameState.State.Solved);
            _IMG_Family.sprite = GameState.spritesPhylum.Find(i => i.name == GameState.GetSpriteFromTypeOfItem(GameState.collectables.Find(it => it.Id == GameState.currentObjectiveName).TypeOfItem));
            _TXT_ScientificName.text = c.ScientificName;
            _TXT_Size.text = GetSize(c);
            _TXT_Depth.text = GetDepth(c);
            _TXT_Place.text = c.Place;

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

        public void Share()
        {
            button.SetActive(false);
            Initialize();
            Populate();
            Show();
            StartCoroutine(SaveScreenshot());
        }

        public IEnumerator SaveScreenshot()
        {
            yield return new WaitForEndOfFrame();

            string sharePath = Path.Combine(Application.persistentDataPath, GameState.currentObjectiveName + GameState.SHARE + GameState.EXTENSION);

            screenCapture = new Texture2D(GameState.resWidth, GameState.resHeight, TextureFormat.RGB24, false);
            screenCapture.ReadPixels(new Rect(0, 0, GameState.resWidth, GameState.resHeight), 0, 0);
            screenCapture.Apply();

            byte[] bytes = screenCapture.EncodeToPNG();
            File.WriteAllBytes(sharePath, bytes);

            GetComponent<NativeShare>().Share(GameState.collectables.Find(n => n.Id == GameState.currentObjectiveName).ScientificName, sharePath, "");
            button.SetActive(true);
        }
    }
}
