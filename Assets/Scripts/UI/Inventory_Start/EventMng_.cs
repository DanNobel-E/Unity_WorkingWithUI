using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEditor.Events;

[Serializable]
public class ItemPicked : UnityEvent<Item_, GameObject>
{
    public void Subscribe(UnityAction<Item_, GameObject> onItemPicked, bool b)
    {
        if (b)
            UnityEventTools.AddPersistentListener(EventMng_.ItemPicked, onItemPicked);
        else
            UnityEventTools.RemovePersistentListener(EventMng_.ItemPicked, onItemPicked);

    }
}

[Serializable]
public class ItemRemoved : UnityEvent<Item_,bool>
{
    public void Subscribe(UnityAction<Item_, bool> onItemRemovedCallback, bool b)
    {
        if (b)
            UnityEventTools.AddPersistentListener(EventMng_.ItemRemoved, onItemRemovedCallback);
        else
            UnityEventTools.RemovePersistentListener(EventMng_.ItemRemoved, onItemRemovedCallback);
    }
}
//Declare 2 UnityEvents
//  - ItemPicked, 2 parameters: Item obj to pick up and 3D GameObject  on the floor to Destroy
//  - ItemRemoved, 1 parameter: Item obj to remove from the Inventory
// E.g. [System.Serializable] public class UnityEventType : UnityEvent<T1, T2, T3, T4> { }



public class EventMng_ : MonoBehaviour {
    //Create 2 public static instances of the 2 Unity Events
    public static ItemPicked ItemPicked= new ItemPicked();
    public static ItemRemoved ItemRemoved= new ItemRemoved();
   

    public static void OnItemRemoved(Item_ i, bool b)
    {
        ItemRemoved.Invoke(i, b);
    }

    public static void OnItemPicked(Item_ i, GameObject go)
    {
        ItemPicked.Invoke(i, go);
    }
}
