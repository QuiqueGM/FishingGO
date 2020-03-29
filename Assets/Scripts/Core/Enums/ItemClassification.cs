using UnityEngine;

namespace VFG.Core
{
    public enum TypeOfItem
    {
        Fish,
        MorayEel,
        Seahorse,
        Coral,
        Corals,
        Anemone,
        Anemones,
        Starfish,
        Urchin,
        Echinoderms,
        Mollusc,
        Molluscs,
        Crustacean,
        Crustaceans,
        Rock,
        Rocks,
        Plant,
        Plants,
        NonClassified
    }

    public class ItemClassification : MonoBehaviour
    {
        public TypeOfItem typeOfItem;
    }
}