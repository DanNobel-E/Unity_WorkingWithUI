using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DebugMessageTest : MonoBehaviour
{
    public bool SendFakeOnClick;
    public GameObject ButtonGObj;

    void Update()
    {
        if (SendFakeOnClick)
        {
            SendFakeOnClick = false;
            PointerEventData PEData = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute<IPointerClickHandler>(ButtonGObj, null, (target, eventData) => target.OnPointerClick(PEData));
        }
    }
}
