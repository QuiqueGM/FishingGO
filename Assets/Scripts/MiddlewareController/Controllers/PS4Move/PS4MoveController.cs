#if UNITY_PS4
using System;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.PS4;

namespace VRUtils
{
    [RequireComponent(typeof(PS4MoveEvents))]
    public class PS4MoveController : MonoBehaviour
    {
        const int NUMBER_OF_BITS = 8;

        public Hand controllerIndex;
        public bool selectButtonIsPressed = false;
        public bool triggerButtonIsPressed = false;
        public bool centralButtonIsPressed = false;
        public bool startButtonIsPressed = false;
        public bool triangleButtonIsPressed = false;
        public bool circleButtonIsPressed = false;
        public bool crossButtonIsPressed = false;
        public bool squareButtonIsPressed = false;

        public event ClickedEventHandler SelectButtonClicked;
        public event ClickedEventHandler SelectButtonUnclicked;
        public event ClickedEventHandler TriggerPressed;
        public event ClickedEventHandler TriggerUnpressed;
        public event ClickedEventHandler TriggerTouched;
        public event ClickedEventHandler CentralButtonClicked;
        public event ClickedEventHandler CentralButtonUnclicked;
        public event ClickedEventHandler StartButtonClicked;
        public event ClickedEventHandler StartButtonUnclicked;
        public event ClickedEventHandler TriangleButtonClicked;
        public event ClickedEventHandler TriangleButtonUnclicked;
        public event ClickedEventHandler CircleButtonClicked;
        public event ClickedEventHandler CircleButtonUnclicked;
        public event ClickedEventHandler CrossButtonClicked;
        public event ClickedEventHandler CrossButtonUnclicked;
        public event ClickedEventHandler SquareButtonClicked;
        public event ClickedEventHandler SquareButtonUnclicked;

        //[SerializeField]
        private float triggerValue;
        private int curState, prevState;
        private string curStateInBits;
        private SenderInfo senderInfo = new SenderInfo();
        private EventArguments eventArguments = new EventArguments();

        void Start()
        {
            curState = prevState = 0;
            GetComponent<PS4MoveEvents>().middlewareController.VibrationEvent += SetControllerVibration;
            senderInfo = ControllerEvents.Instance.SenderInfo(gameObject, this.ToString());
            eventArguments = ControllerEvents.Instance.EventArguments(0, 0);
        }

        public void Update()
        {
            if (VRSettings.loadedDeviceName != VRDeviceNames.PlayStationVR) return;

            curState = PS4Input.MoveGetButtons(0, (int)controllerIndex);

            if (curState != prevState)
            {
                curStateInBits = SetStringByteFormat(Convert.ToString(curState, 2));

                GetSelect(GetBoolFromByte(curStateInBits[7]));
                GetTrigger(GetBoolFromByte(curStateInBits[6]));
                GetCentral(GetBoolFromByte(curStateInBits[5]));
                GetStart(GetBoolFromByte(curStateInBits[4]));
                GetTriangle(GetBoolFromByte(curStateInBits[3]));
                GetCircle(GetBoolFromByte(curStateInBits[2]));
                GetCross(GetBoolFromByte(curStateInBits[1]));
                GetSquare(GetBoolFromByte(curStateInBits[0]));

                prevState = curState;
            }
        }

        private string SetStringByteFormat(string original)
        {
            string str = original;

            for (int n = 0; n < NUMBER_OF_BITS - original.Length; n++) {
                str = str.Insert(0, "0");
            }

            return str;
        }

        private bool GetBoolFromByte(char a)
        {
            return (a == '0') ? false : true;
        }

        private void GetSelect(bool curState)
        {
            if (selectButtonIsPressed == curState) return;

            if (!selectButtonIsPressed)
            {
                selectButtonIsPressed = true;
                if (SelectButtonClicked != null) SelectButtonClicked(senderInfo, eventArguments);
            }
            else
            {
                selectButtonIsPressed = false;
                if (SelectButtonUnclicked != null) SelectButtonUnclicked(senderInfo, eventArguments);
            }
        }

