using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFG.Utils
{
    public class FishMovement : MonoBehaviour
    {
        const float MAX_VALUE = 100;

        public float minSpeed = 0.9f;
        public float maxSpeed = 1.1f;

        private float speed;
        private float delay;
        
        void Start()
        {
            speed = Random.Range(minSpeed, maxSpeed);
            delay = Random.Range(0, maxSpeed);

            Component[] mesh;

            mesh = GetComponentsInChildren(typeof(SkinnedMeshRenderer));

            if (mesh != null)
            {
                foreach (SkinnedMeshRenderer s in mesh)
                {
                    DOTween.To
                        (() => s.GetBlendShapeWeight(0), a => s.SetBlendShapeWeight(0, a), MAX_VALUE, speed)
                        .SetEase(Ease.Linear)
                        .SetLoops(-1)
                        .SetDelay(delay);
                }
            }
        }
    }
}