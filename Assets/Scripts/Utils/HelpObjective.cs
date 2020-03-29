using UnityEngine;

namespace VFG.Utils
{
    public class HelpObjective : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private GameObject _objective;
        public Transform _initPoint;

        private float _distance;

        void Awake()
        {
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            gameObject.SetActive(false);
        }

        void Update()
        {
            _lineRenderer.SetPosition(0, _initPoint.transform.position);
            _lineRenderer.SetPosition(1, _objective.transform.position);
        }

        public void SetObjectivePosition(GameObject objective)
        {
            _objective = objective;
        }
    }
}