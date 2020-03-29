using UnityEngine;

namespace VRMiddlewareController
{
    public class BaseController : MonoBehaviour
    {
        #region EVENTS & VARIABLES

        public event ClickedEventHandler MenuButtonClicked;
        public event ClickedEventHandler MenuButtonUnclicked;
        public event ClickedEventHandler TriggerPressed;
        public event ClickedEventHandler TriggerUnpressed;
        public event ClickedEventHandler TriggerTouched;
        public event ClickedEventHandler GripButtonClicked;
        public event ClickedEventHandler GripButtonUnclicked;
        public event ClickedEventHandler GripButtonHolded;
        public event ClickedEventHandler PadTouched;
        public event ClickedEventHandler PadUntouched;
        public event ClickedEventHandler PadTouchedHolded;
        public event ClickedEventHandler PadClicked;
        public event ClickedEventHandler PadUnclicked;
        public event ClickedEventHandler PadClickedHolded;
        public event ClickedEventHandler PadUpClicked;
        public event ClickedEventHandler PadUpUnclicked;
        public event ClickedEventHandler PadUpHolded;
        public event ClickedEventHandler PadDownClicked;
        public event ClickedEventHandler PadDownUnclicked;
        public event ClickedEventHandler PadDownHolded;
        public event ClickedEventHandler PadLeftClicked;
        public event ClickedEventHandler PadLeftUnclicked;
        public event ClickedEventHandler PadLeftHolded;
        public event ClickedEventHandler PadRightClicked;
        public event ClickedEventHandler PadRightUnclicked;
        public event ClickedEventHandler PadRightHolded;

        public event ClickedEventHandler LMenuButtonClicked;
        public event ClickedEventHandler LMenuButtonUnclicked;
        public event ClickedEventHandler LTriggerPressed;
        public event ClickedEventHandler LTriggerUnpressed;
        public event ClickedEventHandler LTriggerTouched;
        public event ClickedEventHandler LGripButtonClicked;
        public event ClickedEventHandler LGripButtonUnclicked;
        public event ClickedEventHandler LGripButtonHolded;
        public event ClickedEventHandler LPadTouched;
        public event ClickedEventHandler LPadUntouched;
        public event ClickedEventHandler LPadTouchedHolded;
        public event ClickedEventHandler LPadClicked;
        public event ClickedEventHandler LPadUnclicked;
        public event ClickedEventHandler LPadClickedHolded;
        public event ClickedEventHandler LPadUpClicked;
        public event ClickedEventHandler LPadUpUnclicked;
        public event ClickedEventHandler LPadUpHolded;
        public event ClickedEventHandler LPadDownClicked;
        public event ClickedEventHandler LPadDownUnclicked;
        public event ClickedEventHandler LPadDownHolded;
        public event ClickedEventHandler LPadLeftClicked;
        public event ClickedEventHandler LPadLeftUnclicked;
        public event ClickedEventHandler LPadLeftHolded;
        public event ClickedEventHandler LPadRightClicked;
        public event ClickedEventHandler LPadRightUnclicked;
        public event ClickedEventHandler LPadRightHolded;

        public event ClickedEventHandler RMenuButtonClicked;
        public event ClickedEventHandler RMenuButtonUnclicked;
        public event ClickedEventHandler RTriggerPressed;
        public event ClickedEventHandler RTriggerUnpressed;
        public event ClickedEventHandler RTriggerTouched;
        public event ClickedEventHandler RGripButtonClicked;
        public event ClickedEventHandler RGripButtonUnclicked;
        public event ClickedEventHandler RGripButtonHolded;
        public event ClickedEventHandler RPadTouched;
        public event ClickedEventHandler RPadUntouched;
        public event ClickedEventHandler RPadTouchedHolded;
        public event ClickedEventHandler RPadClicked;
        public event ClickedEventHandler RPadUnclicked;
        public event ClickedEventHandler RPadClickedHolded;
        public event ClickedEventHandler RPadUpClicked;
        public event ClickedEventHandler RPadUpUnclicked;
        public event ClickedEventHandler RPadUpHolded;
        public event ClickedEventHandler RPadDownClicked;
        public event ClickedEventHandler RPadDownUnclicked;
        public event ClickedEventHandler RPadDownHolded;
        public event ClickedEventHandler RPadLeftClicked;
        public event ClickedEventHandler RPadLeftUnclicked;
        public event ClickedEventHandler RPadLeftHolded;
        public event ClickedEventHandler RPadRightClicked;
        public event ClickedEventHandler RPadRightUnclicked;
        public event ClickedEventHandler RPadRightHolded;

