using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VFG.Core;

namespace VFG.Canvas
{
	public class CanvasCardBackground : CanvasBase
    {
        private Image _image;

        public override void Initialize()
        {
            _image = GetComponent<Image>();
            base.Initialize();
        }

        public override void Populate()
        {
            _image.color = GameState.GetColorFromObjective(GameState.collectables.Find(i => i.Id == GameState.currentObjectiveName).Color, GameState.State.Solved);
            gameObject.SetActive(true);
            Show();
        }
    }
}
