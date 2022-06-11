using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour {
	public Slider sliderElement;

	void Start(){
		sliderElement.minValue = 0;
		sliderElement.maxValue = 10;
		sliderElement.wholeNumbers = true;
		sliderElement.value = 5;
	}

    public void OnValueChanged()
    {
        Debug.Log("New slider val is: " + sliderElement.value);
    }
    public void OnValueChanged(float newVal)
    {
        Debug.Log("New slider val is: " + newVal);
    }
}