        [SerializeField] protected Hand handController;
        [SerializeField] protected bool menuButtonIsPressed = false;
        [SerializeField] protected bool triggerButtonIsPressed = false;
        [SerializeField] protected float triggerValue;
        [SerializeField] protected bool gripButtonIsPressed = false;
        [SerializeField] protected bool trackpadIsTouched = false;
        [SerializeField] protected Vector2 trackpadValue;
        [Space]
        [SerializeField] protected TrackpadMode trackpadMode;
        [SerializeField] protected bool trackpadIsPressed = false;
        [SerializeField] protected bool upButtonIsPressed = false;
        [SerializeField] protected bool downButtonIsPressed = false;
        [SerializeField] protected bool leftButtonIsPressed = false;
        [SerializeField] protected bool rightButtonIsPressed = false;

        protected SenderInfo senderInfo = new SenderInfo();
        protected EventArguments eventArguments = new EventArguments();

        #endregion

        public virtual void Start()
        {
            senderInfo = ControllerEvents.Instance.SenderInfo(gameObject, this.ToString());
            eventArguments = ControllerEvents.Instance.EventArguments(handController, 0, 0);
        }

        public virtual void OnDestroy()
        {
        }

        protected void OnMenuButtonClicked(SenderInfo sender, EventArguments arg) 
        {
            menuButtonIsPressed = true;
            HandControllerEvent(MenuButtonClicked, LMenuButtonClicked, RMenuButtonClicked, sender, arg);
        }

        protected void OnMenuButtonUnclicked(SenderInfo sender, EventArguments arg) 
        {
            menuButtonIsPressed = false;
            HandControllerEvent(MenuButtonUnclicked, LMenuButtonUnclicked, RMenuButtonUnclicked, sender, arg);
        }

        protected void OnTriggerPressed(SenderInfo sender, EventArguments arg) 
        {
            triggerButtonIsPressed = true;
            HandControllerEvent(TriggerPressed, LTriggerPressed, RTriggerPressed, sender, arg);
        }

        protected void OnTriggerUnpressed(SenderInfo sender, EventArguments arg) 
        {
            triggerButtonIsPressed = false;
            HandControllerEvent(TriggerUnpressed, LTriggerUnpressed, RTriggerUnpressed, sender, arg);
        }

        protected void OnTriggerTouched(SenderInfo sender, EventArguments arg) 
        {
            HandControllerEvent(TriggerTouched, LTriggerTouched, RTriggerTouched, sender, arg);
        }

        protected void OnGripButtonClicked(SenderInfo sender, EventArguments arg) 
        {
            gripButtonIsPressed = true;
            HandControllerEvent(GripButtonClicked, LGripButtonClicked, RGripButtonClicked, sender, arg);
        }

        protected void OnGripButtonUnclicked(SenderInfo sender, EventArguments arg) 
        {
            gripButtonIsPressed = false;
            HandControllerEvent(GripButtonUnclicked, LGripButtonUnclicked, RGripButtonUnclicked, sender, arg);
        }

        protected void OnGripButtonHolded(SenderInfo sender, EventArguments arg) 
        {
            HandControllerEvent(GripButtonHolded, LGripButtonHolded, RGripButtonHolded, sender, arg);
        }

        protected void OnPadTouched(SenderInfo sender, EventArguments arg) 
        {
            trackpadIsTouched = true;
            HandControllerEvent(PadTouched, LPadTouched, RPadTouched, sender, arg);
        }

        protected void OnPadUntouched(SenderInfo sender, EventArguments arg) 
        {
            trackpadIsTouched = false;
            HandControllerEvent(PadUntouched, LPadUntouched, RPadUntouched, sender, arg);
        }

        protected void OnPadTouchedHolded(SenderInfo sender, EventArguments arg)
        {
            HandControllerEvent(PadTouchedHolded, LPadTouchedHolded, RPadTouchedHolded, sender, arg);
        }

        protected void OnPadClicked(SenderInfo sender, EventArguments arg) 
        {
            trackpadIsPressed = true;
            HandControllerEvent(PadClicked, LPadClicked, RPadClicked, sender, arg);
        }

        protected void OnPadUnclicked(SenderInfo sender, EventArguments arg) 
        {
            trackpadIsPressed = false;
            HandControllerEvent(PadUnclicked, LPadUnclicked, RPadUnclicked, sender, arg);
        }

        protected void OnPadClickedHolded(SenderInfo sender, EventArguments arg)
        {
            HandControllerEvent(PadClickedHolded, LPadClickedHolded, RPadClickedHolded, sender, arg);
        }

