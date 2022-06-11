using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomActions : MonoBehaviour {
    //AttackVar needs 2 lines of code
    public delegate void AttackDelegate(int power, string attackMsg);
    public static AttackDelegate AttackVar;
    //AttackAction is the same of AttackVar, but condensed in 1 line of code
    //NB: Action objects return no values
    public static Action<int,string> AttackAction;

    public Camera Cam;
    public int Damage;
    public string AttackString;

    RectTransform rectT;

    private void Start()
    {
        //Take the rect transform of this component
        rectT = this.GetComponent<RectTransform>();
    }

    void Update ()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(rectT, Input.mousePosition, Cam))
            if (AttackAction != null)
                //AttackVar(Damage, AttackString);
                AttackAction(Damage, AttackString);
    }
}
