using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;	//BaseEventData, for OnEnter_Dynamic()

public class ButtonTest : MonoBehaviour {

	public void OnClick(){
		Debug.Log ("OnClick event!");
	}

	public void OnEnter(){
		Debug.Log ("OnEnter event!");
	}

	public void OnExit(){
		Debug.Log ("OnExit event!");
	}

   public void OnEnter_Dynamic(BaseEventData bed){
		Debug.Log ("OnEnter event bed! "+ ((PointerEventData)bed).position);
	}
}
