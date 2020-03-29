using UnityEngine;

namespace VRMiddlewareController
{
    public class TrustGXT28Events : MonoBehaviour, IRegisterEvents
    {
        MiddlewareController middlewareController;
        TrustGXT28Controller trustGTX28Controller;

        public void Register(MiddlewareController mControlller)
        {
            if (GetComponent<TrustGXT28Controller>() != null)
            {
                trustGTX28Controller = GetComponent<TrustGXT28Controller>();
                middlewareController = mControlller;

                trustGTX28Controller.CrossButtonClicked     += middlewareController.MenuButtonClicked;
                trustGTX28Controller.CircleButtonClicked    += middlewareController.MenuButtonClicked;
                trustGTX28Controller.TriangleButtonClicked  += middlewareController.MenuButtonClicked;
                trustGTX28Controller.SquareButtonClicked    += middlewareController.MenuButtonClicked;
            }
        }

        public void Unregister()
        {
            if (trustGTX28Controller != null)
            {
                trustGTX28Controller.CrossButtonClicked     -= middlewareController.MenuButtonClicked;
                trustGTX28Controller.CircleButtonClicked    -= middlewareController.MenuButtonClicked;
                trustGTX28Controller.TriangleButtonClicked  -= middlewareController.MenuButtonClicked;
                trustGTX28Controller.SquareButtonClicked    -= middlewareController.MenuButtonClicked;
            }
        }
    }
}