using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using System.IO;

namespace VFG.Utils
{
    public class LoadRawImage : MonoBehaviour
    {
        public string fileName = string.Empty;
        public RawImage rawImage;

        public void Awake()
        {
            rawImage = transform.Find("RawImage").GetComponent<RawImage>();
            Populate();
        }

        public void Populate()
        {
            Texture2D picture;
            picture = new Texture2D(GameState.resWidth, GameState.resHeight);
            byte[] bytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, fileName + GameState.EXTENSION));
            Debug.Log(bytes);
            picture.LoadImage(bytes);

            rawImage.texture = picture;
        }
    }
}
