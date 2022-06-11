using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;

[RequireComponent(typeof(Button))]
public class BtnUnityEventSubscriber : MonoBehaviour {
	public Button ExternalButton;
    public bool RmvAllNonPListOnButton; //Remove all non persistent listener on button
    Button btn;

    void Start() {
        btn = GetComponent<Button>();

        //This will remove ONLY the NON-Persistent methods (those added at runtime)
        btn.onClick.RemoveAllListeners();
        if(ExternalButton == null)
            //Same as:
            //button.onClick.AddListener (()=>onClickCustomListener());
            btn.onClick.AddListener(onClickCustomListener);
        else
            btn.onClick.AddListener(() => onClickCustomListenerWSender(ExternalButton));
        //We need this Lambda expression to link an empty callback to our method onClickCustomListenerWSender
        //	which is waiting for the button we are clicking on.
    }

    private void Update()
    {
        //This will remove ONLY the NON-Persistent methods (those added at runtime)
        if (RmvAllNonPListOnButton)
        {
            RmvAllNonPListOnButton = false;
            btn.onClick.RemoveAllListeners ();
        }
    }

    void onClickCustomListener()
	{
		Debug.Log (gameObject.name + ": onClickCustomListener triggered!");
	}

	public void onClickCustomListenerWSender(Button button)
	{
        button.transform.position += new Vector3 (10, 0, 0); 
		Debug.Log ("onClickCustomListenerWSender triggered. Button name: "+ button.name);
	}

}