        protected void GetTrackpadButtonsBehaviour(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            trackpadIsPressed = state;
            switch (trackpadMode)
            {
                case TrackpadMode.UpAndDownButtons:     GetUpAndDownButtons(sender, arg, mode, state);      break;
                case TrackpadMode.LeftAndRightButtons:  GetLeftAndRightButtons(sender, arg, mode, state);   break;
                case TrackpadMode.FourDirectionsButtons: GetFourDirectionButtons(sender, arg, mode, state);    break;
            }
        }

        private void GetUpAndDownButtons(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            if (arg.padY > 0)
                UpButtonBehaviour(sender, arg, mode, state);
            else
                DownButtonBehaviour(sender, arg, mode, state);
        }

        private void GetLeftAndRightButtons(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            if (arg.padX > 0)
                RightButtonBehaviour(sender, arg, mode, state);
            else
                LeftButtonBehaviour(sender, arg, mode, state);
        }

        private void GetFourDirectionButtons(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            if (Mathf.Abs(arg.padX) > Mathf.Abs(arg.padY))
                GetLeftAndRightButtons(sender, arg, mode, state);
            else
                GetUpAndDownButtons(sender, arg, mode, state);
        }

        private void UpButtonBehaviour(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            ResetButtonsStates(state, false, false, false);
            switch (mode)
            {
                case ButtonBehavior.Clicked: HandControllerEvent(PadUpClicked, LPadUpClicked, RPadUpClicked, sender, arg); break;
                case ButtonBehavior.Unclicked: HandControllerEvent(PadUpUnclicked, LPadUpUnclicked, RPadUpUnclicked, sender, arg); break;
                case ButtonBehavior.Holded: HandControllerEvent(PadUpHolded, LPadUpHolded, RPadUpHolded, sender, arg); break;
            }
        }

        private void DownButtonBehaviour(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            ResetButtonsStates(false, state, false, false);
            switch (mode)
            {
                case ButtonBehavior.Clicked: HandControllerEvent(PadDownClicked, LPadDownClicked, RPadDownClicked, sender, arg); break;
                case ButtonBehavior.Unclicked: HandControllerEvent(PadDownUnclicked, LPadDownUnclicked, RPadDownUnclicked, sender, arg); break;
                case ButtonBehavior.Holded: HandControllerEvent(PadDownHolded, LPadDownHolded, RPadDownHolded, sender, arg); break;
            }
        }

        private void RightButtonBehaviour(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            ResetButtonsStates(false, false, state, false);
            switch (mode)
            {
                case ButtonBehavior.Clicked: HandControllerEvent(PadRightClicked, LPadRightClicked, RPadRightClicked, sender, arg); break;
                case ButtonBehavior.Unclicked: HandControllerEvent(PadRightUnclicked, LPadRightUnclicked, RPadRightUnclicked, sender, arg); break;
                case ButtonBehavior.Holded: HandControllerEvent(PadRightHolded, LPadRightHolded, RPadRightHolded, sender, arg); break;
            }
        }

        private void LeftButtonBehaviour(SenderInfo sender, EventArguments arg, ButtonBehavior mode, bool state)
        {
            ResetButtonsStates(false, false, false, state);
            switch (mode)
            {
                case ButtonBehavior.Clicked: HandControllerEvent(PadLeftClicked, LPadLeftClicked, RPadLeftClicked, sender, arg); break;
                case ButtonBehavior.Unclicked: HandControllerEvent(PadLeftUnclicked, LPadLeftUnclicked, RPadLeftUnclicked, sender, arg); break;
                case ButtonBehavior.Holded: HandControllerEvent(PadLeftHolded, LPadLeftHolded, RPadLeftHolded, sender, arg); break;
            }
        }

        private void ResetButtonsStates(params bool[] state) 
        {
            upButtonIsPressed = state[0];
            downButtonIsPressed = state[1];
            rightButtonIsPressed = state[2];
            leftButtonIsPressed = state[3];
        }

        private void HandControllerEvent(ClickedEventHandler Both, ClickedEventHandler LEvent, ClickedEventHandler REvent, SenderInfo sender, EventArguments arg)
        {
            SendEvent(Both, sender, arg);
            if (handController == Hand.Left) SendEvent(LEvent, sender, arg);
            if (handController == Hand.Right) SendEvent(REvent, sender, arg);
        }

        private void SendEvent(ClickedEventHandler @event, SenderInfo sender, EventArguments arg)
        {
            ClickedEventHandler handler = @event;
            if (handler != null) handler(sender, arg);
        }

        protected void SetTrackpadMode(Hand hand, TrackpadMode mode)
        {
            if (hand == handController)
                trackpadMode = mode;
        }

        protected void SetHand(Hand targetHand, Hand desireHand)
        {
            if (handController == targetHand)
                handController = desireHand;
        }
    }
}