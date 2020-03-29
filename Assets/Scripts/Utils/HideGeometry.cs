using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGeometry : MonoBehaviour 
{
	void Awake () 
	{
		DestroyImmediate (gameObject.GetComponent<MeshRenderer> ());
		DestroyImmediate (gameObject.GetComponent<MeshFilter> ());
		Destroy (this);
	}
}
