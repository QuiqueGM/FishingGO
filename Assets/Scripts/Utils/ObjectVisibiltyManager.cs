using UnityEngine;

public class ObjectVisibiltyManager : MonoBehaviour
{
    public VisibityCheckbox[] obj;

    void Awake()
    {
        for (int n = 0; n < obj.Length; n++)
        {
            if (obj[n].obj != null)
                obj[n].obj.SetActive(obj[n].isVisible ? true : false);
        }
    }
}