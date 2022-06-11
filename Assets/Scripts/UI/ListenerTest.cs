using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerTest : MonoBehaviour {
	public GameObject GOWithEvents;
	CustomEvents customEvents;

    void OnEnable () {
		customEvents = GOWithEvents.GetComponent<CustomEvents> ();
		//When PointerLeftClick Event will be triggered, 2 Listeners will be called
		customEvents.PointerInside += PointerInsideListener;
		customEvents.PointerInside += PointerInsideListener2;

        //--- WARNING - Difference between Delegates and Events ---
        // * Events cannot be invoked outside their class.
        //      - The following Invoke() is incorrect:
        //          customEvents.PointerInside.Invoke()
        //      - While we get NO ERROR if we call Invoke() on a public delegate variable:
        //          customEvents.DelegateVar.Invoke(null, null);
        // * Events adds a layer of abstraction and protection on delegate, this protection prevents
        //          client of the delegate from resetting the delegate and invocation list.
        //      - The following line is incorrect:
        //          customEvents.PointerInside = PointerInsideListener2;
        //      - While we get NO ERROR if we call:
        //          customEvents.DelegateVar = PointerInsideListener2;
    }

    void OnDisable () {
		//Good Rule: whenever you subscribe a method to an event, you must also have a corresponding unsubscribe
		customEvents.PointerInside -= PointerInsideListener;
		customEvents.PointerInside -= PointerInsideListener2;
	}

    //Listeners need to match the signature of our Event delegate
    public void PointerInsideListener(GameObject sender, string eventMessage){
		Debug.Log ("PointerInsideListener LISTENER - " + sender.name + " SENT an Event with this msg: '" + eventMessage + "', and " + this.name + " used a listener to receive the event");
	}
	public void PointerInsideListener2(GameObject sender, string eventMessage){
		Debug.Log ("PointerInsideListener2 LISTENER - " + sender.name + " SENT an Event with this msg: '" + eventMessage + "', and " + this.name + " used a listener to receive the event");
	}
}
