#if UNITY_EDITOR
using System.Reflection;

namespace VFG.Utils
{
    public class Console
    {
        public static void Clear()
        {
            var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
            var clearMethod = logEntries.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }
    }
}
#endif