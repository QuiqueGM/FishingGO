#if HYPEREAL_VR
using UnityEngine;

namespace VRMiddlewareController
{
    public class HyperealVREvents : MonoBehaviour, IRegisterEvents
    {
        [HideInInspector]
        public MiddlewareController middlewareController;
        HyperealVRController hyperealVRController;

        public void Register(MiddlewareController mControlller)
        {
            if (GetComponent<HyperealVRController>() != null)
            {
                hyperealVRController = GetComponent<HyperealVRController>();
                middlewareController = mControlller;

                hyperealVRController.MenuButtonClicked      += middlewareController.MenuButtonClicked;
                hyperealVRController.MenuButtonUnclicked    += middlewareController.MenuButtonUnclicked;
                hyperealVRController.TriggerPressed         += middlewareController.TriggerPressed;
                hyperealVRController.TriggerUnpressed       += middlewareController.TriggerUnpressed;
                hyperealVRController.TriggerTouched         += middlewareController.TriggerTouched;
                hyperealVRController.GripButtonClicked      += middlewareController.GripButtonClicked;
                hyperealVRController.GripButtonUnclicked    += middlewareController.GripButtonUnclicked;
                hyperealVRController.PadClicked             += middlewareController.PadClicked;
                hyperealVRController.PadUnclicked           += middlewareController.PadUnclicked;
                hyperealVRController.PadClickedHolded       += middlewareController.PadHolded;
                hyperealVRController.PadUpClicked           += middlewareController.PadButtonUpClicked;
                hyperealVRController.PadUpUnclicked         += middlewareController.PadButtonUpUnclicked;
                hyperealVRController.PadUpHolded            += middlewareController.PadButtonUpHolded;
                hyperealVRController.PadDownClicked         += middlewareController.PadButtonDownClicked;
                hyperealVRController.PadDownUnclicked       += middlewareController.PadButtonDownUnclicked;
                hyperealVRController.PadDownHolded          += middlewareController.PadButtonDownHolded;
                hyperealVRController.PadLeftClicked         += middlewareController.PadButtonLeftClicked;
                hyperealVRController.PadLeftUnclicked       += middlewareController.PadButtonLeftUnclicked;
                hyperealVRController.PadLeftHolded          += middlewareController.PadButtonLeftHolded;
                hyperealVRController.PadRightClicked        += middlewareController.PadButtonRightClicked;
                hyperealVRController.PadRightUnclicked      += middlewareController.PadButtonRightUnclicked;
                hyperealVRController.PadRightHolded         += middlewareController.PadButtonRightHolded;

                hyperealVRController.LMenuButtonClicked     += middlewareController.LMenuButtonClicked;
                hyperealVRController.LMenuButtonUnclicked   += middlewareController.LMenuButtonUnclicked;
                hyperealVRController.LTriggerPressed        += middlewareController.LTriggerPressed;
                hyperealVRController.LTriggerUnpressed      += middlewareController.LTriggerUnpressed;
                hyperealVRController.LTriggerTouched        += middlewareController.LTriggerTouched;
                hyperealVRController.LGripButtonClicked     += middlewareController.LGripButtonClicked;
                hyperealVRController.LGripButtonUnclicked   += middlewareController.LGripButtonUnclicked;
                hyperealVRController.LPadClicked            += middlewareController.LPadClicked;
                hyperealVRController.LPadUnclicked          += middlewareController.LPadUnclicked;
                hyperealVRController.LPadClickedHolded      += middlewareController.LPadHolded;
                hyperealVRController.LPadUpClicked          += middlewareController.LPadButtonUpClicked;
                hyperealVRController.LPadUpUnclicked        += middlewareController.LPadButtonUpUnclicked;
                hyperealVRController.LPadUpHolded           += middlewareController.LPadButtonUpHolded;
                hyperealVRController.LPadDownClicked        += middlewareController.LPadButtonDownClicked;
                hyperealVRController.LPadDownUnclicked      += middlewareController.LPadButtonDownUnclicked;
                hyperealVRController.LPadDownHolded         += middlewareController.LPadButtonDownHolded;
                hyperealVRController.LPadLeftClicked        += middlewareController.LPadButtonLeftClicked;
                hyperealVRController.LPadLeftUnclicked      += middlewareController.LPadButtonLeftUnclicked;
                hyperealVRController.LPadLeftHolded         += middlewareController.LPadButtonLeftHolded;
                hyperealVRController.LPadRightClicked       += middlewareController.LPadButtonRightClicked;
                hyperealVRController.LPadRightUnclicked     += middlewareController.LPadButtonRightUnclicked;
                hyperealVRController.LPadRightHolded        += middlewareController.LPadButtonRightHolded;

                hyperealVRController.RMenuButtonClicked     += middlewareController.RMenuButtonClicked;
                hyperealVRController.RMenuButtonUnclicked   += middlewareController.RMenuButtonUnclicked;
                hyperealVRController.RTriggerPressed        += middlewareController.RTriggerPressed;
                hyperealVRController.RTriggerUnpressed      += middlewareController.RTriggerUnpressed;
                hyperealVRController.RTriggerTouched        += middlewareController.RTriggerTouched;
                hyperealVRController.RGripButtonClicked     += middlewareController.RGripButtonClicked;
                hyperealVRController.RGripButtonUnclicked   += middlewareController.RGripButtonUnclicked;
                hyperealVRController.RPadClicked            += middlewareController.RPadClicked;
                hyperealVRController.RPadUnclicked          += middlewareController.RPadUnclicked;
                hyperealVRController.RPadClickedHolded      += middlewareController.RPadHolded;
                hyperealVRController.RPadUpClicked          += middlewareController.RPadButtonUpClicked;
                hyperealVRController.RPadUpUnclicked        += middlewareController.RPadButtonUpUnclicked;
                hyperealVRController.RPadUpHolded           += middlewareController.RPadButtonUpHolded;
                hyperealVRController.RPadDownClicked        += middlewareController.RPadButtonDownClicked;
                hyperealVRController.RPadDownUnclicked      += middlewareController.RPadButtonDownUnclicked;
                hyperealVRController.RPadDownHolded         += middlewareController.RPadButtonDownHolded;
                hyperealVRController.RPadLeftClicked        += middlewareController.RPadButtonLeftClicked;
                hyperealVRController.RPadLeftUnclicked      += middlewareController.RPadButtonLeftUnclicked;
                hyperealVRController.RPadLeftHolded         += middlewareController.RPadButtonLeftHolded;
                hyperealVRController.RPadRightClicked       += middlewareController.RPadButtonRightClicked;
                hyperealVRController.RPadRightUnclicked     += middlewareController.RPadButtonRightUnclicked;
                hyperealVRController.RPadRightHolded        += middlewareController.RPadButtonRightHolded;
            }
        }

