using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Remap : MonoBehaviour
{
    public RectTransform Spot;
    public RectTransform Map;
    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }


    public void ScrollX(float val)
    {

        float xPos = MathUtils.Remap(val, slider.minValue, slider.maxValue, 0, Map.sizeDelta.x);
        

        Spot.anchoredPosition = new Vector2(xPos, Spot.anchoredPosition.y);

    }

    public void ScrollY(float val)
    {


        float yPos = MathUtils.Remap(val, slider.minValue, slider.maxValue, 0, Map.sizeDelta.y);


        Spot.anchoredPosition = new Vector2(Spot.anchoredPosition.x, yPos);
    }
}
