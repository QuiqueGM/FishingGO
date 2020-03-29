using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using VFG.Core;

namespace VFG.Canvas
{
    public class CanvasObjectiveSolved : CanvasBase
    {
        public GameObject buttonShowCard;
        public GameObject buttonContinue;

        public GameObject objectiveSolved;
		public GameObject objectiveReplaced;
        
        private RawImage _image;

        public void Init(Texture2D screenCap, bool isReplaced)
        {
            _image = transform.Find("RAW_Picture").GetComponent<RawImage>();
            _image.texture = screenCap;

            buttonShowCard.SetActive(true);
            buttonContinue.SetActive(false);

            ToggleText (isReplaced);
        }

        public void ReplacePicture(string oldPath, string newPath)
        {
            File.Delete(oldPath);
            File.Move(newPath, oldPath);
            Texture2D picture = new Texture2D(GameState.resWidth, GameState.resHeight);
            byte[] bytes = File.ReadAllBytes(oldPath);
            picture.LoadImage(bytes);

			Init(picture, true);
        }

		private void ToggleText(bool isReplaced)
		{
			objectiveSolved.SetActive (!isReplaced);
			objectiveReplaced.SetActive (isReplaced);
		}

        public void ChangeButton()
        {
            StartCoroutine(ChangeButton(0.2f));
        }

        IEnumerator ChangeButton(float n)
        {
            yield return new WaitForSeconds(n);
            buttonShowCard.SetActive(false);
            buttonContinue.SetActive(true);
        }
    }
}