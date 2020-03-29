using VRMiddlewareController;

namespace VFG.Canvas.LevelEditor
{
    public class ButtonDetection : LaserPointer
    {
        public override void OnPointerIn(PointerArguments e)
        {
            if (e.target.GetComponent<BaseButton>() != null)
                e.target.GetComponent<BaseButton>().ToggleAnimation(true);

            base.OnPointerIn(e);
        }

        public override void OnPointerOut(PointerArguments e)
        {
            if (e.target.GetComponent<BaseButton>() != null)
                e.target.GetComponent<BaseButton>().ToggleAnimation(false);

            base.OnPointerOut(e);
        }

        public void SetHolderVisible(bool state)
        {
            holder.SetActive(state);
        }
    }
}