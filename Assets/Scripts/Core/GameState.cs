using VFG.Models;
using System.Collections.Generic;
using UnityEngine;

namespace VFG.Core
{
    public class GameState
    {
        public const string MAIN_MENU = "MainMenu";
        public const string AQUARIUM = "Aquarium";
        public const string EXTENSION = ".png";
        public const string TEMPORARY = "Temp";
        public const string SHARE = "Share";

        public const string OBJECTIVE = "Objective";
        public const string PATH_AQUARIUMS = "Aquariums";
        public const string PATH_FIHSSCHOOL = "Fishschools/FishSchool";
        public const string PATH_PREFABS = "Prefabs/Items";
        public const string PATH_OBJECTIVES = "Prefabs/Objectives";
        public const string ITEM_LIBRARY = "ItemLibrary";
        public const string ITEM_SCENE = "ItemScene";
        public const string CHORDATA = "Chordata";
        public const string FISHES = "Fishes";
        public const string ANEMONES = "Anemones";
        public const string CNIDARIANS = "Cnidarians";
        public const string CORALS = "Corals";
        public const string ECHINODERMS = "Echinoderms";
        public const string ROCKS = "Rocks";
        public const string MOLLUSCS = "Molluscs";
        public const string CRUSTACEANS = "Crustaceans";
        public const string PLANTS = "Plants";
        public const string NON_CLASSIFIED = "Cnidarians/NonClassified";
        public const string FISHSCHOOL_OBJECTIVE = "FishSchoolObjective";
        public const string FAMILY = "#FAMILY#";
        public const string GENUS = "#GENUS#";
        public const string SIZE = "#SIZE#";
        public const string DEPTH = "#DEPTH#";
        public const string VERY_EASY = "#VERYEASY#";
        public const string EASY = "#EASY#";
        public const string NORMAL = "#NORMAL#";

        public enum Toggle
        {
            Off = 0,
            On = 1
        }

        public enum State
        {
            Locked = 0,
            Unlocked = 1,
            Solved = 2
        }

		public enum Level
		{
            VeryEasy = 0,
            Easy = 1,
            Normal = 2
        }

        public enum Scene
        {
            MainMenu,
            Scanning,
            InGame
        }

        public enum ObjectDetection
        {
            IsDetected,
            DetectedButTooFar,
            NoDetected
        }

        public static string FIRST_TIME_USER_XPERIENCE = "First Time User Experience";
        public static bool isFirstUseApp = true;
        public static Scene isInScene = Scene.MainMenu;
        public static int NUMBER_OF_COMING_SOON_AQUARIUMS = 4;

        public static List<Aquarium> aquariums = new List<Aquarium>();
        public static List<Objective> objectives = new List<Objective>();
        public static List<Collectable> collectables = new List<Collectable>();
        public static List<Sprite> spritesAquariums = new List<Sprite>();
        public static List<Sprite> spritesObjectives = new List<Sprite>();
        public static List<Sprite> spritesPhylum = new List<Sprite>();
        public static List<int> aquariumsState = new List<int>();
        public static Dictionary<string, int> objectivesState = new Dictionary<string, int>();

        public static bool canPressAButton = true;
        public static bool playFromNewObjective = false;
        public static bool newAquariumIsUnlocked = false;
        public static bool allObjectivesUnlockedInAquarium = false;
        public static bool allObjectivesUnlockedInGame = false;

        public static int currentAquarium = 0;
        public static string currentAquariumName = string.Empty;
        public static int newAquariumUnlocked = 0;
        public static string newAquariumUnlockedName = string.Empty;
        public static int currentObjective = 0;
        public static string currentObjectiveName = string.Empty;
        //public static GameObject currentItem = null;

        public static int resWidth;
        public static int resHeight;

        public static int FirstTimeIstUsed
        {
            get { return PlayerPrefs.GetInt("FirstTimeIstUsed", (int)Toggle.On); }
            set { PlayerPrefs.SetInt("FirstTimeIstUsed", value); }
        }

        public static int ShowHelpAtTheBeginning
        {
            get { return PlayerPrefs.GetInt("ShowHelpAtTheBeginning", (int)Toggle.On); }
            set { PlayerPrefs.SetInt("ShowHelpAtTheBeginning", value); }
        }

        public static string AquariumsState
        {
            get { return PlayerPrefs.GetString("AquariumsState", FIRST_TIME_USER_XPERIENCE); }
            set { PlayerPrefs.SetString("AquariumsState", value); }
        }

