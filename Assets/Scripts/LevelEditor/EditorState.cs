using System.Collections.Generic;
using UnityEngine;
using VFG.Core;

namespace VFG.LevelEditor
{
    public class EditorState
    {
        public struct GameObjects
        {
            public GameObject prefab;
            public Vector3 pos;
            public Quaternion rot;
            public Vector3 scl;
        }

        public enum ActiveHand
        {
            Right = 0,
            Left = 1,
            Both = 2,
            None = 3
        }

        public enum TypeOfAction
        {
            Select,
            Scale,
            Duplicate,
            Delete,
            IncreaseSize,
            DecreaseSize,
            Save
        }

        public static ActiveHand activeHand = ActiveHand.None;
        public static TypeOfAction currentAction = TypeOfAction.Select;
        public static string groupOfItemsToShow;
        public static Dictionary<GameObject, Material> itemSelected = new Dictionary<GameObject, Material>();
        public static Dictionary<GameObject, Material> currentItems = new Dictionary<GameObject, Material>();

		public static string GetFolderFromTypeOfItem(TypeOfItem typeOfItem)
		{
			switch (typeOfItem)
			{
                case TypeOfItem.Fish: return GameState.FISHES;
                case TypeOfItem.Anemones:
			    case TypeOfItem.Anemone: return GameState.ANEMONES;
                case TypeOfItem.Corals:
			    case TypeOfItem.Coral: return GameState.CORALS;
                case TypeOfItem.Molluscs:
			    case TypeOfItem.Mollusc: return GameState.MOLLUSCS;
                case TypeOfItem.Plants:
                case TypeOfItem.Plant: return GameState.PLANTS;
                case TypeOfItem.Rocks:
			    case TypeOfItem.Rock: return GameState.ROCKS;
                case TypeOfItem.Echinoderms:
			    case TypeOfItem.Starfish:
			    case TypeOfItem.Urchin: return GameState.ECHINODERMS;
                case TypeOfItem.Crustaceans:
                case TypeOfItem.Crustacean: return GameState.CRUSTACEANS;
                case TypeOfItem.NonClassified: return GameState.NON_CLASSIFIED;
                default: return null;
			}
		}
    }
}