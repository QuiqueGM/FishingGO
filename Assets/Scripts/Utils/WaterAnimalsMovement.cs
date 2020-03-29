using System.Collections.Generic;
using UnityEngine;

namespace VFG.Utils
{
    public class WaterAnimalsMovement : MonoBehaviour
    {
        const float SAFE_DISTANCE = 1.5f;

        public GameObject reference;
        [Tooltip("Speed in m/s")]
        public float speed;
        [Range(0.0f, 1.0f)]
        [Tooltip("Odds of appearance")]
        public float appearance = 0.75f;

        private List<Transform> points = new List<Transform>();
        private int firstPoint, secondPoint;
        private Vector3 pointA, pointB;
        private float dist;
        private float maxDistance;

        void Start()
        {
            for (int n = 0; n < transform.childCount; n++)
                points.Add(transform.GetChild(n).transform);

            maxDistance = Vector3.Distance(points[0].transform.position, points[Mathf.RoundToInt(points.Count/2)].transform.position);
            GetPoints();
        }

        void Update()
        {
            MoveReference();
        }

        private void MoveReference()
        {
            if (dist > SAFE_DISTANCE)
            {
                reference.transform.Translate(Vector3.forward * Time.deltaTime * speed);
                dist = Vector3.Distance(reference.transform.position, pointB);
            }
            else
            {
                GetPoints();
                reference.GetComponent<ObjectsManager>().ActiveObjects();
            }
        }

        private void GetPoints()
        {
            firstPoint = GetPoint();
            secondPoint = GetSecondPoint(firstPoint);
            pointA = points[firstPoint].position;
            pointB = points[secondPoint].position;
            dist = Vector3.Distance(pointA, pointB);

            reference.transform.position = pointA;
            reference.transform.LookAt(points[secondPoint]);
        }

        private int GetPoint()
        {
            return Random.Range(0, points.Count);
        }

        private int GetSecondPoint(int firstPoint)
        {
            int secondPoint;

            do
            {
                secondPoint = GetPoint();
            }
            while ((maxDistance*appearance) > Vector3.Distance(points[firstPoint].position, points[secondPoint].position));

            return secondPoint;
        }
    }
}