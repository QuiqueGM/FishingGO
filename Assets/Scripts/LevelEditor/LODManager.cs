using UnityEngine;

public class LODManager : MonoBehaviour
{
    private LODGroup lodGroup;
    private Transform lodTransform;

    void Start()
    {
        lodGroup = GetComponent<LODGroup>();
        lodTransform = lodGroup.transform;
    }

    public void SetMaterial(Material material)
    {
        for (int n = 0; n < gameObject.transform.childCount; n++)
            gameObject.transform.GetChild(n).GetComponent<Renderer>().material = material;
    }
}