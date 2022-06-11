using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImageTest : MonoBehaviour {
	public Image img;
	public Sprite sprite;
	[Range (0,1)]
	public float fillVal;

	void Start () {
		img.sprite = sprite;
		img.color = new Color(1,1,1,1);
		img.type = Image.Type.Filled;
		img.fillMethod = Image.FillMethod.Radial360;
	}
	
	void Update () {
		img.fillAmount = fillVal;
	}
}
