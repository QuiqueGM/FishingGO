#if SAMSUNG_VR
using UnityEngine;

namespace VRMiddlewareController
{
    public class GearVREvents : MonoBehaviour, IRegisterEvents
    {
        [HideInInspector]
        public MiddlewareController middlewareController;
        GearVRController gearVRController;

        public void Register(MiddlewareController mControlller)
        {
            if (GetComponent<GearVRController>() != null)
            {
                gearVRController = GetComponent<GearVRController>();
                middlewareController = mControlller;

                gearVRController.MenuButtonClicked     += middlewareController.MenuButtonClicked;
                gearVRController.MenuButtonUnclicked   += middlewareController.MenuButtonUnclicked;
                gearVRController.TriggerPressed        += middlewareController.TriggerPressed;
                gearVRController.TriggerUnpressed      += middlewareController.TriggerUnpressed;
                gearVRController.TriggerTouched        += middlewareController.TriggerTouched;
                gearVRController.PadTouched            += middlewareController.PadTouched;
                gearVRController.PadUntouched          += middlewareController.PadUntouched;
                gearVRController.PadTouchedHolded      += middlewareController.PadTouchedHolded;
                gearVRController.PadClicked            += middlewareController.PadClicked;
                gearVRController.PadUnclicked          += middlewareController.PadUnclicked;
                gearVRController.PadClickedHolded      += middlewareController.PadHolded;
                gearVRController.PadUpClicked          += middlewareController.PadButtonUpClicked;
                gearVRController.PadUpUnclicked        += middlewareController.PadButtonUpUnclicked;
                gearVRController.PadUpHolded           += middlewareController.PadButtonUpHolded;
                gearVRController.PadDownClicked        += middlewareController.PadButtonDownClicked;
                gearVRController.PadDownUnclicked      += middlewareController.PadButtonDownUnclicked;
                gearVRController.PadDownHolded         += middlewareController.PadButtonDownHolded;
                gearVRController.PadLeftClicked        += middlewareController.PadButtonLeftClicked;
                gearVRController.PadLeftUnclicked      += middlewareController.PadButtonLeftUnclicked;
                gearVRController.PadLeftHolded         += middlewareController.PadButtonLeftHolded;
                gearVRController.PadRightClicked       += middlewareController.PadButtonRightClicked;
                gearVRController.PadRightUnclicked     += middlewareController.PadButtonRightUnclicked;
                gearVRController.PadRightHolded        += middlewareController.PadButtonRightHolded;
            }
        }

        public void Unregister()
        {
            if (gearVRController != null)
            {
                gearVRController.MenuButtonClicked     -= middlewareController.MenuButtonClicked;
                gearVRController.MenuButtonUnclicked   -= middlewareController.MenuButtonUnclicked;
                gearVRController.TriggerPressed        -= middlewareController.TriggerPressed;
                gearVRController.TriggerUnpressed      -= middlewareController.TriggerUnpressed;
                gearVRController.TriggerTouched        -= middlewareController.TriggerTouched;
                gearVRController.PadTouched            -= middlewareController.PadTouched;
                gearVRController.PadUntouched          -= middlewareController.PadUntouched;
                gearVRController.PadTouchedHolded      -= middlewareController.PadTouchedHolded;
                gearVRController.PadClicked            -= middlewareController.PadClicked;
                gearVRController.PadUnclicked          -= middlewareController.PadUnclicked;
                gearVRController.PadClickedHolded      -= middlewareController.PadHolded;
                gearVRController.PadUpUnclicked        -= middlewareController.PadButtonUpUnclicked;
                gearVRController.PadUpHolded           -= middlewareController.PadButtonUpHolded;
                gearVRController.PadDownClicked        -= middlewareController.PadButtonDownClicked;
                gearVRController.PadDownUnclicked      -= middlewareController.PadButtonDownUnclicked;
                gearVRController.PadDownHolded         -= middlewareController.PadButtonDownHolded;
                gearVRController.PadLeftClicked        -= middlewareController.PadButtonLeftClicked;
                gearVRController.PadLeftUnclicked      -= middlewareController.PadButtonLeftUnclicked;
                gearVRController.PadLeftHolded         -= middlewareController.PadButtonLeftHolded;
                gearVRController.PadRightClicked       -= middlewareController.PadButtonRightClicked;
                gearVRController.PadRightUnclicked     -= middlewareController.PadButtonRightUnclicked;
                gearVRController.PadRightHolded        -= middlewareController.PadButtonRightHolded;
            }
        }
    }
}
#endif