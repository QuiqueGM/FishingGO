using UnityEngine;
using DG.Tweening;
using VFG.Core.Audio;
using VFG.Core;

namespace VFG.Canvas
{
	public class CanvasTryHarder : MonoBehaviour
    {
		const float TIME_TO_SHOW_BUTTONS = 0.3f;

		public event TakePictureRequest SendTakePicture;
        public GameObject tryHarder;
        public GameObject tooFar;
        private CanvasGroup _canvas;
        private Sequence sequence;
		private float _counter;

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
        }
        
		public void ShowCanvas(GameState.ObjectDetection typeOfDetection)
        {
            const float DURATION = 1;
            const float DELAY = 1f;

            ShowCanvas(typeOfDetection == GameState.ObjectDetection.NoDetected ? true : false);
            _canvas.alpha = 1;
            Handheld.Vibrate();
            AudioManager.Instance.PlayEffect(AudiosData.WRONG_OBJECTIVE);

            if (sequence != null) sequence.Kill();

            sequence = DOTween.Sequence();
			sequence.Append(DOTween.To(() => _canvas.alpha, a => _canvas.alpha = a, 0, DURATION).SetDelay(DELAY).OnComplete(OnComplete));
        }

		private void OnComplete()
		{
			gameObject.SetActive (false);
		}

        private void ShowCanvas(bool state)
        {
            tryHarder.SetActive(state);
            tooFar.SetActive(!state);
        }
    }
}
