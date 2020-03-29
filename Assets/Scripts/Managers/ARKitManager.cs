using UnityEngine;
using UnityEngine.XR.iOS;
using UnityStandardAssets.ImageEffects;
using VFG.Canvas;
using VFG.Core;
using VFG.Utils;

namespace VFG.Managers
{
    public class ARKitManager : MonoBehaviour
    {
        [Header("Canvas Manager")]
        public CanvasManager canvasManager;

        [Header("ARKit")]
		public UnityARCameraManager ARManager;
        public Camera camera;
        public GameObject container;
        public PlaceAquarium placeAquarium;
        public UnityARGeneratePlane unityARGeneratePlane;
        public PointsCloudGenerator pointsCloudGenerator;

        private CanvasManager CM;

        private void Awake()
        {
            CM = canvasManager;

            AddListeners();
            
            CM.CNV_Scannig.SetActive(false);
            CM.CNV_TimesUp.SetActive(false);
            CM.CNV_PlaceAquarium.SetActive(false);
            CM.CNV_Buttons.SetActive(false);

            pointsCloudGenerator.ContainerVisibility(true);
            unityARGeneratePlane.ContainerVisibility(false);

            container.SetActive(false);
        }

        private void AddListeners()
        {
            CM.CNV_Scannig.GetComponent<CanvasScanningObjects>().ShowWarningTimesUpEvent += ShowWarningCanvas;
            CM.CNV_TimesUp.GetComponent<CanvasWarning>().HideWarningEvent += HideWarningCanvas;
            placeAquarium.SendHitEvent += HideCanvasPlaceAquarium;
            UnityARUtility.SurfaceDetectedEvent += ShowCanvasPlaceAquarium;
        }

        void OnDestroy()
        {
            CM.CNV_Scannig.GetComponent<CanvasScanningObjects>().ShowWarningTimesUpEvent -= ShowWarningCanvas;
            CM.CNV_TimesUp.GetComponent<CanvasWarning>().HideWarningEvent -= HideWarningCanvas;
            placeAquarium.SendHitEvent -= HideCanvasPlaceAquarium;
            UnityARUtility.SurfaceDetectedEvent -= ShowCanvasPlaceAquarium;
        }

        public void ShowScanningSurfaces()
        {
#if UNITY_EDITOR
            CM.CNV_Scannig.SetActive(false);
            CM.CNV_TimesUp.SetActive(false);
            CM.CNV_PlaceAquarium.SetActive(true);
            return;
#endif
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

//			if (!ARManager.enabled)
//				ARManager.enabled = true;

            if (GameState.isFirstUseApp)
                ScanningSurfaces();
            else
                HideCanvasPlaceAquarium();
        }

        private void ScanningSurfaces()
        {
            camera.GetComponent<BloomOptimized>().enabled = false;
            container.SetActive(false);

            CM.CNV_Scannig.SetActive(true);
            CM.CNV_TimesUp.SetActive(false);
            CM.CNV_PlaceAquarium.SetActive(false);
            CM.CNV_Buttons.SetActive(false);

            pointsCloudGenerator.ContainerVisibility(true);
            unityARGeneratePlane.ContainerVisibility(false);

            GameState.isInScene = GameState.Scene.Scanning;
        }

        public void RelocateAquarium()
        {
            container.SetActive(false);
            GameState.isInScene = GameState.Scene.Scanning;
            ShowCanvasPlaceAquarium();
        }

        private void ShowWarningCanvas()
        {
            CM.CNV_Scannig.SetActive(false);
            CM.CNV_TimesUp.SetActive(true);

            pointsCloudGenerator.ContainerVisibility(false);
        }

        private void HideWarningCanvas()
        {
            CM.CNV_Scannig.SetActive(true);
            CM.CNV_TimesUp.SetActive(false);

            pointsCloudGenerator.ContainerVisibility(true);
        }

        public void ShowCanvasPlaceAquarium()
        {
            if (GameState.isInScene == GameState.Scene.Scanning)
            {
                CM.CNV_Scannig.SetActive(false);
                CM.CNV_TimesUp.SetActive(false);
                CM.CNV_PlaceAquarium.SetActive(true);
                placeAquarium.canvasPlaceAquariumIsOn = true;

                pointsCloudGenerator.ContainerVisibility(false);
                unityARGeneratePlane.ContainerVisibility(true);
            }
        }

        public void HideCanvasPlaceAquarium()
        {
            GameState.isFirstUseApp = false;

            camera.GetComponent<SetCameraEffects>().SetEffects();

            CM.CNV_PlaceAquarium.SetActive(false);
            CM.CNV_Buttons.SetActive(true);
            CM.CNV_ObjectiveTofind.GetComponent<CanvasObjectiveToFind>().Initialize();

            placeAquarium.canvasPlaceAquariumIsOn = false;
            container.SetActive(true);

            pointsCloudGenerator.ContainerVisibility(false);
            unityARGeneratePlane.ContainerVisibility(false);

            GameState.isInScene = GameState.Scene.InGame;
        }

        public void MainMenu()
        {
            GameState.isInScene = GameState.Scene.MainMenu;

            container.SetActive(false);
            CM.CNV_Buttons.SetActive(false);
            CM.GoToSelectedScreen(TypeOfARAction.CloseARCanvas);
        }
    }
}