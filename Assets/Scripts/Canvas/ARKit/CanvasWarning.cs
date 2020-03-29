using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VFG.Canvas
{
	public delegate void HideWarning ();

	public class CanvasWarning : MonoBehaviour 
	{
		public event HideWarning HideWarningEvent;

		void Awake () 
		{
			transform.GetComponent<Button> ().onClick.AddListener (() => HideCanvas());
		}
		
		void HideCanvas() 
		{
			HideWarningEvent ();
		}
	}
}