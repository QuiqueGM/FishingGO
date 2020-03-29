using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VFG.LevelEditor
{
    public class RotateObject : MonoBehaviour
    {
        void Start()
        {
            const int DURATION = 5;
            const int NUM_LOOPS = -1;
            const int COMPLETE_CIRCLE = 360;

            transform.DOLocalRotate(new Vector3(0, COMPLETE_CIRCLE, 0), DURATION, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(NUM_LOOPS);
        }
    }
}