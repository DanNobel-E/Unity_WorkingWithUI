using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class LoadingBarExStart : MonoBehaviour {
	public Image loadingBar; 	//Filled img
	public TMP_Text feedbackText;	
	public TMP_Text loadValText;	//Load percentage value
	public Image overlayPanel;	//Full screen img

	float fillVal;		//Loading bar fill
	float overlayAlpha; //white panel in overlay
	Vector2 barStartWidth;
	Vector2 barEndWidth;
	//Increment values (Loading bar, overlay white panel alpha fade in)
	public float loadingIncVal=0.1f;
	public float alphaIncVal=0.1f;

	//feedbackText == startText at the beginning, endText after loading == 100
	public string startText;
	public string endText;

	void Start()
	{
		feedbackText.text = startText;
		barEndWidth = loadingBar.rectTransform.sizeDelta;
		barStartWidth= new Vector2(0, loadingBar.rectTransform.sizeDelta.y);
		loadingBar.rectTransform.sizeDelta = barStartWidth;
		fillVal = 0;

		loadValText.text = ((int)fillVal).ToString("D3")+"/100";

	}

	void Update () 
	{

		if (fillVal == 100)
		{
			if (overlayPanel.color.a == 1)
            {
				UnityEditor.EditorApplication.isPlaying = false;
				return;
            }

			float alphaVal = overlayPanel.color.a;
			alphaVal += alphaIncVal;

			if (alphaVal > 1)
				alphaVal = 1;

			Color c = new Color(1,1,1, alphaVal);

			overlayPanel.color = c;
		}


		if (Input.GetKey(KeyCode.Space))
        {
            if (fillVal == 100)
            {
				feedbackText.text=endText;
				return;
            }

			fillVal += loadingIncVal;

			if (fillVal > 100)
				fillVal = 100;


			float fraction = (barEndWidth.x * fillVal) / 100;
			loadingBar.rectTransform.sizeDelta = Vector2.Lerp(barStartWidth, barEndWidth, fraction/ barEndWidth.x);

			loadValText.text = ((int)fillVal).ToString("D3") + "/100";

        }
        else
        {
			if (fillVal == 0 || fillVal==100)
				return;

			fillVal -= loadingIncVal;

			if (fillVal < 0)
				fillVal = 0;

			float fraction = (barEndWidth.x * fillVal) / 100;
			loadingBar.rectTransform.sizeDelta = Vector2.Lerp(barEndWidth, barStartWidth,1- (fraction / barEndWidth.x));

			
			loadValText.text = ((int)fillVal).ToString("D3") + "/100";

		}


       
	}
}
