using UnityEngine;
using TMPro;

namespace VFG.Core.Localization
{
    public class LocalizeText : MonoBehaviour
    {
        protected string key;

        void Awake()
        {
            key = GetComponent<TMP_Text>().text;
            LoadLocalization.Instance.ChangeLanguageEvent += SetLanguageKey;
        }

        public virtual void Start()
        {
            SetLanguageKey();
        }

        void OnDestroy()
        {
            LoadLocalization.Instance.ChangeLanguageEvent -= SetLanguageKey;
        }

        public virtual void SetLanguageKey()
        {
            GetComponent<TMP_Text>().text = LoadLocalization.Instance.GetKey(key);
        }

        public void SetNewInitKey(string newKey)
        {
            key = newKey;
        }
    }
}