namespace VRMiddlewareController
{
    public interface IRegisterEvents
    {
        void Register(MiddlewareController mControlller);
        void Unregister();
    }
}
