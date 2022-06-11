using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionListenerTest : MonoBehaviour {

    void OnEnable () {
        //When PointerLeftClick Action will be triggered, we got damaged
        CustomActions.AttackAction += Damage;
        //CustomActions.AttackVar += Damage;
        
        //NB: Being CustomActions.AttackAction similar to delegate, you CAN:
        //  - reset the subscribers
        //CustomActions.AttackAction = Damage;
        //  - call the Action outside its declaration class
        //CustomActions.AttackAction(10, "AttackTestMsg");
    }

    void OnDisable () {
        //Good Rule: whenever you subscribe a method, you must also unsubscribe
        CustomActions.AttackAction -= Damage;
    }

    //Listeners need to match the signature of our Action
    public void Damage(int power, string attackMsg){
		Debug.Log ("Damage LISTENER - I got a damage of " + power + " with a message: '" + attackMsg + "'");
	}
}
