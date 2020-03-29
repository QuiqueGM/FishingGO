using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VFG.Core;
using VFG.Utils;

namespace VFG.Canvas
{
    public class CanvasComparePictures : CanvasBase
    {
        private TMP_Text _txtObjective;
        private RawImage _currentPicture;
        private RawImage _newPicture;

        public void Init()
        {
            _txtObjective = transform.Find("TXT_Objective").GetComponent<TMP_Text>();
            _currentPicture = transform.Find("RAW_CurrentPicture").GetComponent<RawImage>();
            _newPicture = transform.Find("RAW_NewPicture").GetComponent<RawImage>();

			_txtObjective.text = GameState.collectables.Find (i => i.Id == GameState.currentObjectiveName).ScientificName;
            try
            {
                _currentPicture.texture = GetPictureFromData(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, GameState.currentObjectiveName + GameState.EXTENSION)));
            }
            catch
            {
                Debug.LogError(string.Format("The current picture for this objective <color=red>{0}</color> it doesn't exist.", GameState.currentObjectiveName));
            }
            _newPicture.texture = GetPictureFromData(File.ReadAllBytes(Path.Combine(Application.persistentDataPath, GameState.currentObjectiveName + GameState.TEMPORARY + GameState.EXTENSION)));

            ShowPopUp(gameObject);
        }

        private Texture2D GetPictureFromData(byte[] bytesP)
        {
            Texture2D picture = new Texture2D(GameState.resWidth, GameState.resHeight);
            byte[] bytes = bytesP;
            picture.LoadImage(bytes);

            return picture;
        }
    }
}