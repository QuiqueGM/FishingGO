using UnityEngine;

namespace VFG.Utils
{
    public class FadeOut : MonoBehaviour {

        private bool startToFade;
        private float _time;
        private string _sceneToLoad;
        private CanvasGroup canvas;

        void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        void Update()
        {
            if (startToFade)
            {
                if (canvas.alpha >= 1)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneToLoad);
                else
                    canvas.alpha += Time.deltaTime / _time;
            }
        }

        public void Init(string sceneToLoad, float time = 1.0f)
        {
            _sceneToLoad = sceneToLoad;
            _time = time;
            startToFade = true;
        }
    }
}
