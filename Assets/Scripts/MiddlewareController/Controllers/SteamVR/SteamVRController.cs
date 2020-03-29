#if STEAM_VR
using UnityEngine;
using Valve.VR;

namespace VRMiddlewareController
{
    [RequireComponent(typeof(SteamVR_TrackedObject))]
    [RequireComponent(typeof(SteamVREvents))]
    public class SteamVRController : BaseController, IEvents
    {
        private uint controllerIndex;
        public VRControllerState_t controllerState;
        public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)controllerIndex); } }
        public Vector3 velocity { get { return controller.velocity; } }                     // Not used yet
        public Vector3 angularVelocity { get { return controller.angularVelocity; } }       // Not used yet

        public override void Start() 
        {
            GetComponent<SteamVREvents>().middlewareController.VibrationEvent += SetControllerVibration;
            GetComponent<SteamVREvents>().middlewareController.TrackpadModeEvent += SetTrackpadMode;
            GetComponent<SteamVREvents>().middlewareController.ChangeHandEvent += SetHand;

            if (controllerIndex != 0)
            {
                this.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)controllerIndex;
                if (this.GetComponent<SteamVR_RenderModel>() != null)
                {
                    this.GetComponent<SteamVR_RenderModel>().index = (SteamVR_TrackedObject.EIndex)controllerIndex;
                }
            }
            else
                controllerIndex = (uint)this.GetComponent<SteamVR_TrackedObject>().index;

            base.Start();
        }

        public override void OnDestroy()
        {
            GetComponent<SteamVREvents>().middlewareController.VibrationEvent -= SetControllerVibration;
            GetComponent<SteamVREvents>().middlewareController.TrackpadModeEvent -= SetTrackpadMode;
            GetComponent<SteamVREvents>().middlewareController.ChangeHandEvent -= SetHand;

            base.OnDestroy();
        }

        public void Update()
        {
            if (OpenVR.System != null)
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
            if (controller.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu) && !menuButtonIsPressed) {
                OnMenuButtonClicked(senderInfo, eventArguments);
            }

            if (controller.GetPressUp(EVRButtonId.k_EButton_ApplicationMenu) && menuButtonIsPressed) {
                OnMenuButtonUnclicked(senderInfo, eventArguments);
            }
        }

        public void GetTrigger()
        {
            if (controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) && !triggerButtonIsPressed) {
                OnTriggerPressed(senderInfo, eventArguments);
            }

            if (controller.GetPressUp(EVRButtonId.k_EButton_SteamVR_Trigger) && triggerButtonIsPressed) {
                OnTriggerUnpressed(senderInfo, eventArguments);
            }

            if (controller.GetTouch(EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                triggerValue = controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x;
                OnTriggerTouched(senderInfo, ControllerEvents.Instance.EventArguments(handController, triggerValue, triggerValue));
            }
        }

        public void GetGripButton()
        {
            if (controller.GetPressDown(EVRButtonId.k_EButton_Grip) && !gripButtonIsPressed) {
                OnGripButtonClicked(senderInfo, eventArguments);
            }

            if (controller.GetPressUp(EVRButtonId.k_EButton_Grip) && gripButtonIsPressed) {
                OnGripButtonUnclicked(senderInfo, eventArguments);
            }

            if (controller.GetPress(EVRButtonId.k_EButton_Grip)) {
                OnGripButtonHolded(senderInfo, eventArguments);
            }
        }

        public void GetPadTouched()
        {
            if (controller.GetTouchDown(EVRButtonId.k_EButton_SteamVR_Touchpad) && !trackpadIsTouched)
            {
                trackpadValue = controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
                OnPadTouched(senderInfo, ControllerEvents.Instance.EventArguments(handController, trackpadValue.x, trackpadValue.y));
            }

            if (controller.GetTouchUp(EVRButtonId.k_EButton_SteamVR_Touchpad) && trackpadIsTouched)
            {
                trackpadValue = controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
                OnPadUntouched(senderInfo, ControllerEvents.Instance.EventArguments(handController, trackpadValue.x, trackpadValue.y));
            }

            if (controller.GetTouch(EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                trackpadValue = controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
                OnPadTouchedHolded(senderInfo, ControllerEvents.Instance.EventArguments(handController, trackpadValue.x, trackpadValue.y));
            }
        }

        public void GetPadClicked()
        {
            if (controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad) && !trackpadIsPressed)
            {
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadClicked(senderInfo, ControllerEvents.Instance.EventArguments(handController, controller.GetAxis().x, controller.GetAxis().y));
                }
                else
                    GetTrackpadButtonsBehaviour(senderInfo, ControllerEvents.Instance.EventArguments(handController, controller.GetAxis().x, controller.GetAxis().y), ButtonBehavior.Clicked, true);
            }

            if (controller.GetPressUp(EVRButtonId.k_EButton_SteamVR_Touchpad) && trackpadIsPressed)
            {
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadUnclicked(senderInfo, ControllerEvents.Instance.EventArguments(handController, controller.GetAxis().x, controller.GetAxis().y));
                }
                else
                    GetTrackpadButtonsBehaviour(senderInfo, ControllerEvents.Instance.EventArguments(handController, controller.GetAxis().x, controller.GetAxis().y), ButtonBehavior.Unclicked, false);
            }

            if (controller.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                if (trackpadMode == TrackpadMode.OneButton) {
                    OnPadClickedHolded(senderInfo, ControllerEvents.Instance.EventArguments(handController, controller.GetAxis().x, controller.GetAxis().y));
                }
                else
                    GetTrackpadButtonsBehaviour(senderInfo, ControllerEvents.Instance.EventArguments(handController, controller.GetAxis().x, controller.GetAxis().y), ButtonBehavior.Holded, true);
            }
        }

        public void SetControllerVibration(Hand hand, float pulse, float duration)
        {
            if (hand == Hand.Any || hand == handController)
                controller.TriggerHapticPulse((ushort)pulse);
        }
    }
}
#endif