using TMPro;
using UnityEngine;
using VFG.Core.Localization;

namespace VFG.Canvas.LevelEditor
{
    public class WarningDialog : BaseDialog
    {
        public LocalizeTextWithParameters textWithParameters;

        public override void Init()
        {
            SetActive(false);
        }

        public void Populate(string lvl)
        {
            SetActive(true);
            textWithParameters.AddParameter(lvl);
        }

        public void ResetKey()
        {
            textWithParameters.SetInitKey();
        }
    }
}