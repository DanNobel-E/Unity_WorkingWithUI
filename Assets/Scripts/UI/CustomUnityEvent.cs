using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;

[System.Serializable]
public class myUnityEvent : UnityEvent<string> { } //Empty class, just need to exist

public class CustomUnityEvent : MonoBehaviour {
    public bool addBoolPersistentListener;
    public bool addStringPersistentListener;
    public bool changeWithAStringPersistentListener;
    public bool removePersistentListener;
    public int removePListenerIndex = 0;
    public string DynamicStringParameter;
    //This is how you get the classic UnityEvent Inspector
    public myUnityEvent OnClickString;
    public UnityEvent OnClick; //If there are no parameters, use UnityEvent type

    int persistentListenerToChangeIndex = 0;

    UnityAction<string> UActionString;
    UnityAction<string> UActionString2;
    UnityAction<bool> UActionBool;

    public void StringMethod(string inString)
    {
        Debug.Log("StringMethod: inString is " + inString);
    }
    public void StringMethod2(string inString)
    {
        Debug.Log("StringMethod2: inString is " + inString);
    }
    public void BoolMethod(bool inBool)
    {
        Debug.Log("BoolMeethod: inBool is " + inBool);
    }

    //NB: OnRightClickString() UnityEvent in the inspector is linked to:
    //  - Static version of printString(): textToPrint will be "staticName"
    //  - Dynamic version of printString(): textToPrint will be  DynamicStringParameter!
    public void printString(string textToPrint)
    {
        Debug.Log("printString - " + textToPrint);
    }

    void Start () {
        UActionString   = StringMethod;
        UActionString2  = StringMethod2;
        UActionBool     = BoolMethod;
    }

	void Update(){
        if (Input.GetMouseButton(0))
        {
            //This will call:
            //  printString("staticName")
            //  printString(DynamicStringParameter)
            OnClickString.Invoke(DynamicStringParameter);
        }
        if (Input.GetMouseButton(1))
        {
            //We can get and list info about the PERSISTENT listener registered to this UnityEvent
            if (OnClickString.GetPersistentEventCount() > 0)
            {
                Debug.Log("I will invoke these persistent methods:");
                for (int i = 0; i < OnClickString.GetPersistentEventCount(); i++)
                {
                    Debug.Log(i + " - " + OnClickString.GetPersistentMethodName(i));
                    //We can disable this Persistent listener
                    //OnRightClickString.SetPersistentListenerState(i, UnityEventCallState.Off);
                }
            }
        }

        if (addBoolPersistentListener)
        {
            addBoolPersistentListener = false;
            UnityEventTools.AddBoolPersistentListener(OnClickString, UActionBool, true);
        }
        if (addStringPersistentListener)
        {
            addStringPersistentListener = false;
            UnityEventTools.AddStringPersistentListener(OnClickString, UActionString, "stringParameter");
        }
        if (changeWithAStringPersistentListener)
        {
            changeWithAStringPersistentListener = false;
            UnityEventTools.RegisterStringPersistentListener(OnClickString, persistentListenerToChangeIndex++,
                UActionString2, "stringParameter2");

        }
        if (removePersistentListener)
        {
            removePersistentListener = false;
            UnityEventTools.RemovePersistentListener(OnClickString, removePListenerIndex);

        }


    }


}
