using UnityEngine;

namespace VFG.Utils
{
    public class ArrowCompass : MonoBehaviour
    {
        public Transform reference;
        private GameObject objective;

        void Awake()
        {
            gameObject.SetActive(false);
        }

        void Update()
        {
            gameObject.transform.position = reference.transform.position;

            if (objective != null)
            {
                Vector3 targetPostition = new Vector3(objective.transform.position.x, transform.position.y, objective.transform.position.z);
				transform.LookAt(objective.transform);
            }
        }

        public void SetObjectivePosition(GameObject objective)
        {
            this.objective = objective;
        }
    }
}