using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;

public class ColliderTriggerEventListener : MonoBehaviour {
    public void PrintStringListener(string msg)
    {
        Debug.Log("You entered the trigger! This is your message from the Event invoker: " + msg);
    }
    public void PrintIntListener(int val)
    {
        Debug.Log("You entered the trigger! This is your value from the Event invoker: " + val);
    }
    public void PrintListener()
    {
        Debug.Log("You entered the trigger!");
    }


}
