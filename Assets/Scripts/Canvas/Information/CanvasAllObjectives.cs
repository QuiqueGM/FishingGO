using UnityEngine;

namespace VFG.Canvas
{
    public class CanvasAllObjectives : CanvasBase
    {
        public GameObject buttonAquarium;
        public GameObject buttonContinue;

        public void SetButtons(bool isInMenu)
        {
            buttonAquarium.SetActive(isInMenu);
            buttonContinue.SetActive(!isInMenu);
        }
    }
}
