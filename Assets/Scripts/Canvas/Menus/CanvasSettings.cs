using TMPro;
using UnityEngine;
using VFG.Core;
using UnityEngine.UI;
using VFG.Core.Audio;
using UnityEngine.iOS;
using VFG.Core.Localization;

namespace VFG.Canvas
{
	public class CanvasSettings : CanvasBase
    {
		private GameState.Level difficulty;
		private Toggle _TGL_Music;
		private Toggle _TGL_Effects;
		private Toggle _TGL_Shadows;
		private Toggle _TGL_Lighting;
		private Toggle _TGL_Help;
        private GameObject _TXT_Difficulty;

        public void Awake()
		{
			_TGL_Music = transform.Find ("TGL_Music").GetComponent<Toggle> ();
			_TGL_Effects = transform.Find ("TGL_Effects").GetComponent<Toggle> ();
			_TGL_Shadows = transform.Find ("TGL_Shadows").GetComponent<Toggle> ();
			_TGL_Lighting = transform.Find ("TGL_Lighting").GetComponent<Toggle> ();
			_TGL_Help = transform.Find ("TGL_Help").GetComponent<Toggle> ();
            _TXT_Difficulty = transform.Find("BTN_Difficulty/TXT_Difficulty").gameObject;
        }

		public override void Initialize()
		{
            difficulty = (GameState.Level)GameState.LevelOfDifficulty;
            _TXT_Difficulty.GetComponent<LocalizeText>().SetInitKey(ChangeTextDifficulty(difficulty));

            SetToggles ();

//			if (Device.generation == DeviceGeneration.iPadUnknown || 
//				Device.generation == DeviceGeneration.iPad4Gen ||
//				Device.generation == DeviceGeneration.iPad5Gen || 
//				Device.generation == DeviceGeneration.iPadPro10Inch1Gen || 
//				Device.generation == DeviceGeneration.iPadPro1Gen ||
//				Device.generation == DeviceGeneration.iPadPro2Gen ||
//				Device.generation == DeviceGeneration.iPadPro10Inch2Gen
//			    )
//
//			ChangeResolution (2);

            base.Initialize();
        }

		private void SetToggles()
		{
            _TGL_Music.isOn = GameState.Music == (int)GameState.Toggle.On ? false : true;
			_TGL_Effects.isOn = GameState.Effects == (int)GameState.Toggle.On ? false : true;

            if (GameState.FirstTimeIstUsed == (int)GameState.Toggle.On)
            {
                GameState.FirstTimeIstUsed = (int)GameState.Toggle.Off;
                _TGL_Shadows.isOn = !(Device.generation == DeviceGeneration.iPadUnknown || Device.generation == DeviceGeneration.iPhoneUnknown);
                _TGL_Lighting.isOn = !(Device.generation == DeviceGeneration.iPadUnknown || Device.generation == DeviceGeneration.iPhoneUnknown);
				_TGL_Help.isOn = false;
            }
            else
            {
                _TGL_Shadows.isOn = GameState.AdvancedShadows == (int)GameState.Toggle.On ? false : true;
                _TGL_Lighting.isOn = GameState.AdvancedLighting == (int)GameState.Toggle.On ? false : true;
				_TGL_Help.isOn = GameState.ShowHelpAtTheBeginning == (int)GameState.Toggle.On ? false : true;
            }

            ToggleMusic();
            ToggleEffects();
            ToggleLigthing();
            ToggleShadows();
			ToggleHelp ();
        }
      
//        public void ChangeResolution(int percent)
//        {
//			Debug.Log ("Changing resolution!!");
//			Screen.SetResolution(Screen.width / percent, Screen.height / percent, true);
//			GameState.resWidth = Screen.width;
//			GameState.resHeight = Screen.height;
//        }

		public void ChangeLanguage()
		{
			LoadLocalization.Instance.ChangeLanguage();
			GameState.Language = LoadLocalization.Instance.GetCurrentLanguage;
		}

		public void ChangeDifficulty()
		{
            difficulty++;

            if (difficulty > GameState.Level.Normal)
                difficulty = GameState.Level.VeryEasy;

            _TXT_Difficulty.GetComponent<TMP_Text>().text = LoadLocalization.Instance.GetKey(ChangeTextDifficulty(difficulty));
            _TXT_Difficulty.GetComponent<LocalizeText>().SetInitKey(ChangeTextDifficulty(difficulty));

            GameState.LevelOfDifficulty = (int)difficulty;
		}

		private string ChangeTextDifficulty(GameState.Level dif)
		{
			switch (dif) 
			{
				case GameState.Level.VeryEasy: return GameState.VERY_EASY;
				case GameState.Level.Easy: return GameState.EASY;
				case GameState.Level.Normal: return GameState.NORMAL;
				default: return GameState.NORMAL;
			}
		}

		public void ToggleMusic()
		{
			GameState.Music = _TGL_Music.isOn ? (int)GameState.Toggle.Off : (int)GameState.Toggle.On;
			AudioManager.Instance.Mute ((int)Channel.Music, _TGL_Music.isOn);
		}

		public void ToggleEffects()
		{
			GameState.Effects = _TGL_Effects.isOn ? (int)GameState.Toggle.Off : (int)GameState.Toggle.On;
            AudioManager.Instance.Mute ((int)Channel.Effects, _TGL_Effects.isOn);
        }

		public void ToggleLigthing()
		{
			GameState.AdvancedLighting = _TGL_Lighting.isOn ? (int)GameState.Toggle.Off : (int)GameState.Toggle.On;
        }

		public void ToggleShadows()
		{
			GameState.AdvancedShadows = _TGL_Shadows.isOn ? (int)GameState.Toggle.Off : (int)GameState.Toggle.On;
        }

		public void ToggleHelp()
		{
			GameState.ShowHelpAtTheBeginning = _TGL_Help.isOn ? (int)GameState.Toggle.Off : (int)GameState.Toggle.On;
		}

        public void ResetGame()
        {
            PlayerPrefs.DeleteKey("FirstTimeIstUsed");
            PlayerPrefs.DeleteKey("AquariumsState");
            PlayerPrefs.DeleteKey("ObjectivesState");
            PlayerPrefs.DeleteKey("NextAquarium");
            PlayerPrefs.DeleteKey("NextObjective");
            PlayerPrefs.DeleteKey("AllObjectivesSolved");
            GameState.newAquariumUnlocked = 0;
            GameState.currentAquarium = 0;
            GameState.currentObjective = 0;
            LoadUserPreferences.Instance.Init();
        }
	}
}