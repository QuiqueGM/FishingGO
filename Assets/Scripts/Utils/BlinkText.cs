using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace VFG.Utils
{
	public class BlinkText : MonoBehaviour 
	{
		public float linkSpeed;

		void Update ()
		{
			gameObject.GetComponent<TMP_Text>().color = new Color (
				gameObject.GetComponent<TMP_Text>().color.r,
				gameObject.GetComponent<TMP_Text>().color.g,
				gameObject.GetComponent<TMP_Text>().color.b, 
	           	Mathf.Round(Mathf.PingPong(Time.unscaledTime * linkSpeed, 1)));
		}
	}
}
