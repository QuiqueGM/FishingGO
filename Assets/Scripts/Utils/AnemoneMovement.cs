using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFG.Utils
{
    public class AnemoneMovement : MonoBehaviour
    {
        const float INIT_VALUE = 0;
        const float MAX_VALUE = 100;
        const float MIN_TENT_DURATION = 2;
        const float MAX_TENT_DURATION = 5;
        const float MIN_MOVE_DURATION = 2;
        const float MAX_MOVE_DURATION = 5;

        public enum Direction { Base, Up, Right, Down, Left};
        public bool moveTentacles = true;
        public bool moveBody = true;

        private SkinnedMeshRenderer mesh;
        
        private void Start()
        {
            mesh = GetComponent<SkinnedMeshRenderer>();

            for (int n = 0; n < 5; n++)
                mesh.SetBlendShapeWeight(n, INIT_VALUE);

            if (moveTentacles) MoveTentacles(3);
            if (moveBody) MoveMainDirection(3);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) MoveMainDirection(1);
        }

        private void MoveMainDirection(float duration)
        {
            int mainDirection = Random.Range((int)Direction.Up, (int)Direction.Left + 1);
            MoveSecondaryDirection(duration, mainDirection);
            
            DOTween.To
                (() => mesh.GetBlendShapeWeight(mainDirection), a => mesh.SetBlendShapeWeight(mainDirection, a), MAX_VALUE, duration)
                .SetEase(Ease.InOutCubic)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => MoveMainDirection(Random.Range(MIN_MOVE_DURATION, MAX_MOVE_DURATION)));
        }

        private void MoveSecondaryDirection(float duration, int mainDirection)
        {
            int secondDirection = GetSecondaryDirection(mainDirection);

            DOTween.To
                (() => mesh.GetBlendShapeWeight(secondDirection), a => mesh.SetBlendShapeWeight(secondDirection, a), MAX_VALUE/2, duration)
                .SetEase(Ease.InOutCubic)
                .SetLoops(2, LoopType.Yoyo);
        }

        private int GetSecondaryDirection(int mainDirection)
        {
            int n = Random.Range(0, 2) == 0 ? -1 : 1;
            int sec = mainDirection + n;

            if (sec < (int)Direction.Up) sec = (int)Direction.Left;
            else if (sec > (int)Direction.Left) sec = (int)Direction.Up;

            return sec;
        }

        private void MoveTentacles(float duration)
        {
            mesh.SetBlendShapeWeight((int)Direction.Base, INIT_VALUE);

            DOTween.To
                (() => mesh.GetBlendShapeWeight((int)Direction.Base), a => mesh.SetBlendShapeWeight((int)Direction.Base, a), MAX_VALUE, duration)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .OnComplete(() => MoveTentacles(Random.Range(MIN_TENT_DURATION, MAX_TENT_DURATION)));
        }
    }
}