using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TextOps : MonoBehaviour {
	public string textToDisplay;
	public Text textUI;

	void Start () {
		textUI = GetComponent<Text> ();
	}
	
	void Update () {
		textUI.text = textToDisplay;
	}
}
