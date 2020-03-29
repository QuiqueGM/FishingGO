#if STEAM_VR
using UnityEngine;

namespace VRMiddlewareController
{
    public class SteamVREvents : MonoBehaviour, IRegisterEvents
    {
        [HideInInspector]
        public MiddlewareController middlewareController;
        SteamVRController steamVRController;

        public void Register(MiddlewareController mControlller)
        {
            if (GetComponent<SteamVRController>() != null)
            {
                steamVRController = GetComponent<SteamVRController>();
                middlewareController = mControlller;

                steamVRController.MenuButtonClicked     += middlewareController.MenuButtonClicked;
                steamVRController.MenuButtonUnclicked   += middlewareController.MenuButtonUnclicked;
                steamVRController.TriggerPressed        += middlewareController.TriggerPressed;
                steamVRController.TriggerUnpressed      += middlewareController.TriggerUnpressed;
                steamVRController.TriggerTouched        += middlewareController.TriggerTouched;
                steamVRController.GripButtonClicked     += middlewareController.GripButtonClicked;
                steamVRController.GripButtonUnclicked   += middlewareController.GripButtonUnclicked;
                steamVRController.GripButtonHolded      += middlewareController.GripButtonHolded;
                steamVRController.PadTouched            += middlewareController.PadTouched;
                steamVRController.PadUntouched          += middlewareController.PadUntouched;
                steamVRController.PadTouchedHolded      += middlewareController.PadTouchedHolded;
                steamVRController.PadClicked            += middlewareController.PadClicked;
                steamVRController.PadUnclicked          += middlewareController.PadUnclicked;
                steamVRController.PadClickedHolded      += middlewareController.PadHolded;
                steamVRController.PadUpClicked          += middlewareController.PadButtonUpClicked;
                steamVRController.PadUpUnclicked        += middlewareController.PadButtonUpUnclicked;
                steamVRController.PadUpHolded           += middlewareController.PadButtonUpHolded;
                steamVRController.PadDownClicked        += middlewareController.PadButtonDownClicked;
                steamVRController.PadDownUnclicked      += middlewareController.PadButtonDownUnclicked;
                steamVRController.PadDownHolded         += middlewareController.PadButtonDownHolded;
                steamVRController.PadLeftClicked        += middlewareController.PadButtonLeftClicked;
                steamVRController.PadLeftUnclicked      += middlewareController.PadButtonLeftUnclicked;
                steamVRController.PadLeftHolded         += middlewareController.PadButtonLeftHolded;
                steamVRController.PadRightClicked       += middlewareController.PadButtonRightClicked;
                steamVRController.PadRightUnclicked     += middlewareController.PadButtonRightUnclicked;
                steamVRController.PadRightHolded        += middlewareController.PadButtonRightHolded;

                steamVRController.LMenuButtonClicked    += middlewareController.LMenuButtonClicked;
                steamVRController.LMenuButtonUnclicked  += middlewareController.LMenuButtonUnclicked;
                steamVRController.LTriggerPressed       += middlewareController.LTriggerPressed;
                steamVRController.LTriggerUnpressed     += middlewareController.LTriggerUnpressed;
                steamVRController.LTriggerTouched       += middlewareController.LTriggerTouched;
                steamVRController.LGripButtonClicked    += middlewareController.LGripButtonClicked;
                steamVRController.LGripButtonUnclicked  += middlewareController.LGripButtonUnclicked;
                steamVRController.LGripButtonHolded     += middlewareController.LGripButtonHolded;
                steamVRController.LPadTouched           += middlewareController.LPadTouched;
                steamVRController.LPadUntouched         += middlewareController.LPadUntouched;
                steamVRController.LPadTouchedHolded     += middlewareController.LPadTouchedHolded;
                steamVRController.LPadClicked           += middlewareController.LPadClicked;
                steamVRController.LPadUnclicked         += middlewareController.LPadUnclicked;
                steamVRController.LPadClickedHolded     += middlewareController.LPadHolded;
                steamVRController.LPadUpClicked         += middlewareController.LPadButtonUpClicked;
                steamVRController.LPadUpUnclicked       += middlewareController.LPadButtonUpUnclicked;
                steamVRController.LPadUpHolded          += middlewareController.LPadButtonUpHolded;
                steamVRController.LPadDownClicked       += middlewareController.LPadButtonDownClicked;
                steamVRController.LPadDownUnclicked     += middlewareController.LPadButtonDownUnclicked;
                steamVRController.LPadDownHolded        += middlewareController.LPadButtonDownHolded;
                steamVRController.LPadLeftClicked       += middlewareController.LPadButtonLeftClicked;
                steamVRController.LPadLeftUnclicked     += middlewareController.LPadButtonLeftUnclicked;
                steamVRController.LPadLeftHolded        += middlewareController.LPadButtonLeftHolded;
                steamVRController.LPadRightClicked      += middlewareController.LPadButtonRightClicked;
                steamVRController.LPadRightUnclicked    += middlewareController.LPadButtonRightUnclicked;
                steamVRController.LPadRightHolded       += middlewareController.LPadButtonRightHolded;

                steamVRController.RMenuButtonClicked    += middlewareController.RMenuButtonClicked;
                steamVRController.RMenuButtonUnclicked  += middlewareController.RMenuButtonUnclicked;
                steamVRController.RTriggerPressed       += middlewareController.RTriggerPressed;
                steamVRController.RTriggerUnpressed     += middlewareController.RTriggerUnpressed;
                steamVRController.RTriggerTouched       += middlewareController.RTriggerTouched;
                steamVRController.RGripButtonClicked    += middlewareController.RGripButtonClicked;
                steamVRController.RGripButtonUnclicked  += middlewareController.RGripButtonUnclicked;
                steamVRController.RGripButtonHolded     += middlewareController.RGripButtonHolded;
                steamVRController.RPadTouched           += middlewareController.RPadTouched;
                steamVRController.RPadUntouched         += middlewareController.RPadUntouched;
                steamVRController.RPadTouchedHolded     += middlewareController.RPadTouchedHolded;
                steamVRController.RPadClicked           += middlewareController.RPadClicked;
                steamVRController.RPadUnclicked         += middlewareController.RPadUnclicked;
                steamVRController.RPadClickedHolded     += middlewareController.RPadHolded;
                steamVRController.RPadUpClicked         += middlewareController.RPadButtonUpClicked;
                steamVRController.RPadUpUnclicked       += middlewareController.RPadButtonUpUnclicked;
                steamVRController.RPadUpHolded          += middlewareController.RPadButtonUpHolded;
                steamVRController.RPadDownClicked       += middlewareController.RPadButtonDownClicked;
                steamVRController.RPadDownUnclicked     += middlewareController.RPadButtonDownUnclicked;
                steamVRController.RPadDownHolded        += middlewareController.RPadButtonDownHolded;
                steamVRController.RPadLeftClicked       += middlewareController.RPadButtonLeftClicked;
                steamVRController.RPadLeftUnclicked     += middlewareController.RPadButtonLeftUnclicked;
                steamVRController.RPadLeftHolded        += middlewareController.RPadButtonLeftHolded;
                steamVRController.RPadRightClicked      += middlewareController.RPadButtonRightClicked;
                steamVRController.RPadRightUnclicked    += middlewareController.RPadButtonRightUnclicked;
                steamVRController.RPadRightHolded       += middlewareController.RPadButtonRightHolded;
            }
        }