        public static string ObjectivesState
        {
            get { return PlayerPrefs.GetString("ObjectivesState"); }
            set { PlayerPrefs.SetString("ObjectivesState", value); }
        }

//        public static int NextAquarium
//        {
//            get { return PlayerPrefs.GetInt("NextAquarium", 0); }
//            set { PlayerPrefs.SetInt("NextAquarium", value); }
//        }
//
//        public static int NextObjective
//        {
//            get { return PlayerPrefs.GetInt("NextObjective", 0); }
//            set { PlayerPrefs.SetInt("NextObjective", value); }
//        }

        public static int AllObjectivesSolved
        {
            get { return PlayerPrefs.GetInt("AllObjectivesSolved", (int)Toggle.Off); }
            set { PlayerPrefs.SetInt("AllObjectivesSolved", value); }
        }

        public static string Language
        {
            get { return PlayerPrefs.GetString("Language", "English"); }
            set { PlayerPrefs.SetString("Language", value); }
        }

        public static int Effects
        {
            get { return PlayerPrefs.GetInt("Effects", (int)Toggle.On); }
            set { PlayerPrefs.SetInt("Effects", value); }
        }

        public static int Music
        {
            get { return PlayerPrefs.GetInt("Music", (int)Toggle.On); }
            set { PlayerPrefs.SetInt("Music", value); }
        }

		public static int AdvancedShadows
		{
			get { return PlayerPrefs.GetInt("AdvancedShadows", (int)Toggle.On); }
			set { PlayerPrefs.SetInt("AdvancedShadows", value); }
		}

		public static int AdvancedLighting
		{
			get { return PlayerPrefs.GetInt("AdvancedLighting", (int)Toggle.On); }
			set { PlayerPrefs.SetInt("AdvancedLighting", value); }
		}

		public static int LevelOfDifficulty
		{
			get { return PlayerPrefs.GetInt("LevelOfDifficulty", (int)Level.Normal); }
			set { PlayerPrefs.SetInt("LevelOfDifficulty", value); }
		}

		public static Color textColorDisabled = new Color(0, 0, 0, 0.18f);

		public static Color GetColorFromObjective(string code, State state)
        {
            Color col = Color.white;

            if (state == State.Locked)
            {
                switch (code)
                {
					case "1B": col = new Color32(151,162,165, 255); break;
					case "2B": col = new Color32(93,114,123, 255); break;
					case "3B": col = new Color32(173,182,184, 255); break;
					case "4B": col = new Color32(133,151,155, 255); break;
					case "1Y": col = new Color32(182,178,155, 255); break;
					case "1BR": col = new Color32(150,149,139, 255); break;
					case "2BR": col = new Color32(174,171,157, 255); break;
					case "1G": col = new Color32(162,169,160, 255); break;
					case "2G": col = new Color32(167,175,157, 255); break;
					case "3G": col = new Color32(159,163,145, 255); break;
					case "1O": col = new Color32(193,173,162, 255); break;
					case "2O": col = new Color32(196,181,170, 255); break;
					case "1RE": col = new Color32(176,153,153, 255); break;
					case "2RE": col = new Color32(181,165,166, 255); break;
					case "1P": col = new Color32(169,164,175, 255); break;
                }
            }
            else
            {
                switch (code)
                {
					case "1B": col = new Color32(141, 169, 175, 255); break;
					case "2B": col = new Color32(58, 129, 159, 255); break;
					case "3B": col = new Color32(160, 191, 198, 255); break;
					case "4B": col = new Color32(108, 167, 181, 255); break;
					case "1Y": col = new Color32(214, 201, 124, 255); break;
					case "1BR": col = new Color32(163, 161, 126, 255); break;
					case "2BR": col = new Color32(194, 185, 138, 255); break;
					case "1G": col = new Color32(158, 180, 150, 255); break;
					case "2G": col = new Color32(171, 197, 136, 255); break;
					case "3G": col = new Color32(171, 185, 124, 255); break;
					case "1O": col = new Color32(229, 161, 126, 255); break;
					case "2O": col = new Color32(226, 176, 141, 255); break;
					case "1RE": col = new Color32(203, 127, 127, 255); break;
					case "2RE": col = new Color32(200, 146, 151, 255); break;
					case "1P": col = new Color32(169, 152, 187, 255); break;
                }
            }
            return col;
        }

        public static string GetSpriteFromTypeOfItem(TypeOfItem item)
        {
            switch (item)
            {
                case TypeOfItem.Coral:
                case TypeOfItem.Anemone: return CNIDARIANS;
                case TypeOfItem.Crustacean: return CRUSTACEANS;
                case TypeOfItem.Echinoderms:
                case TypeOfItem.Starfish:
                case TypeOfItem.Urchin: return ECHINODERMS;
                case TypeOfItem.Mollusc: return MOLLUSCS;
                case TypeOfItem.Fish:
                case TypeOfItem.MorayEel:
                case TypeOfItem.Seahorse: return CHORDATA;
                default: return null;
            }
        }
    }
}