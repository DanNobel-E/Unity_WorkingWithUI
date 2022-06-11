using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEditor.Events;
using TMPro;



//[ExecuteInEditMode]
public class UIGraphValuesProviderStart : MonoBehaviour
{
    public UIGraphStart UIGraphStartObj;
    public TextMeshProUGUI Healthy, Sick, Recovered;
    public bool UpdateValues;
    public float[] Values= new float[500];
    public float StepTime = 0.5f;
    public static int InfectCounter;
    public static int RecoveredCounter;
    float timer;
    int currIndex;
    int n;
    public void Init(int n)
    {
        this.n = n;
        UIGraphStartObj.Init();
        UIGraphStartObj.SetxStep(Values.Length * StepTime);
        Values[currIndex] = 0;
        currIndex++;
        Healthy.text = (n-InfectCounter-RecoveredCounter).ToString();
        Sick.text = InfectCounter.ToString();
        Recovered.text = RecoveredCounter.ToString();

    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= StepTime)
        {
            Values[currIndex] = InfectCounter;
            Healthy.text = (n - InfectCounter-RecoveredCounter).ToString();
            Sick.text = InfectCounter.ToString();
            Recovered.text = RecoveredCounter.ToString();
            currIndex++;
            timer = 0;
            UpdateValues = true;
        }

        if (UpdateValues)
        {
            UpdateValues = false;
            UIGraphStartObj.UpdateGraphValues(Values);
        }
    }

  
}
