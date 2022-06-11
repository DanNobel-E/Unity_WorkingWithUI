using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFade : MonoBehaviour {
	public CanvasGroup cGroup;
	public float startFadeDistance;
	public float endFadeDistance;

	void Update () {
		float d = (transform.position - Camera.main.transform.position).magnitude;
		float canvasAlpha;
		if (d > endFadeDistance)
			canvasAlpha = 0;
		else if(d < startFadeDistance)
			canvasAlpha = 1;
		else
			canvasAlpha = (endFadeDistance-d)/(endFadeDistance-startFadeDistance);

		cGroup.alpha = canvasAlpha;
	}
}
