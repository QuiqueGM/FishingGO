using UnityEngine;
using VFG.Core;
using VFG.Core.Audio;

namespace VFG.Utils
{
    public class FadeIn : MonoBehaviour {

        public float fadeDuration = 5.0f;
        private CanvasGroup canvas;

        void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
            GameState.canPressAButton = true;
			AudioManager.Instance.PlayMusic ();
        }

        void Update()
        {
            canvas.alpha -= Time.deltaTime / fadeDuration;

            if (canvas.alpha <= 0)
                Destroy(gameObject);
        }
    }
}
