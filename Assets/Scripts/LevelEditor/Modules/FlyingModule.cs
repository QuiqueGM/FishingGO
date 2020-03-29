using UnityEngine;

namespace VFG.LevelEditor
{
    public class FlyingModule : MonoBehaviour
    {
        const float FACTOR_REDUCTION = 1000;

        [Range(0.1f, 1.0f)]
        public float factorAcceleration = 1.0f;
        public GameObject cameraSet;

        private float acceleration;
        
        private GameObject v1A, v1B;
        private GameObject v2A, v2B;
        private Vector3 init, end;

        public bool leftControllerIsPressed { get; set; }
        public bool rightControllerIsPressed { get; set; }

        public void CreateReferences(GameObject controller)
        {
            v1A = CreateReferences("v1A", Vector3.down * 5, controller, false);
            v1B = CreateReferences("v1B", Vector3.zero, controller, false);
            v2A = CreateReferences("v2A", Vector3.forward * 5, controller, true);
            v2B = CreateReferences("v2B", Vector3.zero, controller, true);

            v1A.transform.parent = v1B.transform;
        }

        public void Fly(GameObject controller)
        {
            if (!rightControllerIsPressed) return;

            cameraSet.transform.Translate((controller.transform.up) * -acceleration);
        }

        public void GetAcceleration(GameObject controller)
        {
            if (!leftControllerIsPressed) return;

            Debug.DrawLine(v1A.transform.position, v1B.transform.position, Color.red);
            Debug.DrawLine(v2A.transform.position, v2B.transform.position, Color.cyan);

            v1B.transform.eulerAngles = new Vector3(v1B.transform.eulerAngles.x, v2A.transform.eulerAngles.y, v1B.transform.eulerAngles.z);

            init = v1B.transform.position - v1A.transform.position;
            end = v2B.transform.position - v2A.transform.position;

            acceleration = GetAngle(init, end) * factorAcceleration / FACTOR_REDUCTION;
        }

        public void Stop()
        {
            rightControllerIsPressed = false;
            DestroyReferences();
        }

        float GetAngle(Vector3 init, Vector3 end)
        {
            float dot = Vector3.Dot(init, end);
            float mag1 = Vector3.Magnitude(init);
            float mag2 = Vector3.Magnitude(end);
            return (Mathf.Asin(dot / (mag1 * mag2)) * Mathf.Rad2Deg);
        }

        private GameObject CreateReferences(string name, Vector3 pos, GameObject parent, bool isParent)
        {
            GameObject reference;

            float FACTOR_SCALE = 0.05f;

            reference = GameObject.CreatePrimitive(PrimitiveType.Cube);
            reference.name = name;
            reference.transform.parent = parent.transform;
            reference.transform.localPosition = Vector3.zero;
            reference.transform.localPosition += pos * FACTOR_SCALE;
            reference.transform.localScale *= FACTOR_SCALE;

            if (!isParent)
                reference.transform.parent = null;

            return reference;
        }

        private void DestroyReferences()
        {
            Destroy(v1A);
            Destroy(v1B);
            Destroy(v2A);
            Destroy(v2B);
        }
    }
}