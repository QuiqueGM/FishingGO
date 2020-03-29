using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VFG.Core
{
    public class LoadUserPreferences : MonoBehaviour
    {
        static public LoadUserPreferences Instance;

        public void Init()
        {
            Instance = this;

            if (GameState.AquariumsState == GameState.FIRST_TIME_USER_XPERIENCE)
            {
                GameState.AquariumsState = PopulateArray(((int)GameState.State.Unlocked).ToString(), GameState.aquariums.Count);
                InitAquariumsState();
                InitObjectivesState();
                UnlockFirstObjective(0);
            }
            else
            {
                LoadAquariumsState();
                LoadObjectivesState();
            }
        }

        #region PRIVATE METHODS
        private void InitAquariumsState()
        {
            GameState.aquariumsState.Clear();
            string[] strSplit = GameState.AquariumsState.Split(new[] { ',' });

            for (int n = 0; n < strSplit.Length; n++)
                GameState.aquariumsState.Add(int.Parse(strSplit[n]));
        }

        private void InitObjectivesState()
        {
            GameState.objectivesState.Clear();
            for (int n = 0; n < GameState.objectives.Count; n++)
            {
                if (!GameState.objectivesState.Keys.Any(key => key.Contains(GameState.objectives[n].Id)))
                    GameState.objectivesState.Add(GameState.objectives[n].Id, (int)GameState.State.Locked);
            }

            GameState.ObjectivesState = SaveDiccionaryToPlayerPrefs(GameState.objectivesState);
        }

        private string SaveDiccionaryToPlayerPrefs(Dictionary<string, int> diccionary)
        {
            string str = string.Empty;

            foreach (KeyValuePair<string, int> item in diccionary)
                str = string.Format("{0}{1},{2},", str, item.Key, item.Value);

            return str.Substring(0, str.Length - 1);
        }

        private void LoadAquariumsState()
        {
            InitAquariumsState();

            if (GameState.aquariums.Count > GameState.aquariumsState.Count)
            {
                Debug.Log("<color=Orange>New Aquariums found in JSON file.</color> Adding new Aquariums to PlayerPrefs");

                int numberOfNewAquariums = GameState.aquariums.Count - GameState.aquariumsState.Count;
                GameState.AquariumsState = PopulateArray(GameState.AquariumsState, numberOfNewAquariums + 1);

                InitAquariumsState();
            }
        }

        private void LoadObjectivesState()
        {
            string[] listOfSolvedObjectives = GameState.ObjectivesState.Split(new[] { ',' });

            for (int n = 0; n < listOfSolvedObjectives.Length; n = n + 2)
            {
                try
                {
                    GameState.objectivesState.Add(listOfSolvedObjectives[n], int.Parse(listOfSolvedObjectives[n + 1]));
                    GameState.collectables.Find(i => i.Id == listOfSolvedObjectives[n]).State = (GameState.State)System.Enum.Parse(typeof(GameState.State), listOfSolvedObjectives[n + 1]);
                }
                catch
                {
                    Debug.LogError(string.Format("The element <color=red>{0}</color> already exists in the dictionary was not present in the dictionary.", listOfSolvedObjectives[n]));
                }
            }

            if (GameState.objectives.Count > GameState.objectivesState.Count)
            {
                Debug.Log("<color=Orange>New Itmes found in JSON file.</color> Adding new Items to PlayerPrefs");
                InitObjectivesState();
            }
        }

        private string PopulateArray(string firstNumber, int numItems)
        {
            string str = firstNumber;

            for (int n = 0; n < numItems - 1; n++)
                str = string.Format("{0},{1}", str, ((int)GameState.State.Locked).ToString());

            return str;
        }

        void CheckNextLockedAquarium(int aquarium)
        {
            if (aquarium == GameState.aquariums.Count - GameState.NUMBER_OF_COMING_SOON_AQUARIUMS) return;

            if (GameState.aquariumsState[aquarium] == (int)GameState.State.Locked)
            {
                GameState.aquariumsState[aquarium] = (int)GameState.State.Unlocked;
                SaveAquariumsState();
                UnlockFirstObjective(aquarium);
                
                GameState.newAquariumIsUnlocked = true;
                GameState.newAquariumUnlocked = aquarium;
                GameState.newAquariumUnlockedName = GameState.aquariums[aquarium].Id;

                Debug.Log(string.Format("New Aquarium <color=orange>{0}</color> unlocked", GameState.aquariums[aquarium].Id));
            }
        }

        private void UnlockNextAquarium(int aquarium)
        {
            int nextAquarium = aquarium + 1;

            if (aquarium < GameState.aquariums.Count - GameState.NUMBER_OF_COMING_SOON_AQUARIUMS - 1)
                GameState.aquariumsState[nextAquarium] = (int)GameState.State.Unlocked;
        }

        private void SaveAquariumsState()
        {
            string str = GameState.aquariumsState[0].ToString();

            for (int n = 1; n < GameState.aquariumsState.Count; n++)
                str = string.Format("{0},{1}", str, GameState.aquariumsState[n].ToString());

            GameState.AquariumsState = str;
        }
        #endregion

        #region PUBLIC METHODS
        public void UnlockFirstObjective(int aquarium)
        {
            GameState.objectivesState[GameState.aquariums[aquarium].Objectives[0].Id] = (int)GameState.State.Unlocked;
            GameState.ObjectivesState = SaveDiccionaryToPlayerPrefs(GameState.objectivesState);
        }

        public void MarkObjectiveAsSolved(int aquarium)
        {
            int numObj = GameState.currentObjective;

            if (GameState.currentObjective == GameState.aquariums[aquarium].Objectives.Count - 1 && GameState.objectivesState[GameState.aquariums[aquarium].Objectives[numObj].Id] == (int)GameState.State.Unlocked)
            {
                GameState.objectivesState[GameState.aquariums[aquarium].Objectives[numObj].Id] = (int)GameState.State.Solved;
                if (CheckIfAllIsUnlocked())
                    GameState.allObjectivesUnlockedInGame = true;
                else
                    GameState.allObjectivesUnlockedInAquarium = true;
            }

            GameState.objectivesState[GameState.aquariums[aquarium].Objectives[numObj].Id] = (int)GameState.State.Solved;
            GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName).State = GameState.State.Solved;

            if (GameState.currentObjective < GameState.aquariums [aquarium].Objectives.Count - 1)
				GameState.objectivesState [GameState.aquariums [aquarium].Objectives [numObj + 1].Id] = (int)GameState.State.Unlocked;

            if (GameState.currentObjective == GameState.aquariums[aquarium].NumberOfSolvedObjectivesToUnlockNextAquarium - 1)
                CheckNextLockedAquarium(aquarium+1);

            GameState.ObjectivesState = SaveDiccionaryToPlayerPrefs(GameState.objectivesState);
        }

        private bool CheckIfAllIsUnlocked()
        {
            for (int a = 0; a < GameState.aquariums.Count - GameState.NUMBER_OF_COMING_SOON_AQUARIUMS; a++)
            {
                for (int o = 0; o < GameState.aquariums[a].Objectives.Count; o++)
                {
                    if (GameState.objectivesState[GameState.aquariums[a].Objectives[o].Id] == (int)GameState.State.Unlocked)
                    {
                        return false;
                    }
                }
            }

            Debug.Log("<color=cyan>All objectives have been solved!!</color>");
            GameState.AllObjectivesSolved = (int)GameState.Toggle.On;
            return true;
        }
        #endregion
    }
}