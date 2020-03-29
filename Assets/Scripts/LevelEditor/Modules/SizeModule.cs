using UnityEngine;

namespace VFG.LevelEditor
{
    public class SizeModule : MonoBehaviour
    {
        public float InitPosition = 0.135f;
        public float ScaleSensitivity = 0.15f;
        public float MinSize = 0.03f;
        public float MaxSize = 2.0f;

        public void ChangeSize(Transform leftSelector, Transform rightSelector)
        {
            float scaleFactor = ScaleSensitivity * Time.deltaTime;

            if (EditorState.currentAction == EditorState.TypeOfAction.DecreaseSize)
            {
                leftSelector.localScale = rightSelector.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
                if (leftSelector.localScale.x < MinSize) leftSelector.localScale = rightSelector.localScale = new Vector3(MinSize, MinSize, MinSize);
            }

            if (EditorState.currentAction == EditorState.TypeOfAction.IncreaseSize)
            {
                leftSelector.localScale = rightSelector.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);
                if (leftSelector.localScale.x > MaxSize) leftSelector.localScale = rightSelector.localScale = new Vector3(MaxSize, MaxSize, MaxSize);
            }

            leftSelector.localPosition = rightSelector.localPosition = new Vector3(0, 0, (InitPosition + ((leftSelector.localScale.x - MinSize) / 2)));
        }
    }
}