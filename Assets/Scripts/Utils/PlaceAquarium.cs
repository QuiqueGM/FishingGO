using System;
using System.Collections.Generic;
using VFG.Core;

namespace UnityEngine.XR.iOS
{
	public delegate void HitEvent();

	public class PlaceAquarium : MonoBehaviour
	{
		public event HitEvent SendHitEvent;
        public bool canvasPlaceAquariumIsOn;
		private Transform _parent;
		private GameObject _FSBackUp;
        private GameObject _fishSchool;

        private void Awake()
        {
            _parent = gameObject.transform.parent.gameObject.transform;
            canvasPlaceAquariumIsOn = false;
        }

        void Update ()
        {
			if (Input.touchCount > 0 && canvasPlaceAquariumIsOn)
			{
				var touch = Input.GetTouch(0);

				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
				{
					SendHitEvent();

                    var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

                    // prioritize reults types
                    ARHitTestResultType[] resultTypes = {
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                        // if you want to use infinite planes use this:
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    }; 
					
                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
                        {
                            return;
                        }
                    }
				}
			}
		}

        bool HitTestWithResultType(ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface().HitTest(point, resultTypes);
            if (hitResults.Count > 0)
            {
                foreach (var hitResult in hitResults)
                {
                    Debug.Log("Got hit! (PlaceAquarium)");
                    _parent.position = UnityARMatrixOps.GetPosition(hitResult.worldTransform);
                    _parent.rotation = UnityARMatrixOps.GetRotation(hitResult.worldTransform);
                    //Debug.Log(string.Format("x:{0:0.##} y:{1:0.##} z:{2:0.##}", _parent.position.x, _parent.position.y, _parent.position.z));
                    return true;
                }
            }
            return false;
        }
    }
}

