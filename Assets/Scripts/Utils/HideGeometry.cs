using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGeometry : MonoBehaviour 
{
    public bool destroyGameObject = false;
    public float timeToDestroy = 5;

	void Awake () 
	{
        if (destroyGameObject) Destroy(gameObject,timeToDestroy);
        else
        {
            DestroyImmediate(gameObject.GetComponent<MeshRenderer>());
            DestroyImmediate(gameObject.GetComponent<MeshFilter>());
            Destroy(this);
        }
	}
}