        public void Unregister()
        {
            if (hyperealVRController != null)
            {
                hyperealVRController.MenuButtonClicked      -= middlewareController.MenuButtonClicked;
                hyperealVRController.MenuButtonUnclicked    -= middlewareController.MenuButtonUnclicked;
                hyperealVRController.TriggerPressed         -= middlewareController.TriggerPressed;
                hyperealVRController.TriggerUnpressed       -= middlewareController.TriggerUnpressed;
                hyperealVRController.TriggerTouched         -= middlewareController.TriggerTouched;
                hyperealVRController.GripButtonClicked      -= middlewareController.GripButtonClicked;
                hyperealVRController.GripButtonUnclicked    -= middlewareController.GripButtonUnclicked;
                hyperealVRController.PadClicked             -= middlewareController.PadClicked;
                hyperealVRController.PadUnclicked           -= middlewareController.PadUnclicked;
                hyperealVRController.PadClickedHolded       -= middlewareController.PadHolded;
                hyperealVRController.PadUpClicked           -= middlewareController.PadButtonUpClicked;
                hyperealVRController.PadUpUnclicked         -= middlewareController.PadButtonUpUnclicked;
                hyperealVRController.PadUpHolded            -= middlewareController.PadButtonUpHolded;
                hyperealVRController.PadDownClicked         -= middlewareController.PadButtonDownClicked;
                hyperealVRController.PadDownUnclicked       -= middlewareController.PadButtonDownUnclicked;
                hyperealVRController.PadDownHolded          -= middlewareController.PadButtonDownHolded;
                hyperealVRController.PadLeftClicked         -= middlewareController.PadButtonLeftClicked;
                hyperealVRController.PadLeftUnclicked       -= middlewareController.PadButtonLeftUnclicked;
                hyperealVRController.PadLeftHolded          -= middlewareController.PadButtonLeftHolded;
                hyperealVRController.PadRightClicked        -= middlewareController.PadButtonRightClicked;
                hyperealVRController.PadRightUnclicked      -= middlewareController.PadButtonRightUnclicked;
                hyperealVRController.PadRightHolded         -= middlewareController.PadButtonRightHolded;

                hyperealVRController.LMenuButtonClicked     -= middlewareController.LMenuButtonClicked;
                hyperealVRController.LMenuButtonUnclicked   -= middlewareController.LMenuButtonUnclicked;
                hyperealVRController.LTriggerPressed        -= middlewareController.LTriggerPressed;
                hyperealVRController.LTriggerUnpressed      -= middlewareController.LTriggerUnpressed;
                hyperealVRController.LTriggerTouched        -= middlewareController.LTriggerTouched;
                hyperealVRController.LGripButtonClicked     -= middlewareController.LGripButtonClicked;
                hyperealVRController.LGripButtonUnclicked   -= middlewareController.LGripButtonUnclicked;
                hyperealVRController.LPadClicked            -= middlewareController.LPadClicked;
                hyperealVRController.LPadUnclicked          -= middlewareController.LPadUnclicked;
                hyperealVRController.LPadClickedHolded      -= middlewareController.LPadHolded;
                hyperealVRController.LPadUpClicked          -= middlewareController.LPadButtonUpClicked;
                hyperealVRController.LPadUpUnclicked        -= middlewareController.LPadButtonUpUnclicked;
                hyperealVRController.LPadUpHolded           -= middlewareController.LPadButtonUpHolded;
                hyperealVRController.LPadDownClicked        -= middlewareController.LPadButtonDownClicked;
                hyperealVRController.LPadDownUnclicked      -= middlewareController.LPadButtonDownUnclicked;
                hyperealVRController.LPadDownHolded         -= middlewareController.LPadButtonDownHolded;
                hyperealVRController.LPadLeftClicked        -= middlewareController.LPadButtonLeftClicked;
                hyperealVRController.LPadLeftUnclicked      -= middlewareController.LPadButtonLeftUnclicked;
                hyperealVRController.LPadLeftHolded         -= middlewareController.LPadButtonLeftHolded;
                hyperealVRController.LPadRightClicked       -= middlewareController.LPadButtonRightClicked;
                hyperealVRController.LPadRightUnclicked     -= middlewareController.LPadButtonRightUnclicked;
                hyperealVRController.LPadRightHolded        -= middlewareController.LPadButtonRightHolded;

                hyperealVRController.RMenuButtonClicked     -= middlewareController.RMenuButtonClicked;
                hyperealVRController.RMenuButtonUnclicked   -= middlewareController.RMenuButtonUnclicked;
                hyperealVRController.RTriggerPressed        -= middlewareController.RTriggerPressed;
                hyperealVRController.RTriggerUnpressed      -= middlewareController.RTriggerUnpressed;
                hyperealVRController.RTriggerTouched        -= middlewareController.RTriggerTouched;
                hyperealVRController.RGripButtonClicked     -= middlewareController.RGripButtonClicked;
                hyperealVRController.RGripButtonUnclicked   -= middlewareController.RGripButtonUnclicked;
                hyperealVRController.RPadClicked            -= middlewareController.RPadClicked;
                hyperealVRController.RPadUnclicked          -= middlewareController.RPadUnclicked;
                hyperealVRController.RPadClickedHolded      -= middlewareController.RPadHolded;
                hyperealVRController.RPadUpClicked          -= middlewareController.RPadButtonUpClicked;
                hyperealVRController.RPadUpUnclicked        -= middlewareController.RPadButtonUpUnclicked;
                hyperealVRController.RPadUpHolded           -= middlewareController.RPadButtonUpHolded;
                hyperealVRController.RPadDownClicked        -= middlewareController.RPadButtonDownClicked;
                hyperealVRController.RPadDownUnclicked      -= middlewareController.RPadButtonDownUnclicked;
                hyperealVRController.RPadDownHolded         -= middlewareController.RPadButtonDownHolded;
                hyperealVRController.RPadLeftClicked        -= middlewareController.RPadButtonLeftClicked;
                hyperealVRController.RPadLeftUnclicked      -= middlewareController.RPadButtonLeftUnclicked;
                hyperealVRController.RPadLeftHolded         -= middlewareController.RPadButtonLeftHolded;
                hyperealVRController.RPadRightClicked       -= middlewareController.RPadButtonRightClicked;
                hyperealVRController.RPadRightUnclicked     -= middlewareController.RPadButtonRightUnclicked;
                hyperealVRController.RPadRightHolded        -= middlewareController.RPadButtonRightHolded;
            }
        }
    }
}
#endif