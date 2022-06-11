using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;

[System.Serializable]
public class UEventString : UnityEvent<string> { } //Empty class, just need to exist
[System.Serializable]
public class UEventInt : UnityEvent<int> { } //Empty class, just need to exist

public class ColliderTriggerEvent : MonoBehaviour {
    public UEventString OnTriggerEventString;
    public UEventInt OnTriggerEventInt;
    public UnityEvent OnTriggerEvent; //If there are no parameters, use UnityEvent type

    public string StringArgument;
    public int IntArgument;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEventString.Invoke(StringArgument);
        OnTriggerEventInt.Invoke(IntArgument);
        OnTriggerEvent.Invoke();
    }


}
