using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;
using DG.Tweening;

namespace VFG.Canvas
{
    public class CanvasObjectiveToFind : MonoBehaviour
    {
        private TMP_Text _TXT_ScientificName;
        private Image _IMG_Objective;
        private string key;

		public void ShowObjectToFind()
		{
            Populate();
            gameObject.SetActive(true);

            int DURATION = 6;
			int n = 0;

			DOTween.To(() => n, a => n = a, 1, DURATION).OnComplete(OnComplete);
		}

		private void OnComplete()
		{
			gameObject.SetActive (false);
		}

        public void Initialize()
        {
			_TXT_ScientificName = transform.Find("TXT_ScientificName").GetComponent<TMP_Text>();
			_IMG_Objective = transform.Find("IMG_Objective").GetComponent<Image>();

            ShowObjectToFind();
        }

        public void Populate()
        {
            _TXT_ScientificName.text = GameState.collectables.Find (i => i.Id == GameState.currentObjectiveName).ScientificName;
			_IMG_Objective.sprite = GameState.spritesObjectives.Find (i => i.name == GameState.currentObjectiveName);
        }
    }
}
