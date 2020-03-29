using TMPro;
using System.Collections.Generic;

namespace VFG.Core.Localization
{
    public class LocalizeTextWithParameters : LocalizeText
    {
        public List<string> parameters;

        public override void Start() { }

        public override void SetLanguageKey()
        {
            key = GetComponent<TMP_Text>().text;

            if (parameters.Count > 0)
                GetComponent<TMP_Text>().text = string.Format(LoadLocalization.Instance.GetKey(key), parameters.ToArray());
        }

        public void AddParameter(int param)
        {
            ClearParameters();
            parameters.Add(param.ToString());
            SetLanguageKey();
        }

        public void AddParameter(float param)
        {
            ClearParameters();
            parameters.Add(param.ToString());
            SetLanguageKey();
        }

        public void AddParameter(string param)
        {
            ClearParameters();
            parameters.Add(param);
            SetLanguageKey();
        }

        public void AddParameters(List<string> parameters)
        {
            ClearParameters();
            this.parameters.AddRange(parameters);
            SetLanguageKey();
        }

        public void ClearParameters()
        {
            parameters.Clear();
        }
    }
}