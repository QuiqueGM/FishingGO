using System.Collections.Generic;
using UnityEngine;

namespace VFG.Core
{
    public class LoadSprites : MonoBehaviour
    {
        const string PATH_AQUARIUMS = "Sprites/Aquariums";
        const string PATH_OBJECTIVES = "Sprites/Objectives";
        const string PATH_ICONS = "Sprites/Phylum";

        public void Init()
        {
            LoadSpritesList(PATH_AQUARIUMS, GameState.spritesAquariums);
            LoadSpritesList(PATH_OBJECTIVES, GameState.spritesObjectives);
            LoadSpritesList(PATH_ICONS, GameState.spritesPhylum);
        }

        void LoadSpritesList(string path, List<Sprite> list)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(path);

            for (int m = 0; m < sprites.Length; m++)
                list.Add(sprites[m]);

            sprites = null;
        }
    }
}