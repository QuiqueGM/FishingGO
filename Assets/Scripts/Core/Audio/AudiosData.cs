using System.Collections.Generic;
using UnityEngine;

namespace VFG.Core.Audio
{
    public class AudiosData : MonoBehaviour
    {
        public List<AudioClip> audio;

        public const string WHEEL_SELECTOR = "WheelSelector";
        public const string MUSIC_MENUS = "Ambient";
        public const string MUSIC_INGAME = "Underwater";
        public const string BUTTON_DOWN = "ButtonDown";
        public const string BUTTON_UP = "ButtonUp";
        public const string WRONG_OBJECTIVE = "WrongObjective";
        public const string TAKE_PICTURE = "TakePicture";
        public const string ALL_OBJECTIVES = "AllObjectives";
        public const string NEW_AQUARIUM = "NewAquarium";
    }
}