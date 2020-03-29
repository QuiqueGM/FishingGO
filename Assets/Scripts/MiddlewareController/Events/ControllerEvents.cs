using UnityEngine;

namespace VRMiddlewareController
{
    public delegate void ClickedEventHandler(SenderInfo sender, EventArguments e);
    public delegate void PointerEventHandler(PointerArguments e);
    public delegate void VibrationEventHandler(Hand hand, float pulse, float duration);
    public delegate void TrackpadModeEventHandler(Hand hand, TrackpadMode mode);
    public delegate void ChangeHandEventHandler(Hand targetHand, Hand desireHand);

    public enum Hand 
    {
        Right = 0,
        Left = 1,
        Any = 2
    }

    public enum ButtonBehavior
    {
        Clicked,
        Unclicked,
        Holded
    }

    public enum TrackpadMode
    {
        OneButton,
        UpAndDownButtons,
        LeftAndRightButtons,
        FourDirectionsButtons
    }

    public struct EventArguments 
    {
        public Hand hand;
        public float padX;
        public float padY;
    }

    public struct PointerArguments 
    {
        public float distance;
        public Transform target;
    }

    public struct SenderInfo 
    {
        public GameObject senderObject;
        public string eventAction;
        public string senderController;
    }

    public class ControllerEvents
    {
        private static ControllerEvents sharedinstance = null;

        public static ControllerEvents Instance 
        {
            get 
            {
                if (sharedinstance == null) 
                {
                    sharedinstance = new ControllerEvents();
                    return sharedinstance;
                } 
                else 
                {
                    return ControllerEvents.sharedinstance;
                }
            }
        }

        public SenderInfo SenderInfo (GameObject senderObject, string senderController, string eventAction = "") 
        {
            return new SenderInfo 
            {
                senderObject = senderObject,
                senderController = senderController,
                eventAction = eventAction
            };
        }

        public EventArguments EventArguments (Hand hand, float padX, float padY) 
        {
            return new EventArguments 
            {
                hand = hand,
                padX = padX,
                padY = padY
            };
        }
    }
}
