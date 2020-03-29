using UnityEngine;
using System.Collections;

namespace VRMiddlewareController
{
    public class LaserPointer : MonoBehaviour
    {
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;

        [HideInInspector] public GameObject objectInFocus;
        [HideInInspector] public Transform reference;

        public Color color;
        public float thickness = 0.002f;
        public float maxDistance = 100;
        public LayerMask layerMask;

        protected bool isEnabled = true;
        protected GameObject holder;

        private bool _isActive = false;
        GameObject _pointer;
        Transform _previousContact = null;
        RaycastHit _hit;
        bool _bHit;
        float dist;

        void Start()
        {
            CreatePointer();
        }

        private void CreatePointer()
        {
            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.name = "Holder";

            _pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _pointer.transform.parent = holder.transform;
            _pointer.transform.localScale = new Vector3(thickness, thickness, maxDistance);
            _pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            Destroy(_pointer.GetComponent<Collider>());

            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            _pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        public virtual void Update()
        {
            if (!_isActive)
            {
                _isActive = true;
                holder.SetActive(true);
            }

            Ray raycast = new Ray(transform.position, transform.forward);
            _bHit = Physics.Raycast(raycast, out _hit, maxDistance, layerMask);

            GetPointerIn();
            GetPointerOut();

            if (!_bHit)
                _previousContact = null;

            dist = maxDistance;

            if (_bHit && _hit.distance < dist)
                dist = _hit.distance;

            _pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            _pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }

        public void GetPointerIn()
        {
            if (_previousContact && _previousContact != _hit.transform)
            {
                PointerArguments args = new PointerArguments();

                args.distance = 0f;
                args.target = _previousContact;
                OnPointerOut(args);
                objectInFocus = null;
                _previousContact = null;
            }
        }

        public void GetPointerOut()
        {
            if (_bHit && _previousContact != _hit.transform)
            {
                PointerArguments argsIn = new PointerArguments();

                argsIn.distance = _hit.distance;
                argsIn.target = _hit.transform;
                objectInFocus = _hit.transform.gameObject;
                OnPointerIn(argsIn);
                _previousContact = _hit.transform;
            }
        }

        public virtual void OnPointerIn(PointerArguments e)
        {
            if (PointerIn != null)
                PointerIn(e);
        }

        public virtual void OnPointerOut(PointerArguments e)
        {
            if (PointerOut != null)
                PointerOut(e);
        }
    }
}