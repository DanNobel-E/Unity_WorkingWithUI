using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrossFade : MonoBehaviour
{
    public bool CrossFadeAlpha;
    public float EndAlpha, AlphaCrossDuration;
    public bool CrossFadeColor;
    public Color EndColor;
    public float ColorCrossDuration;
    public Graphic GraphicImg;
    public Graphic GraphicButton;

    void Update()
    {
        if (CrossFadeAlpha)
        {
            CrossFadeAlpha = false;
            GraphicImg.CrossFadeAlpha(EndAlpha, AlphaCrossDuration, true);
        }
        if (CrossFadeColor)
        {
            CrossFadeColor = false;
            GraphicButton.CrossFadeColor(EndColor, ColorCrossDuration, true, true);
        }

    }
}
