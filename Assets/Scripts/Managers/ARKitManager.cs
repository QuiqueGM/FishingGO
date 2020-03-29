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
        [Header("Managers")]
        public CanvasManager canvasManager;

        [Header("ARKit")]
		public UnityARCameraManager ARManager;
        public Camera mainCamera;
        public GameObject container;
        public PlaceAquarium placeAquarium;
        public UnityARGeneratePlane unityARGeneratePlane;
        public PointsCloudGenerator pointsCloudGenerator;

        private CanvasManager CM;
		private bool planeIsDetected = true;

        private void Awake()
        {
            CM = canvasManager;

            AddListeners();
            
            CM.CNV_Scannig.SetActive(false);
            CM.CNV_PlaceAquarium.SetActive(false);
            CM.CNV_Buttons.SetActive(false);

            pointsCloudGenerator.ContainerVisibility(true);
            unityARGeneratePlane.ContainerVisibility(false);

            container.SetActive(false);
        }

        private void AddListeners()
        {
            CM.CNV_Scannig.GetComponent<CanvasScanningObjects>().ShowWarningTimesUpEvent += ShowWarningCanvas;
			placeAquarium.SendHitEvent += HideCanvasPlaceAquarium;
            UnityARUtility.SurfaceDetectedEvent += ShowCanvasPlaceAquarium;
        }

        void OnDestroy()
        {
            CM.CNV_Scannig.GetComponent<CanvasScanningObjects>().ShowWarningTimesUpEvent -= ShowWarningCanvas;
			placeAquarium.SendHitEvent -= HideCanvasPlaceAquarium;
            UnityARUtility.SurfaceDetectedEvent -= ShowCanvasPlaceAquarium;
        }

        public void ShowScanningSurfaces()
        {
#if UNITY_EDITOR
            CM.CNV_Scannig.SetActive(false);
            CM.CNV_PlaceAquarium.SetActive(true);
            return;
#endif
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

            if (GameState.isFirstUseApp)
                ScanningSurfaces();
            else
                HideCanvasPlaceAquarium();
        }

        private void ScanningSurfaces()
        {
            mainCamera.GetComponent<BloomOptimized>().enabled = false;
            container.SetActive(false);

            CM.CNV_Scannig.SetActive(true);
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
			ShowCanvasPlaceAquarium();
        }

        private void HideWarningCanvas()
        {
            CM.CNV_Scannig.SetActive(true);
            pointsCloudGenerator.ContainerVisibility(true);
        }

        public void ShowCanvasPlaceAquarium()
        {
            if (GameState.isInScene == GameState.Scene.Scanning)
            {
                CM.CNV_Scannig.SetActive(false);
                CM.CNV_PlaceAquarium.SetActive(true);
                placeAquarium.canvasPlaceAquariumIsOn = true;

                pointsCloudGenerator.ContainerVisibility(false);
                unityARGeneratePlane.ContainerVisibility(true);
            }
        }

		public void HideCanvasPlaceAquarium()
        {
            GameState.isFirstUseApp = false;

            mainCamera.GetComponent<SetCameraEffects>().SetEffects();

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
            CM.GoToSelectedScreen(TypeOfAction.CloseARCanvas);
        }
    }
}