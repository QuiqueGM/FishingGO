using UnityEngine;
using UnityEngine.VR;
using VFG.Core.Localization;

namespace VFG.Core
{
    public enum Scenes { Editor, GameVR, GameAR }
    public delegate void GameIsInitializedRequest(Scenes sceneToload);

    public class InitializeGame : MonoBehaviour, ILoadingGame
    {
        public event GameIsInitializedRequest GameIsInitializeEvent;
        public Scenes sceneToLoad;

        private bool deleteAllKeys;
        private bool allAquariumsUnlocked;
        private bool allObjectvesUnlocked;
        private bool deleteRateKey;

        private LoadJSON loadJSON;
        private LoadSprites loadSprites;
        private LoadUserPreferences userPreferences;
        private LoadLocalization localizationTexts;

        private void Awake()
        {
			deleteAllKeys           = false;
            allAquariumsUnlocked    = false;
            allObjectvesUnlocked    = false;
            deleteRateKey           = false;
        }   

        void Start()
        {
            if (deleteAllKeys) PlayerPrefs.DeleteAll();

            LoadJSONFiles();
            LoadSprites();
            LoadLocalization();
            LoadPlayerPreferences();
            LoadFakeValues();
            SetResolutionScreen();
            LoadMainScene();

            DontDestroyOnLoad(gameObject);
        }

        public void LoadJSONFiles()
        {
            loadJSON = GetComponent<LoadJSON>();
            loadJSON.Init();
            Debug.Log("<color=#1bc71b>[Loading...]</color> JSON Files succesfully loaded");
        }

        public void LoadSprites()
        {
            loadSprites = GetComponent<LoadSprites>();
            loadSprites.Init();
            Debug.Log("<color=#1bc71b>[Loading...]</color> Sprites & textures succesfully loaded");
        }

        public void LoadLocalization()
        {
            localizationTexts = GetComponent<LoadLocalization>();
            localizationTexts.Init();
            Debug.Log("<color=#1bc71b>[Loading...]</color> Localization module succesfully loaded");
        }

        public void LoadPlayerPreferences()
        {
            userPreferences = GetComponent<LoadUserPreferences>();
            userPreferences.Init();
            Debug.Log("<color=#1bc71b>[Loading...]</color> Player preferences succesfully loaded");
        }

        private void SetResolutionScreen()
        {
            GameState.resWidth = Screen.width;
            GameState.resHeight = Screen.height;
            Debug.Log(string.Format("<color=#1bc71b>[Set resolution...]</color> Width: {0} | Height: {1}", GameState.resWidth, GameState.resHeight));
        }

        public void LoadMainScene()
        {
            Debug.Log("<color=#1bc71b>[Loading...]</color> All modules succesfully loaded! Selecting proper SDK...");
            if (sceneToLoad == Scenes.GameAR) VRSettings.LoadDeviceByName(string.Empty);
            GameIsInitializeEvent(sceneToLoad);
        }
        
        #region FAKE
        private void LoadFakeValues()
        {
            //GameState.AquariumsState = GameState.FIRST_TIME_USER_XPERIENCE;
            //GameState.Language = "English";
            //GameState.AquariumsState = "1,1,1,1,1,1,1,1,1,1";
            //GameState.ObjectivesState = "AcroporaCervicornis,2,AcroporaHorrida,2,AcroporaHumilis,2,AcroporaRobusta,1,CryptodendrumAdhaesivum,0,DiploriaStrigosa,0,DiscosomaCarlgreni,0,DiscosomaMalaccensis,0,DiscosomaMutabilis,0,DiscosomaSp,0,FaviaRotundata,0,GorgoniaFlabellum,0,HelioporaCoerulea,0,PocilloporaVerrucosa,0,PodabaciaCrustacea,0,PodabaciaSinai,0,PoritesNodifera,0,PoritesProfundus,0,Sarcophyton,0,CulcitaSchmideliana,0,DavidasterRubiginosus,0,EchinothrixDiadema,0,LinckiaLaevigata,0,ProtoreasterLinckii,0,CaulerpaProlifera,0,CaulerpaRacemosa,0,CaulerpaSertularioides,0,CaulerpaTaxifolia,0,CymodoceaNodosa,0,PenicillusSp,0,PosidoniaOceanica,0,ZosteraMarina,0,AcanthurusAchilles,0,AcanthurusGuttatus,0,AcanthurusJaponicus,0,AcanthurusLeucosternon,0,AcanthurusNigricans,0,AcanthurusPyroferus,0,AcanthurusTennentii,0,AcanthurusTriostegus,0,AcanthurusTristis,0,CtenochaetusStrigosus,0,ZebrasomaFlavescens,0,ZebrasomaVeliferum,0,ZebrasomaXanthurum,0,ZebrasomaScopas,0,ZebrasomaDesjardinii,0";

            if (deleteRateKey) PlayerPrefs.DeleteKey("GameHasBeenRated");

            if (allAquariumsUnlocked)
            {
                GameState.aquariumsState.Clear();

                for (int n = 0; n < GameState.aquariums.Count; n++)
                    GameState.aquariumsState.Add((int)GameState.State.Unlocked);
            }

            if (allObjectvesUnlocked)
            {
                string[] listOfSolvedObjectives = GameState.ObjectivesState.Split(new[] { ',' });
                GameState.objectivesState.Clear();

                for (int n = 0; n < listOfSolvedObjectives.Length; n = n + 2)
                    GameState.objectivesState.Add(listOfSolvedObjectives[n], (int)GameState.State.Solved);

                //string str = string.Empty;
                //foreach (KeyValuePair<string, int> item in GameState.objectivesState)
                //    str = string.Format("{0}{1},{2},", str, item.Key, item.Value);

                //str = str.Substring(0, str.Length - 1);
                //Debug.Log(str);
            }
        }
        #endregion

        private void Update()
        {
            //Debug.Log("Play from New Objective: " + GameState.playFromNewObjective);
            //Debug.Log("Current Aquarium: " + GameState.currentAquarium);
            //Debug.Log("Current Objective: " + GameState.currentObjective);
            //Debug.Log("Aquarium Name: " + GameState.currentAquariumName);
            //Debug.Log("Objective name: " + GameState.currentObjectiveName);
            //Debug.Log("Can I Press a button? " + GameState.canPressAButton);
        }
    }
}