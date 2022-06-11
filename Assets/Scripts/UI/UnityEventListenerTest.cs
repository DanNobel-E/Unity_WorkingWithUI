using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventListenerTest : MonoBehaviour
{
    public CustomUnityEvent CustomUnityEventReference;
    void Start()
    {
        UnityAction<string> UActionString = (name) => { };
        CustomUnityEventReference.OnClickString.AddListener(UActionString);
        //We can Remove all non-pesistent listener from another class
        CustomUnityEventReference.OnClickString.RemoveAllListeners();
        //We can disable a pesistent listener from another class
        CustomUnityEventReference.OnClickString.SetPersistentListenerState(0, UnityEventCallState.Off);
        //We can invoke the Unity event from another class
        CustomUnityEventReference.OnClickString.Invoke("a");
    }
}
