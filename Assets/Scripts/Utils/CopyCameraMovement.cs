using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFG.Utils
{
    public class CopyCameraMovement : MonoBehaviour
    {
        public Camera mainCamera;
        private Camera cam;

        private void Start()
        {
            cam = GetComponent<Camera>();
            cam.fov = mainCamera.fov;
        }

        private void Update()
        {
            cam.transform.position = mainCamera.transform.position;
            cam.transform.rotation = mainCamera.transform.rotation;
        }
    }
}