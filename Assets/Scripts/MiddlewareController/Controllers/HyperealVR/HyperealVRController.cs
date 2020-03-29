#if HYPEREAL_VR
using UnityEngine;
using Hypereal;
using System.Collections;

namespace VRMiddlewareController
{
    [RequireComponent(typeof(HyTrackObj))]
    [RequireComponent(typeof(HyperealVREvents))]
    public class HyperealVRController : BaseController, IEvents
    {
        private uint controllerIndex;
        public override void Start() 
        {
            base.Start();
            GetComponent<HyperealVREvents>().middlewareController.VibrationEvent += SetControllerVibration;
            GetComponent<HyperealVREvents>().middlewareController.TrackpadModeEvent += SetTrackpadMode;
            controllerIndex = (uint)gameObject.GetComponent<HyTrackObj>().device;
        }

        public void Update()
        {
            if (HyperealVR.Instance != null)
            {
                GetMenuButton();
                GetTrigger();
                GetGripButton();
                GetPadTouched();
                GetPadClicked();
            }
        }
        
        public void GetMenuButton()
        {
            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressDown(HyInputKey.Menu) && !menuButtonIsPressed) {
                OnMenuButtonClicked(senderInfo, eventArguments);
            }

            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressUp(HyInputKey.Menu) && menuButtonIsPressed) {
                OnMenuButtonUnclicked(senderInfo, eventArguments);
            }
        }
        
        public void GetTrigger()
        {
            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressDown(HyInputKey.IndexTrigger) && !triggerButtonIsPressed) {
                OnTriggerPressed(senderInfo, eventArguments);
            }

            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressUp(HyInputKey.IndexTrigger) && triggerButtonIsPressed) {
                OnTriggerUnpressed(senderInfo, eventArguments);
            }
            
            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouch(HyInputKey.IndexTrigger) &&
                HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTriggerAxis(HyInputKey.IndexTrigger) > 0) 
            {
                triggerValue = HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTriggerAxis(HyInputKey.IndexTrigger);
                OnTriggerTouched(senderInfo, ControllerEvents.Instance.EventArguments(handController, triggerValue, triggerValue));
            }
        }
        
        public void GetGripButton()
        {
            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressDown(HyInputKey.SideTrigger) && !gripButtonIsPressed) {
                OnGripButtonClicked(senderInfo, eventArguments);
            }

            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressUp(HyInputKey.SideTrigger) && gripButtonIsPressed) {
                OnGripButtonUnclicked(senderInfo, eventArguments);
            }
            
            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouch(HyInputKey.SideTrigger)) {
                OnGripButtonHolded(senderInfo, eventArguments);
            }
        }

        public void GetPadTouched() { }

        public void GetPadClicked()
        {
            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressDown(HyInputKey.Touchpad) && !trackpadIsPressed)
            {
                trackpadValue = HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouchpadAxis();
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadClicked(senderInfo, ControllerEvents.Instance.EventArguments(handController, trackpadValue.x, trackpadValue.y));
                }
                else
                    GetTrackpadButtonsBehaviour(HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouchpadAxis(), ButtonBehavior.Clicked, true);
            }

            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPressUp(HyInputKey.Touchpad) && trackpadIsPressed)
            {
                trackpadValue = HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouchpadAxis();
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadUnclicked(senderInfo, ControllerEvents.Instance.EventArguments(handController, trackpadValue.x, trackpadValue.y));
                }
                else
                    GetTrackpadButtonsBehaviour(HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouchpadAxis(), ButtonBehavior.Unclicked, false);
            }

            if (HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetPress(HyInputKey.Touchpad))
            {
                trackpadValue = HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouchpadAxis();
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadClickedHolded(senderInfo, ControllerEvents.Instance.EventArguments(handController, trackpadValue.x, trackpadValue.y));
                }
                else
                    GetTrackpadButtonsBehaviour(HyperealVR.GetInputDevice((HyDevice)controllerIndex).GetTouchpadAxis(), ButtonBehavior.Holded, true);
            }
        }

        public void SetControllerVibration(Hand hand, float pulse, float duration = 0)
        {
            if (hand == handController)
                HyperealVR.Instance.TriggerHapticPulse((HyDevice)controllerIndex, 1, pulse, duration, 1);
        }

        public void SetTrackpadMode(Hand hand, TrackpadMode mode)
        {
            OnSetTrackpadMode(hand, mode);
        }

        public void SetHand(Hand hand)
        {
            OnSetHand(hand);
        }
    }
}
#endif