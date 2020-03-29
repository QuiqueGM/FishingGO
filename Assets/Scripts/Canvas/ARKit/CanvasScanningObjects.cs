using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace VFG.Canvas
{
	public delegate void ShowWarningTimesUp();

	public class CanvasScanningObjects : MonoBehaviour 
	{
		public event ShowWarningTimesUp ShowWarningTimesUpEvent;

		private Sequence sequence;

		public void OnEnable()
		{
			int TIMES_UP = 10;
			int n = 0;

			sequence = DOTween.Sequence();
			sequence.Append(DOTween.To(() => n, a => n = a, 1, TIMES_UP).OnComplete(OnComplete));
		}

		private void OnComplete()
		{
			ShowWarningTimesUpEvent();
		}

		private void OnDisable()
		{
			sequence.Kill ();
		}
	}
}