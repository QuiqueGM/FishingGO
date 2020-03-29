namespace VRMiddlewareController
{
    public interface IEvents
    {
        void GetMenuButton();
        void GetTrigger();
        void GetGripButton();
        void GetPadTouched();
        void GetPadClicked();

        //void SetControllerVibration(Hand hand, float pulse, float duration);
        //void SetTrackpadMode(Hand hand, TrackpadMode mode);
    }
}