        public void Unregister()
        {
            if (steamVRController != null)
            {
                steamVRController.MenuButtonClicked     -= middlewareController.MenuButtonClicked;
                steamVRController.MenuButtonUnclicked   -= middlewareController.MenuButtonUnclicked;
                steamVRController.TriggerPressed        -= middlewareController.TriggerPressed;
                steamVRController.TriggerUnpressed      -= middlewareController.TriggerUnpressed;
                steamVRController.TriggerTouched        -= middlewareController.TriggerTouched;
                steamVRController.GripButtonClicked     -= middlewareController.GripButtonClicked;
                steamVRController.GripButtonUnclicked   -= middlewareController.GripButtonUnclicked;
                steamVRController.GripButtonHolded      -= middlewareController.GripButtonHolded;
                steamVRController.PadTouched            -= middlewareController.PadTouched;
                steamVRController.PadUntouched          -= middlewareController.PadUntouched;
                steamVRController.PadTouchedHolded      -= middlewareController.PadTouchedHolded;
                steamVRController.PadClicked            -= middlewareController.PadClicked;
                steamVRController.PadUnclicked          -= middlewareController.PadUnclicked;
                steamVRController.PadClickedHolded      -= middlewareController.PadHolded;
                steamVRController.PadUpUnclicked        -= middlewareController.PadButtonUpUnclicked;
                steamVRController.PadUpHolded           -= middlewareController.PadButtonUpHolded;
                steamVRController.PadDownClicked        -= middlewareController.PadButtonDownClicked;
                steamVRController.PadDownUnclicked      -= middlewareController.PadButtonDownUnclicked;
                steamVRController.PadDownHolded         -= middlewareController.PadButtonDownHolded;
                steamVRController.PadLeftClicked        -= middlewareController.PadButtonLeftClicked;
                steamVRController.PadLeftUnclicked      -= middlewareController.PadButtonLeftUnclicked;
                steamVRController.PadLeftHolded         -= middlewareController.PadButtonLeftHolded;
                steamVRController.PadRightClicked       -= middlewareController.PadButtonRightClicked;
                steamVRController.PadRightUnclicked     -= middlewareController.PadButtonRightUnclicked;
                steamVRController.PadRightHolded        -= middlewareController.PadButtonRightHolded;

                steamVRController.LMenuButtonClicked    -= middlewareController.LMenuButtonClicked;
                steamVRController.LMenuButtonUnclicked  -= middlewareController.LMenuButtonUnclicked;
                steamVRController.LTriggerPressed       -= middlewareController.LTriggerPressed;
                steamVRController.LTriggerUnpressed     -= middlewareController.LTriggerUnpressed;
                steamVRController.LTriggerTouched       -= middlewareController.LTriggerTouched;
                steamVRController.LGripButtonClicked    -= middlewareController.LGripButtonClicked;
                steamVRController.LGripButtonUnclicked  -= middlewareController.LGripButtonUnclicked;
                steamVRController.LGripButtonHolded     -= middlewareController.LGripButtonHolded;
                steamVRController.LPadTouched           -= middlewareController.LPadTouched;
                steamVRController.LPadUntouched         -= middlewareController.LPadUntouched;
                steamVRController.LPadTouchedHolded     -= middlewareController.LPadTouchedHolded;
                steamVRController.LPadClicked           -= middlewareController.LPadClicked;
                steamVRController.LPadUnclicked         -= middlewareController.LPadUnclicked;
                steamVRController.LPadClickedHolded     -= middlewareController.LPadHolded;
                steamVRController.LPadUpClicked         -= middlewareController.LPadButtonUpClicked;
                steamVRController.LPadUpUnclicked       -= middlewareController.LPadButtonUpUnclicked;
                steamVRController.LPadUpHolded          -= middlewareController.LPadButtonUpHolded;
                steamVRController.LPadDownClicked       -= middlewareController.LPadButtonDownClicked;
                steamVRController.LPadDownUnclicked     -= middlewareController.LPadButtonDownUnclicked;
                steamVRController.LPadDownHolded        -= middlewareController.LPadButtonDownHolded;
                steamVRController.LPadLeftClicked       -= middlewareController.LPadButtonLeftClicked;
                steamVRController.LPadLeftUnclicked     -= middlewareController.LPadButtonLeftUnclicked;
                steamVRController.LPadLeftHolded        -= middlewareController.LPadButtonLeftHolded;
                steamVRController.LPadRightClicked      -= middlewareController.LPadButtonRightClicked;
                steamVRController.LPadRightUnclicked    -= middlewareController.LPadButtonRightUnclicked;
                steamVRController.LPadRightHolded       -= middlewareController.LPadButtonRightHolded;

                steamVRController.RMenuButtonClicked    -= middlewareController.RMenuButtonClicked;
                steamVRController.RMenuButtonUnclicked  -= middlewareController.RMenuButtonUnclicked;
                steamVRController.RTriggerPressed       -= middlewareController.RTriggerPressed;
                steamVRController.RTriggerUnpressed     -= middlewareController.RTriggerUnpressed;
                steamVRController.RTriggerTouched       -= middlewareController.RTriggerTouched;
                steamVRController.RGripButtonClicked    -= middlewareController.RGripButtonClicked;
                steamVRController.RGripButtonUnclicked  -= middlewareController.RGripButtonUnclicked;
                steamVRController.RGripButtonHolded     -= middlewareController.RGripButtonHolded;
                steamVRController.RPadTouched           -= middlewareController.RPadTouched;
                steamVRController.RPadUntouched         -= middlewareController.RPadUntouched;
                steamVRController.RPadTouchedHolded     -= middlewareController.RPadTouchedHolded;
                steamVRController.RPadClicked           -= middlewareController.RPadClicked;
                steamVRController.RPadUnclicked         -= middlewareController.RPadUnclicked;
                steamVRController.RPadClickedHolded     -= middlewareController.RPadHolded;
                steamVRController.RPadUpClicked         -= middlewareController.RPadButtonUpClicked;
                steamVRController.RPadUpUnclicked       -= middlewareController.RPadButtonUpUnclicked;
                steamVRController.RPadUpHolded          -= middlewareController.RPadButtonUpHolded;
                steamVRController.RPadDownClicked       -= middlewareController.RPadButtonDownClicked;
                steamVRController.RPadDownUnclicked     -= middlewareController.RPadButtonDownUnclicked;
                steamVRController.RPadDownHolded        -= middlewareController.RPadButtonDownHolded;
                steamVRController.RPadLeftClicked       -= middlewareController.RPadButtonLeftClicked;
                steamVRController.RPadLeftUnclicked     -= middlewareController.RPadButtonLeftUnclicked;
                steamVRController.RPadLeftHolded        -= middlewareController.RPadButtonLeftHolded;
                steamVRController.RPadRightClicked      -= middlewareController.RPadButtonRightClicked;
                steamVRController.RPadRightUnclicked    -= middlewareController.RPadButtonRightUnclicked;
                steamVRController.RPadRightHolded       -= middlewareController.RPadButtonRightHolded;
            }
        }
    }
}
#endif