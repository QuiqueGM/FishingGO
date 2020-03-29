#if UNITY_PS4
using UnityEngine;

namespace VRUtils
{
    public class PS4MoveEvents : MonoBehaviour, IEvents
    {
        [HideInInspector]
        public MiddlewareController middlewareController;
        PS4MoveController PS4MoveController;

        public void Register(MiddlewareController mControlller)
        {
            if (GetComponent<PS4MoveController>() != null)
            {
                PS4MoveController = GetComponent<PS4MoveController>();
                middlewareController = mControlller;
                PS4MoveController.SelectButtonClicked       += middlewareController.GripButtonClicked;
                PS4MoveController.SelectButtonUnclicked     += middlewareController.GripButtonUnclicked;
                PS4MoveController.TriggerPressed            += middlewareController.TriggerPressed;
                PS4MoveController.TriggerUnpressed          += middlewareController.TriggerUnpressed;
                PS4MoveController.TriggerTouched            += middlewareController.TriggerTouched;
                PS4MoveController.CentralButtonClicked      += middlewareController.PadClicked;
                PS4MoveController.CentralButtonUnclicked    += middlewareController.PadUnclicked;
                PS4MoveController.StartButtonClicked        += middlewareController.PadTouched;
                PS4MoveController.StartButtonUnclicked      += middlewareController.PadUntouched;
                PS4MoveController.TriangleButtonClicked     += middlewareController.PadButtonDownClicked;
                PS4MoveController.TriangleButtonUnclicked   += middlewareController.PadButtonDownUnclicked;
                PS4MoveController.CircleButtonClicked       += middlewareController.PadButtonRightClicked;
                PS4MoveController.CircleButtonUnclicked     += middlewareController.PadButtonRightUnclicked;
                PS4MoveController.CrossButtonClicked        += middlewareController.PadButtonLeftClicked;
                PS4MoveController.CrossButtonUnclicked      += middlewareController.PadButtonLeftUnclicked;
                PS4MoveController.SquareButtonClicked       += middlewareController.PadButtonUpClicked;
                PS4MoveController.SquareButtonUnclicked     += middlewareController.PadButtonUpUnclicked;
            }
        }

        public void Unregister()
        {
            if (PS4MoveController != null)
            {
                PS4MoveController.SelectButtonClicked       -= middlewareController.GripButtonClicked;
                PS4MoveController.SelectButtonUnclicked     -= middlewareController.GripButtonUnclicked;
                PS4MoveController.TriggerPressed            -= middlewareController.TriggerPressed;
                PS4MoveController.TriggerUnpressed          -= middlewareController.TriggerUnpressed;
                PS4MoveController.TriggerTouched            -= middlewareController.TriggerTouched;
                PS4MoveController.CentralButtonClicked      -= middlewareController.PadClicked;
                PS4MoveController.CentralButtonUnclicked    -= middlewareController.PadUnclicked;
                PS4MoveController.StartButtonClicked        -= middlewareController.PadTouched;
                PS4MoveController.StartButtonUnclicked      -= middlewareController.PadUntouched;
                PS4MoveController.TriangleButtonClicked     -= middlewareController.PadButtonDownClicked;
                PS4MoveController.TriangleButtonUnclicked   -= middlewareController.PadButtonDownUnclicked;
                PS4MoveController.CircleButtonClicked       -= middlewareController.PadButtonRightClicked;
                PS4MoveController.CircleButtonUnclicked     -= middlewareController.PadButtonRightUnclicked;
                PS4MoveController.CrossButtonClicked        -= middlewareController.PadButtonLeftClicked;
                PS4MoveController.CrossButtonUnclicked      -= middlewareController.PadButtonLeftUnclicked;
                PS4MoveController.SquareButtonClicked       -= middlewareController.PadButtonUpClicked;
                PS4MoveController.SquareButtonUnclicked     -= middlewareController.PadButtonUpUnclicked;
            }
        }
    }
}
#endif

