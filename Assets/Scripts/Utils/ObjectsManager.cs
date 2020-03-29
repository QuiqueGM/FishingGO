using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VFG.Utils
{
    public class ObjectsManager : MonoBehaviour
    {
        private List<GameObject> animals = new List<GameObject>();
        private List<GameObject> newAnimals = new List<GameObject>();

        private void Start()
        {
            for (int n = 0; n < transform.childCount; n++)
                animals.Add(transform.GetChild(n).gameObject);
        }

        public void ActiveObjects()
        {
            if (animals.Count <= 1) return;

            ShowAllObjects(false);

            int numObj = Random.Range(1, animals.Count);

            if (numObj == (animals.Count - 1))
                ShowAllObjects(true);
            else
            {
                newAnimals.Clear();
                List<GameObject> animalsAUX = animals.ToList();

                for (int n = 0; n < numObj; n++)
                {
                    int a = Random.Range(0, animalsAUX.Count);
                    newAnimals.Add(animals[a]);
                    animalsAUX.RemoveAt(a);
                }

                ShowAllObjects(newAnimals);
            }
        }

        private void ShowAllObjects(bool state)
        {
            foreach (GameObject a in animals)
                a.SetActive(state);
        }

        private void ShowAllObjects(List<GameObject> animals)
        {
            foreach (GameObject a in animals)
                a.SetActive(true);
        }
    }
}