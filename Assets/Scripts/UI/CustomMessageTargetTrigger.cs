using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEventData : BaseEventData
{
    public string m_CustomData;

    public CustomEventData(EventSystem eventSystem, string customData): base(eventSystem)
    {
        m_CustomData = customData;
    }
}

public class CustomMessageTargetTrigger : MonoBehaviour
{
    [Tooltip("TrigMessage will deliver ICustomMessageTarget to TargetObject, and if it will find MBehaviour" +
        "components that implements ICustomMessageTarget interface, it will call:\n\t-HandleEvent method (and then Message2() method)" +
        "\n\t-Message1() Method")]
    public bool TrigMessage;
    [Tooltip("TrigMessageOnHierarchy will deliver ICustomMessageTarget to TargetObject, and if no MBehaviour" +
        "components that implements ICustomMessageTarget interface will be found, it will deliver the same message to its parent")]
    public bool TrigMessageOnHierarchy;
    public string TrigMessageData;
    public GameObject TargetObject;

    void Update()
    {
        if (TrigMessage)
        {
            TrigMessage = false;
            //If we don't use Lambda and want to carry a Custom data within the CustomMessage we are delivering...
            //NB: 'TargetObject' is a GameObject. It could have 0 to N 'target' components of type ICustomMessageTarget on which we want to invoke HandleEvent() method!
            ExecuteEvents.Execute<ICustomMessageTarget>(TargetObject, new CustomEventData(EventSystem.current, TrigMessageData), HandleEvent);
            //If we want to use Lambda, we need to specify 2 parameters (like HandleEvent method)
            ExecuteEvents.Execute<ICustomMessageTarget>(TargetObject, null, (target, eventData) => target.Message1());
        }
        if (TrigMessageOnHierarchy)
        {
            TrigMessageOnHierarchy = false;
            ExecuteEvents.ExecuteHierarchy<ICustomMessageTarget>(TargetObject, null, (x, y) => x.Message1());
        }
    }

    //If 'TargetObject' has N 'target' components of type ICustomMessageTarget, this method will be executed N times:
    //each time 'target' will be replaced by the i-th ICustomMessageTarget component found on 'TargetObject'!
    private void HandleEvent(ICustomMessageTarget target, BaseEventData eventData)
    {
        target.Message2(eventData);
    }
}
