using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFG.LevelEditor
{
    [System.Serializable]
    public class ItemListStruct
    {
        public GameObject go;
        public Material ma;
    }

    public class DebugListObjects : MonoBehaviour
    {
        public List<ItemListStruct> itemSelected;
        public List<ItemListStruct> currentItems;
        public List<ItemListStruct> freezedItems;
    }
}
