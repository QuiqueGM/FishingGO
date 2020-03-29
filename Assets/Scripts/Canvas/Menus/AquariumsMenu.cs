using System.Collections.Generic;
using UnityEngine;
using VFG.Core;
using VFG.Core.Localization;
using VFG.Models;

namespace VFG.Canvas
{
    public class AquariumsMenu : CanvasBase
    {
        private GameObject _aquariums;

        public override void Initialize()
        {
            _aquariums = transform.Find("Aquariums/Buttons").gameObject;
            base.Initialize();
        }

        public override void Populate()
        {
            if (GameState.aquariums.Count != _aquariums.transform.childCount)
            {
                Debug.Log("<color=red>[VFG Error...]</color> Impossible to populate!. The number of aquariums doesn't match with the number of buttons within the hierarchy.");
                return;
            }

            for (int n = 0; n < GameState.aquariums.Count; n++)
            {
                string id = GameState.aquariums[n].Id;

                _aquariums.transform.GetChild(n).GetComponent<PopulateAquariumButtons>().Initialize();
                _aquariums.transform.GetChild(n).GetComponent<PopulateAquariumButtons>().FillButton
                (
                    id,
                    GameState.aquariums[n].IsComingSoon,
                    LoadLocalization.Instance.GetKey(id),
                    GameState.spritesAquariums.Find(it => it.name == id),
                    GameState.aquariumsState[n] == (int)GameState.State.Unlocked ? true : false,
                    GetNumberOfObjectives(n)
                );
            }
        }

        private string GetNumberOfObjectives(int numAquarium)
        {
            int numSolvedObjectives = GetNumberOfSolvedObjectives(numAquarium);
            int numTotalObjectives = GameState.aquariums[numAquarium].Objectives.Count;
            return string.Format("{0}/{1}", numSolvedObjectives, numTotalObjectives);
        }

        private int GetNumberOfSolvedObjectives(int numAquarium)
        {
            int numObjectives = 0;
            List<Objective> objectives = GameState.aquariums[numAquarium].Objectives;

            for (int n = 0; n<objectives.Count; n++)
                if (GameState.objectivesState[objectives[n].Id] == (int)GameState.State.Solved) numObjectives++;

            return numObjectives;
        }
    }
}