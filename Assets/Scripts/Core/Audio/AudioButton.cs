using UnityEngine;
using UnityEngine.EventSystems;

namespace VFG.Core.Audio
{
    public class AudioButton : MonoBehaviour
    {
        public OverrideAudioButton overrideButtonDown;
        public OverrideAudioButton overrideButtonUp;

        private void Awake()
        {
            var pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            pointerDown.callback.AddListener((e) => OnButtonDown());

            var pointerUp = new EventTrigger.Entry();
            pointerUp.eventID = EventTriggerType.PointerUp;
            pointerUp.callback.AddListener((e) => OnButtonUp());

            EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
            trigger.triggers.Add(pointerDown);
            trigger.triggers.Add(pointerUp);
        }

        public void OnButtonDown()
        {
            if (overrideButtonDown.isOverride)
                AudioManager.Instance.PlayEffect(overrideButtonDown.clip);
            else
                AudioManager.Instance.PlayEffect(AudiosData.BUTTON_DOWN);
        }

        public void OnButtonUp()
        {
            if (overrideButtonUp.isOverride)
                AudioManager.Instance.PlayEffect(overrideButtonUp.clip);
            else
                AudioManager.Instance.PlayEffect(AudiosData.BUTTON_UP, 0.7f, 0.8f);
        }
    }
}