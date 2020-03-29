using UnityEngine;

namespace VRMiddlewareController
{
    public class MiddlewareController : MonoBehaviour
    {
        public event VibrationEventHandler VibrationEvent;
        public event TrackpadModeEventHandler TrackpadModeEvent;
        public event ChangeHandEventHandler ChangeHandEvent;

        #region REGISTER & UNREGISTER CONTROLLERS

        public bool showLogMessage;
        [Space]
        public GameObject leftController;
        public GameObject rightController;

        public virtual void Awake()
        {
            RegisterController(leftController);
            RegisterController(rightController);
        }

        private void OnDestroy()
        {
            UnregisterController(leftController);
            UnregisterController(rightController);
        }

        private void RegisterController(GameObject controller)
        {
            if (controller != null)
            {
                try {
                    controller.GetComponent<IRegisterEvents>().Register(this);
                }
                catch {
                    Debug.LogError(string.Format("Can't register {0}. Maybe the events controller compenent is missing.", controller.name));
                }
            }
        }

        private void UnregisterController(GameObject controller)
        {
            if (controller != null)
            {
                try {
                    controller.GetComponent<IRegisterEvents>().Unregister();
                }
                catch {
                    Debug.LogError(string.Format("{0} coudn't be unregistrated. Check if the events controller compenent exists.", controller.name));
                }
            }
        }

        #endregion

        #region ACTIONS IN (BOTH/ANY HANDS)

        public virtual void MenuButtonClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Menu Button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void MenuButtonUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Menu Button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void TriggerPressed(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger PRESSED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void TriggerUnpressed(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger UNPRESSED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void TriggerTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void GripButtonClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void GripButtonUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void GripButtonHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void GripButtonTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadUntouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad UNTOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadTouchedHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad TOUCHED & HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonUpClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonUpUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonUpHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonDownClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonDownUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonDownHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonLeftClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonLeftUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonLeftHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonRightClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonRightUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void PadButtonRightHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        #endregion

        #region ACTIONS IN (LFET HAND)

        public virtual void LMenuButtonClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Menu Button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LMenuButtonUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Menu Button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LTriggerPressed(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger PRESSED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LTriggerUnpressed(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger UNPRESSED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LTriggerTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LGripButtonClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LGripButtonUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LGripButtonHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LGripButtonTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadUntouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad UNTOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadTouchedHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad TOUCHED & HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonUpClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonUpUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonUpHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonDownClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonDownUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonDownHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonLeftClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonLeftUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonLeftHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonRightClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonRightUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void LPadButtonRightHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        #endregion

        #region ACTIONS IN (RIGTH HAND)

        public virtual void RMenuButtonClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Menu Button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RMenuButtonUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Menu Button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RTriggerPressed(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger PRESSED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RTriggerUnpressed(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger UNPRESSED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RTriggerTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Trigger TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RGripButtonClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RGripButtonUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RGripButtonHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RGripButtonTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Grip Button TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadTouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad TOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadUntouched(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad UNTOUCHED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadTouchedHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad TOUCHED & HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("Pad HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonUpClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonUpUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonUpHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("UP button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonDownClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonDownUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonDownHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("DOWN button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonLeftClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonLeftUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonLeftHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("LEFT button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonRightClicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button CLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonRightUnclicked(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button UNCLICKED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        public virtual void RPadButtonRightHolded(SenderInfo sender, EventArguments e) {
            ShowLogMessage(string.Format("RIGHT button HOLDED from {0} ({1})", sender.senderObject.name, e.hand));
        }

        #endregion

        private void ShowLogMessage(string message) {
            if (showLogMessage) Debug.Log(message);
        }

        #region ACTIONS OUT

        protected void SetVibration(Hand hand, float pulse, float duration = 0)
        {
            VibrationEvent(hand, pulse, duration);
        }

        protected void SetTrackpadMode(Hand hand, TrackpadMode mode)
        {
            TrackpadModeEvent(hand, mode);
        }

        protected void SetHand(Hand targetHand, Hand desireHand)
        {
            ChangeHandEvent(targetHand, desireHand);
        }

        #endregion
    }
}
