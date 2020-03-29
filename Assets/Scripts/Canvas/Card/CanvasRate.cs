using VFG.Core.Localization;
using UnityEngine;
using VFG.Core;
using VFG.Managers;

namespace VFG.Canvas
{
    public class CanvasRate : CanvasBase
    {
        private CanvasManager CM;

        public override void Populate()
        {
            CM = gameObject.transform.parent.GetComponent<CanvasManager>();
            gameObject.transform.Find("TXT_Enjoying").GetComponent<LocalizeTextWithParameters>().SetLanguageKey();
        }

        public void Rate()
        {
            GameState.GameHasBeenRated = (int)GameState.Toggle.On;
            Application.OpenURL("https://itunes.apple.com/app/fishinggo/id1262044370?mt=8");
        }

        public void CloseCanvas()
        {
            if (CM.HasToShowObjectiveFind)
            {
                CM.HasToShowObjectiveFind = false;
                CM.CNV_ObjectiveTofind.GetComponent<CanvasObjectiveToFind>().ShowObjectToFind();
            }

            gameObject.SetActive(false);
        }
    }
}
