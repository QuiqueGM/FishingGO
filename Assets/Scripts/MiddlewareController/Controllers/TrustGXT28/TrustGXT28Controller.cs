using System;
using UnityEngine;

namespace VRMiddlewareController
{
    [RequireComponent(typeof(TrustGXT28Events))]
    public class TrustGXT28Controller : MonoBehaviour
    {
        [SerializeField]
        protected Hand handController;
        public bool crossButton = false;
        public bool circleButton = false;
        public bool triangleButton = false;
        public bool squareButton = false;
        public bool menuButton = false;

        public event ClickedEventHandler CrossButtonClicked;
        public event ClickedEventHandler CircleButtonClicked;
        public event ClickedEventHandler TriangleButtonClicked;
        public event ClickedEventHandler SquareButtonClicked;

        private SenderInfo senderInfo = new SenderInfo();
        private EventArguments eventArguments = new EventArguments();

        void Start() 
        {
            senderInfo = ControllerEvents.Instance.SenderInfo(gameObject, this.ToString());
            eventArguments = ControllerEvents.Instance.EventArguments(handController, 0, 0);
        }

        void Update()
        {
            GetCrossButton();
            GetCircleButton();
            GetTriangleButton();
            GetSquareButton();
        }

        private void GetCrossButton()
        {
            if (Input.GetButtonDown("Cross") && !crossButton)
            {
                crossButton = true;
                if (CrossButtonClicked != null ) CrossButtonClicked(senderInfo, eventArguments);
            }
            else if (Input.GetButtonUp("Cross") && crossButton)
                crossButton = false;
        }

        private void GetCircleButton()
        {
            if (Input.GetButtonDown("Circle") && !circleButton)
            {
                circleButton = true;
                if (CircleButtonClicked != null) CircleButtonClicked(senderInfo, eventArguments);
            }
            else if (Input.GetButtonUp("Circle") && circleButton)
                circleButton = false;
        }

        private void GetTriangleButton()
        {
            if (Input.GetButtonDown("Triangle") && !triangleButton)
            {
                triangleButton = true;
                if (TriangleButtonClicked != null) TriangleButtonClicked(senderInfo, eventArguments);
            }
            else if (Input.GetButtonUp("Triangle") && triangleButton)
                triangleButton = false;
        }

        private void GetSquareButton()
        {
            if (Input.GetButtonDown("Square") && !squareButton)
            {
                squareButton = true;
                if (SquareButtonClicked != null) SquareButtonClicked(senderInfo, eventArguments);
            }
            else if (Input.GetButtonUp("Square") && squareButton)
                squareButton = false;
        }
    }
}
