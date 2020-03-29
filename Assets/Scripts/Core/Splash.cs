using System;
using UnityEngine;

namespace VFG.Core
{
    public class Splash : MonoBehaviour
    {
        public InitializeGame initGame;

        private Scenes _sceneToLoad;
        private bool _gameIsLoad = false;
        private bool _animationsIsComplete = false;

        void Awake()
        {
            initGame.GameIsInitializeEvent += LoadGame;
        }

        private void OnDestroy()
        {
            initGame.GameIsInitializeEvent -= LoadGame;
        }

        private void Update()
        {
            if (_gameIsLoad && _animationsIsComplete)
            {
                Debug.Log(string.Format("<color=#1bc71b>[Loading...]</color> Loading <color=orange>{0}</color> Scene...", _sceneToLoad.ToString()));
                UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneToLoad.ToString());
            }
        }

        public void LoadGame(Scenes sceneToLoad)
        {
            _sceneToLoad = sceneToLoad;
            _gameIsLoad = true;
        }

        public void AnimationSplashIsComplete()
        {
            _animationsIsComplete = true;
        }
    }
}