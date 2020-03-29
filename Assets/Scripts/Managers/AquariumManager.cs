using System.Collections.Generic;
using UnityEngine;

namespace VFG.Managers
{
    public class AquariumManager : MonoBehaviour
    {
        public GameObject objectives;
        private List<GameObject> list = new List<GameObject>();

        public void Init()
        {
            list.Clear();

            foreach (Transform o in objectives.transform)
                list.Add(o.gameObject);

            HideAllObjectives();
        }

        public void HideAllObjectives()
        {
            foreach (Transform o in objectives.transform)
                o.gameObject.SetActive(false);
        }

        public void ActiveObjective(string name)
        {
            HideAllObjectives();
            list.Find(n => n.gameObject.name == name).SetActive(true);
        }

        public GameObject GetTransform(string name)
        {
            return list.Find(n => n.gameObject.name == name).gameObject;
        }
    }
}