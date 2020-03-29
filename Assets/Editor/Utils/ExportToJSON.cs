#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using VFG.Core;

namespace VFG.Utils 
{
    public class ExportToJSON : MonoBehaviour
    {
        [MenuItem("Tools/Export object(s) to JSON %#w", false, 10)]
        static void WriteStringToJSON()
        {
            Console.Clear();
            GameObject[] tSelection = Selection.gameObjects;

            if (tSelection.Length > 0)
            {
                string str = "[";

                for (int n = 0; n < tSelection.Length; n++)
                {
                    string item = string.Format
                    (
                        "{{\"IdObjective\":\"{0}\",\"TypeOf\":\"{11}\",\"Transform\":[{{\"Position\":[{{\"x\":{1},\"y\":{2},\"z\":{3}}}],\"Rotation\":[{{\"x\":{4},\"y\":{5},\"z\":{6},\"w\":{7}}}],\"Scale\":[{{\"x\":{8},\"y\":{9},\"z\":{10}}}]}}]}}",
                        tSelection[n].name,
                        tSelection[n].transform.position.x,
                        tSelection[n].transform.position.y,
                        tSelection[n].transform.position.z,
                        tSelection[n].transform.rotation.x,
                        tSelection[n].transform.rotation.y,
                        tSelection[n].transform.rotation.z,
                        tSelection[n].transform.rotation.w,
                        tSelection[n].transform.localScale.x,
                        tSelection[n].transform.localScale.y,
                        tSelection[n].transform.localScale.z,
                        tSelection[n].GetComponent<ItemClassification>().typeOfItem
                    );

                    str = string.Format("{0}{1},", str, item);
                }

                str = string.Format("{0}]", str.Substring(0, str.Length - 1));
                Debug.Log(str);
            }
            else Debug.Log("<color=red>Nothing selected!!!</color> Please, select at least one object to export to JSON.");

        }
    }
}
#endif