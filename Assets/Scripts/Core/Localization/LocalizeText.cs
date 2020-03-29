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

        /// <summary>
        /// Set the default key as initial key to localize
        /// </summary>
        public void SetInitKey()
        {
            GetComponent<TMP_Text>().text = key;
        }

        /// <summary>
        /// Set an alternate key as initial key to localize
        /// </summary>
        /// <param name="newKey">Alternate key</param>
        public void SetInitKey(string newKey)
        {
            GetComponent<TMP_Text>().text = newKey;
        }
    }
}