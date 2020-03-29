using System;
using System.Collections.Generic;
using UnityEngine;
using VRMiddlewareController;
using System.Linq;
using VFG.Core;
using VFG.Core.Audio;

namespace VFG.LevelEditor
{
    public class TestController : MiddlewareController
    {
        [Header("Properties")]
        public float speed;
        public GameObject oceanSurfaceReference;
        public GameObject mainCamera;

        public override void PadButtonDownHolded(SenderInfo sender, EventArguments e)
        {
            if (gameObject.transform.localPosition.y > 0.3f)
                gameObject.transform.localPosition += Time.deltaTime * Vector3.down * speed;

            base.PadButtonDownHolded(sender, e);
        }

        public override void PadButtonUpHolded(SenderInfo sender, EventArguments e)
        {
            if (gameObject.transform.localPosition.y < 7)
                gameObject.transform.localPosition += Time.deltaTime * Vector3.up * speed;

            base.PadButtonDownClicked(sender, e);
        }

        //public void Update()
        //{
        //    if (mainCamera.transform.position.y > oceanSurfaceReference.transform.position.y)
        //        Debug.Log("<color=green>OUT</color>");
        //    else
        //        Debug.Log("<color=red>IN</color>");
        //}
    }
}