        private void GetTrigger(bool curState)
        {
            if (curState) GetTriggerAxis();
            if (triggerButtonIsPressed == curState) return;

            if (!triggerButtonIsPressed)
            {
                triggerButtonIsPressed = true;
                if (TriggerPressed != null) TriggerPressed(senderInfo, eventArguments);
            }
            else
            {
                triggerButtonIsPressed = false;
                if (TriggerUnpressed != null) TriggerUnpressed(senderInfo, eventArguments);
            }
        }

        private void GetTriggerAxis()
        {
            float axis = (float)PS4Input.MoveGetAnalogButton(0, (uint)controllerIndex) / 255;
            if (TriggerTouched != null) TriggerTouched(senderInfo, ControllerEvents.Instance.EventArguments(axis, axis));
        }

        private void GetCentral(bool curState)
        {
            if (centralButtonIsPressed == curState) return;

            if (!centralButtonIsPressed)
            {
                centralButtonIsPressed = true;
                if (CentralButtonClicked != null) CentralButtonClicked(senderInfo, eventArguments);
            }
            else
            {
                centralButtonIsPressed = false;
                if (CentralButtonUnclicked != null) CentralButtonUnclicked(senderInfo, eventArguments);
            }
        }

        private void GetStart(bool curState)
        {
            if (startButtonIsPressed == curState) return;

            if (!startButtonIsPressed)
            {
                startButtonIsPressed = true;
                if (StartButtonClicked != null) StartButtonClicked(senderInfo, eventArguments);
            }
            else
            { 
                startButtonIsPressed = false;
                if (StartButtonUnclicked != null) StartButtonUnclicked(senderInfo, eventArguments);
            }
        }

        private void GetTriangle(bool curState)
        {
            if (triangleButtonIsPressed == curState) return;

            if (!triangleButtonIsPressed)
            {
                triangleButtonIsPressed = true;
                if (TriangleButtonClicked != null) TriangleButtonClicked(senderInfo, eventArguments);
            }
            else
            {
                triangleButtonIsPressed = false;
                if (TriangleButtonUnclicked != null) TriangleButtonUnclicked(senderInfo, eventArguments);
            }
        }

        private void GetCircle(bool curState)
        {
            if (circleButtonIsPressed == curState) return;

            if (!circleButtonIsPressed)
            {
                circleButtonIsPressed = true;
                if (CircleButtonClicked != null) CircleButtonClicked(senderInfo, eventArguments);
            }
            else
            {
                circleButtonIsPressed = false;
                if (CircleButtonUnclicked != null) CircleButtonUnclicked(senderInfo, eventArguments);
            }
        }

        private void GetCross(bool curState)
        {
            if (crossButtonIsPressed == curState) return;

            if (!crossButtonIsPressed)
            {
                crossButtonIsPressed = true;
                if (CrossButtonClicked != null) CrossButtonClicked(senderInfo, eventArguments);
            }
            else
            {
                crossButtonIsPressed = false;
                if (CrossButtonUnclicked != null) CrossButtonUnclicked(senderInfo, eventArguments);
            }
        }

        private void GetSquare(bool curState)
        {
            if (squareButtonIsPressed == curState) return;

            if (!squareButtonIsPressed)
            {
                squareButtonIsPressed = true;
                if (SquareButtonClicked != null) SquareButtonClicked(senderInfo, eventArguments);
            }
            else
            {
                squareButtonIsPressed = false;
                if (SquareButtonUnclicked != null) SquareButtonUnclicked(senderInfo, eventArguments);
            }
        }

        private void SetControllerVibration(float pulse)
        {
            PS4Input.MoveSetVibration(0, (int)controllerIndex, Mathf.RoundToInt(pulse));
        }
    }
}
#endif