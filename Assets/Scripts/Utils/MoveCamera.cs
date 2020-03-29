using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    const float MOUSE_SENSIBILITY_X = 5.0f;
    const float MOUSE_SENSIBILITY_Y = 2.5f;

    private float mouseX = 0;
    private float mouseY = 0;

    Quaternion rotation = Quaternion.identity;


    void Start () {
	
	}


    void Update()
    {
        if (!UnityEngine.XR.XRSettings.enabled)
        {
            if (Input.GetMouseButton(0))
            {
                mouseX += Input.GetAxis("Mouse X") * MOUSE_SENSIBILITY_X;
                if (mouseX <= -180)
                {
                    mouseX += 360;
                }
                else if (mouseX > 180)
                {
                    mouseX -= 360;
                }

                mouseY -= Input.GetAxis("Mouse Y") * MOUSE_SENSIBILITY_Y;
                mouseY = Mathf.Clamp(mouseY, -89, 89);
            }


            gameObject.transform.eulerAngles = new Vector3(mouseY, mouseX, 0);

            rotation.eulerAngles = new Vector3(0, 0, -mouseX);
        }
    }
}
