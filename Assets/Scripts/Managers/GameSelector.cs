using UnityEngine;
using UnityEngine.XR.iOS;

namespace VFG.Managers
{
	public class GameSelector : MonoBehaviour 
	{
		public GameObject camera;
		public GameObject ARmodels;
		public GameObject ARToolKit;
		public GameObject touchScreenToPlaceAquariumButton;

		void Awake () 
		{
#if UNITY_EDITOR

            camera.GetComponent<UnityARVideo> ().enabled = false;
			camera.GetComponent<UnityARCameraNearFar> ().enabled = false;
			camera.GetComponent<Camera> ().clearFlags = CameraClearFlags.Skybox;

			ARmodels.GetComponent<PlaceAquarium> ().enabled = false;
			ARToolKit.SetActive (false);

			touchScreenToPlaceAquariumButton.SetActive (true);
#endif
            Destroy(gameObject);
        }
    }
}