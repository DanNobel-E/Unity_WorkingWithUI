using UnityEngine.EventSystems;

public interface ICustomMessageTarget : IEventSystemHandler
{
    // These methods can be called via the messaging system
    void Message1();
    void Message2(BaseEventData inData);
}