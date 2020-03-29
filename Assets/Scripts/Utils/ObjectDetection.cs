using UnityEngine;
using VFG.Core;

namespace VFG.Utils
{
    public class ObjectDetection : MonoBehaviour
    {
        [HideInInspector]
        public bool isDetected = true;

        private Camera _camera;
        private Plane[] _planes;
        private Collider _collider;
        private RaycastHit _hit;
        private Vector3 _direction;
        private Ray _ray;
        private bool debug;
        private SphereCollider sp;

        void Awake()
        {
            debug = false;
            _camera = Camera.main;
            _collider = GetComponent<Collider>();

            //GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //Mesh mesh = gm.GetComponent<MeshFilter>().sharedMesh;
            //Destroy(gm);

            //gameObject.AddComponent<MeshFilter>();
            //gameObject.GetComponent<MeshFilter>().mesh = mesh;
            //gameObject.AddComponent<MeshRenderer>();
            sp = gameObject.AddComponent<SphereCollider>();

        }

        void FixedUpdate()
        {
            sp.radius = Vector3.Distance(transform.position, _camera.transform.position);

            _planes = GeometryUtility.CalculateFrustumPlanes(_camera);
            _direction = _camera.transform.position - transform.position;
            _ray = new Ray(transform.position, _direction);

            if (Physics.Raycast(_ray, out _hit, 20))
            {
                if (debug) DebugDetection();
                isDetected = (GeometryUtility.TestPlanesAABB(_planes, _collider.bounds) && _hit.collider.gameObject.name == _camera.gameObject.tag) ? true : false;
                return;
            }

            isDetected = false;
        }

        private void DebugDetection()
        {
            Debug.DrawRay(transform.position, _direction, Color.magenta);
            Debug.Log("Planes: " + GeometryUtility.TestPlanesAABB(_planes, _collider.bounds));
            Debug.Log("Hit.Collider.Name: " + _hit.collider.gameObject.name);
            Debug.Log("Camera.tag: " + _camera.gameObject.tag);
            Debug.Log("Collider: " + (_hit.collider.gameObject.name == _camera.gameObject.tag));
            Debug.Log("Distance: " + Vector3.Distance(transform.position, _camera.transform.position));
        }
    }
}
