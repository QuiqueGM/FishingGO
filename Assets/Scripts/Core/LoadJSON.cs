using VFG.Models;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VFG.Core
{
    public class LoadJSON : MonoBehaviour
    {
        const string PATH_OBJECTIVES = "Json/Objectives";
        const string PATH_AQUARIUMS = "Json/Aquariums";
        const string PATH_FISHES = "Json/Fishes";
        const string PATH_CNIDARIANS = "Json/Cnidarians";
        const string PATH_ECHINODERMS = "Json/Echinoderms";
        const string PATH_MOLUSCS = "Json/Molluscs";
        const string PATH_ROCKS = "Json/Rocks";
        const string PATH_PLANTS = "Json/Plants";
		const string PATH_CRUSTACEANS = "Json/Crustaceans";

        private TextAsset objectivesJSON;
        private TextAsset aquariumsJSON;
        private TextAsset fishesJSON;
        private TextAsset cnidariansJSON;
        private TextAsset echinodermsJSON;
        private TextAsset moluscsJSON;
        private TextAsset rocksJSON;
        private TextAsset plantsJSON;
		private TextAsset crustaceansJSON;

        private JSONNode nodoJSON;

        void Awake()
        {
            objectivesJSON = Resources.Load(PATH_OBJECTIVES) as TextAsset;
            aquariumsJSON = Resources.Load(PATH_AQUARIUMS) as TextAsset;
            fishesJSON = Resources.Load(PATH_FISHES) as TextAsset;
            cnidariansJSON = Resources.Load(PATH_CNIDARIANS) as TextAsset;
            echinodermsJSON = Resources.Load(PATH_ECHINODERMS) as TextAsset;
            moluscsJSON = Resources.Load(PATH_MOLUSCS) as TextAsset;
            rocksJSON = Resources.Load(PATH_ROCKS) as TextAsset;
            plantsJSON = Resources.Load(PATH_PLANTS) as TextAsset;
			crustaceansJSON = Resources.Load (PATH_CRUSTACEANS) as TextAsset;
        }

        public void Init()
        {
			LoadCollectable(fishesJSON, cnidariansJSON, echinodermsJSON, moluscsJSON, rocksJSON, plantsJSON, crustaceansJSON);
            LoadObjectives();
            LoadAquariums();
        }

        private void LoadAquariums()
        {
            string strTmp = aquariumsJSON.ToString();
            nodoJSON = JSON.Parse(strTmp);

            for (int n = 0; n < nodoJSON.Count; n++)
                GameState.aquariums.Add(GetAquarium(nodoJSON, n));
        }

        private void LoadObjectives()
        {
            string strTmp = objectivesJSON.ToString();
            nodoJSON = JSON.Parse(strTmp);

            for (int n = 0; n < nodoJSON.Count; n++)
                GameState.objectives.Add(GetObjective(nodoJSON, n));
        }

        private void LoadCollectable(params TextAsset[] textAsset)
        {
            for (int n = 0; n < textAsset.Length; n++)
            {
                string strTmp = textAsset[n].ToString();
                nodoJSON = JSON.Parse(strTmp);

                for (int i = 0; i < nodoJSON.Count; i++)
                    GameState.collectables.Add(GetCollectable(nodoJSON, i));
            }
        }

        private Collectable GetCollectable(JSONNode node, int n)
        {
            Collectable collectable = new Collectable();

            collectable.Id = node[n]["IdItem"].Value;
            collectable.ScientificName = GetScientificName(node, n);
            collectable.TypeOfItem = (TypeOfItem)Enum.Parse(typeof(TypeOfItem), node[n]["TypeOf"].Value);
            collectable.State = GameState.State.Unlocked;
            collectable.Color = node[n]["Color"].Value;
            collectable.Classification = GetClassification(node, n);
            collectable.Features = GetFeatures(node, n);
            collectable.Place = node[n]["Place"].Value;
            collectable.NumCatches = 10000;

            return collectable;
        }

        private string GetScientificName(JSONNode node, int n)
        {
            return string.Format("{0} {1}", node[n]["Genus"].Value, node[n]["Specie"].Value);
        }

        private Classification GetClassification(JSONNode node, int n)
        {
            Classification classification = new Classification();

            classification.Kingdom          = node[n]["Kingdom"].Value;
            classification.Phylum           = node[n]["Phylum"].Value;
            classification.Class            = node[n]["Class"].Value;
            classification.Order            = node[n]["Order"].Value;
            classification.Family           = node[n]["Family"].Value;
            classification.Genus            = node[n]["Genus"].Value;
            classification.Specie           = node[n]["Specie"].Value;
            classification.StageSexVar      = node[n]["StageSexVar"].Value;

            return classification;
        }

        private Features GetFeatures(JSONNode node, int n)
        {
            Features features = new Features();

            features.CommonSize            = node[n]["CommonSize"].AsFloat;
            features.MaxSize               = node[n]["MaxSize"].AsFloat;
            features.MinDepth              = node[n]["MinDepth"].AsInt;
            features.MaxDepth              = node[n]["MaxDepth"].AsInt;

            return features;
        }

        private Objective GetObjective(JSONNode node, int n)
        {
            Objective objective = new Objective();

			objective.Id                  	= node[n]["IdObjective"].Value;
            objective.TypeOfItem            = (TypeOfItem)Enum.Parse(typeof(TypeOfItem), node[n]["TypeOf"].Value);
            objective.Position              = new Vector3
                                                (
                                                    node[n]["Transform"][0]["Position"][0]["x"].AsFloat, 
                                                    node[n]["Transform"][0]["Position"][0]["y"].AsFloat, 
                                                    node[n]["Transform"][0]["Position"][0]["z"].AsFloat
                                                );

            objective.Rotation              = new Quaternion
                                                (
                                                    node[n]["Transform"][0]["Rotation"][0]["x"].AsFloat,
                                                    node[n]["Transform"][0]["Rotation"][0]["y"].AsFloat,
                                                    node[n]["Transform"][0]["Rotation"][0]["z"].AsFloat,
                                                    node[n]["Transform"][0]["Rotation"][0]["w"].AsFloat
                                                );

            objective.Scale              = new Vector3
                                                (
                                                    node[n]["Transform"][0]["Scale"][0]["x"].AsFloat, 
                                                    node[n]["Transform"][0]["Scale"][0]["y"].AsFloat, 
                                                    node[n]["Transform"][0]["Scale"][0]["z"].AsFloat
                                                );

            return objective;
        }

        private Aquarium GetAquarium(JSONNode node, int n)
        {
            Aquarium aquarium = new Aquarium();

            aquarium.Id                                             = node[n]["idAquarium"].Value;
            aquarium.NumberOfSolvedObjectivesToUnlockNextAquarium   = node[n]["numberOfSolvedObjectivesToUnlockNextAquarium"].AsInt;
            aquarium.Objectives                                     = new List<Objective>();

            int numItems = node[n]["objectives"].Count;

            for (int i = 0; i < numItems; i++)
            {
                Objective objective = new Objective();
                objective = GameState.objectives.Find(it => it.Id == node[n]["objectives"][i]["idObjective"].Value);
                aquarium.Objectives.Add(objective);
            }

            aquarium.IsComingSoon = aquarium.Objectives.Count == 0 ? true : false;

            return aquarium;
        }
    }
}