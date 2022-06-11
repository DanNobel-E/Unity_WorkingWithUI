using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomMessageTarget : MonoBehaviour, ICustomMessageTarget
{
    //These methods will be invoked by CustomMessageTargetTrigger
    public void Message1()
    {
        Debug.Log(name + " Message 1 received");
    }

    public void Message2(BaseEventData inData)
    {
        //We know that BaseEventData is a CustomEventData
        string inDataString = ((CustomEventData)inData).m_CustomData;
        Debug.Log(name + "Message 2 received with inData: " + inDataString);
    }
}
