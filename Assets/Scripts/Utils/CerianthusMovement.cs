using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFG.Utils
{
    public class CerianthusMovement : MonoBehaviour
    {
        const float MAX_VALUE = 100;
        const float DURATION = 5f;

        private SkinnedMeshRenderer mesh;

        void Start()
        {
            mesh = GetComponent<SkinnedMeshRenderer>();
            mesh.SetBlendShapeWeight(0, 0);

            DOTween.To
                (() => mesh.GetBlendShapeWeight(0), a => mesh.SetBlendShapeWeight(0, a), MAX_VALUE, DURATION)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}