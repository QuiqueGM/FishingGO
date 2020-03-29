using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFG.Core;

namespace VFG.LevelEditor
{
    public class MenuItems : MonoBehaviour
    {
        private TextMesh TXT_GroupOfItems;
        private GameObject itemsContainer;
        private Object[] items;
        private GameObject item;

        private void Awake()
        {
            TXT_GroupOfItems = transform.Find("TXT_GroupOfItems").GetComponent<TextMesh>();
            gameObject.SetActive(false);
        }

        public void Populate()
        {
            CreateContainer();

            TXT_GroupOfItems.text = EditorState.groupOfItemsToShow;

            string path = string.Format("Prefabs/Items/{0}", EditorState.GetFolderFromTypeOfItem((TypeOfItem)System.Enum.Parse(typeof(TypeOfItem), EditorState.groupOfItemsToShow)));
            items = Resources.LoadAll(path, typeof(GameObject));
            int numItems = items.Length;

            for (int n=0; n< itemsContainer.transform.childCount; n++)
            {
                itemsContainer.transform.GetChild(n).gameObject.SetActive(false);
                if (numItems > n) CreateItem(n);
            }

            gameObject.SetActive(true);
        }

        private void CreateContainer()
        {
            if (itemsContainer != null) DestroyImmediate(itemsContainer);

            itemsContainer = Instantiate(Resources.Load("Prefabs/ItemsContainer"), gameObject.transform) as GameObject;
            itemsContainer.transform.localPosition = Vector3.zero;
            itemsContainer.transform.localScale = Vector3.one;
        }

        private void CreateItem(int n)
        {
            item = Instantiate(items[n], itemsContainer.transform.GetChild(n)) as GameObject;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localScale = Vector3.one;
            item.gameObject.name = items[n].name;
            item.tag = GameState.ITEM_LIBRARY;
            item.AddComponent<RotateObject>();

            itemsContainer.transform.GetChild(n).gameObject.SetActive(true);
        }
    }
}