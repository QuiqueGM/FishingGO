#if SAMSUNG_VR
using UnityEngine;

namespace VRMiddlewareController
{
    [RequireComponent(typeof(OVRGearVrController))]
    [RequireComponent(typeof(GearVREvents))]
    public class GearVRController : BaseController, IEvents
    {
        private OVRInput.Controller controller;

        public override void Start() 
        {
            base.Start();

            GetComponent<GearVREvents>().middlewareController.VibrationEvent += SetControllerVibration;
            GetComponent<GearVREvents>().middlewareController.TrackpadModeEvent += SetTrackpadMode;

            controller = GetComponent<OVRGearVrController>().m_controller;
        }
      
        public void Update()
        {
            GetMenuButton();
            GetTrigger();
            GetGripButton();
            GetPadTouched();
            GetPadClicked();
        }

        public void GetMenuButton()
        {
            if (OVRInput.GetDown(OVRInput.Button.Back) && !menuButtonIsPressed) {
                OnMenuButtonClicked(senderInfo, eventArguments);
            }

            if (OVRInput.GetUp(OVRInput.Button.Back) && menuButtonIsPressed) {
                OnMenuButtonUnclicked(senderInfo, eventArguments);
            }
        }

        //private void GetTrigger()
        //{
        //    if (OVRInput.GetDown(GetButtonControllerIndex(Button.Trigger)))
        //    {
        //        triggerButtonIsPressed = true;
        //        if (TriggerPressed != null) TriggerPressed(senderInfo, eventArguments);
        //    }

        //    if (OVRInput.GetUp(GetButtonControllerIndex(Button.Trigger)))
        //    {
        //        triggerButtonIsPressed = false;
        //        if (TriggerUnpressed != null) TriggerUnpressed(senderInfo, eventArguments);
        //    }

        //    if (OVRInput.Get(GetAxis1DControllerIndex(Button.Trigger)) > 0)
        //    {
        //        triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        //        if (TriggerTouched != null) TriggerTouched(senderInfo, ControllerEvents.Instance.EventArguments((uint)controller, 0, triggerValue, triggerValue));
        //    }
        //}

        public void GetTrigger()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller) && !triggerButtonIsPressed) {
                OnTriggerPressed(senderInfo, eventArguments);
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller) && triggerButtonIsPressed) {
                OnTriggerUnpressed(senderInfo, eventArguments);
            }

            if ((OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller)) > 0)
            {
                triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
                OnTriggerTouched(senderInfo, ControllerEvents.Instance.EventArguments(triggerValue, triggerValue));
            }
        }

        public void GetGripButton() { }

        public void GetPadTouched()
        {
            if (OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad, controller) && !trackpadIsTouched)
            {
                trackpadValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
                OnPadTouched(senderInfo, ControllerEvents.Instance.EventArguments(trackpadValue.x, trackpadValue.y));
            }

            if (OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad, controller) && trackpadIsTouched)
            {
                trackpadValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
                OnPadUntouched(senderInfo, ControllerEvents.Instance.EventArguments(trackpadValue.x, trackpadValue.y));
            }

            if (OVRInput.Get(OVRInput.Touch.PrimaryTouchpad, controller))
            {
                trackpadValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
                OnPadTouchedHolded(senderInfo, ControllerEvents.Instance.EventArguments(trackpadValue.x, trackpadValue.y));
            }
        }

        public void GetPadClicked()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad, controller) && !trackpadIsPressed)
            {
                trackpadValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadClicked(senderInfo, ControllerEvents.Instance.EventArguments(trackpadValue.x, trackpadValue.y));
                }
                else
                    GetTrackpadButtonsBehaviour(OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller), ButtonBehavior.Clicked, true);
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad, controller) && trackpadIsPressed)
            {
                trackpadValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadUnclicked(senderInfo, ControllerEvents.Instance.EventArguments(trackpadValue.x, trackpadValue.y));
                }
                else
                    GetTrackpadButtonsBehaviour(OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller), ButtonBehavior.Unclicked, false);
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad, controller))
            {
                trackpadValue = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadClickedHolded(senderInfo, ControllerEvents.Instance.EventArguments(trackpadValue.x, trackpadValue.y));
                }
                else
                    GetTrackpadButtonsBehaviour(trackpadValue, ButtonBehavior.Holded, true);
            }
        }

        public void SetControllerVibration(Hand hand, float pulse)
        {
            //OVRHaptics
            //controller.TriggerHapticPulse((ushort)pulse);
        }

        public void SetTrackpadMode(Hand hand, TrackpadMode mode)
        {
            OnSetTrackpadMode(mode);
        }
        //private OVRInput.Button GetButtonControllerIndex(Button button)
        //{
        //    switch (button)
        //    {
        //        case Button.Trigger: return controllerIndex == Hand.Left ? OVRInput.Button.PrimaryIndexTrigger : OVRInput.Button.SecondaryIndexTrigger;
        //        default: return 0;
        //    }
        //}

        //private OVRInput.Axis1D GetAxis1DControllerIndex(Button button)
        //{
        //    switch (button)
        //    {
        //        case Button.Trigger: return controllerIndex == Hand.Left ? OVRInput.Axis1D.PrimaryIndexTrigger : OVRInput.Axis1D.SecondaryIndexTrigger;
        //        default: return 0;
        //    }
        //}
    }
}
#endif