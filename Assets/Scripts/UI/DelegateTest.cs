using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTest : MonoBehaviour {

	//Delegate type
	public delegate void SampleDelegate();
	delegate void SampleDelegateWithInputs(int a, int b);
	//delegate void MulticastDelegateWithInputs(int a, int b);

	public SampleDelegate sampleDelegateVar;

	//Member function
	void firstDelegateMethodExample(){
		Debug.Log ("firstDelegateExample Log output");
	}
	void secondDelegateMethodExample(){
		Debug.Log ("secondDelegateExample Log output");
	}

	//
	void AddDelegateMethod(int a, int b){
		Debug.Log ("SUM (" + a + "," + b + ") = " + (a+b));
	}
	void SubtractDelegateMethod(int a, int b){
		Debug.Log ("SUB (" + a + "," + b + ") = " + (a-b));
	}

	void Start () {
		//Member variable
		sampleDelegateVar = firstDelegateMethodExample;
		//SampleDelegate sampleDelegateVar = secondDelegateMethodExample;
		//Every delegate variable with no method assigned is null. So this is a common check to do
		if(sampleDelegateVar != null)
			sampleDelegateVar.Invoke ();

		//
		SampleDelegateWithInputs sampleMethodWithInputsVar = AddDelegateMethod;
		sampleMethodWithInputsVar.Invoke (1,2);
		sampleMethodWithInputsVar = SubtractDelegateMethod;
		sampleMethodWithInputsVar.Invoke (1,2);

		//Multicasting
		SampleDelegateWithInputs multicastMethodWithInputsVar = AddDelegateMethod;
        
        //If we add another Add method, we'll call AddDelegateMethod twice
        //multicastMethodWithInputsVar += AddDelegateMethod;
        multicastMethodWithInputsVar += SubtractDelegateMethod;
        
        //Using Lambda expression, we can add an anonymous method (a,b) and then call other methods that would be impossible to assign to multicastMethodWithInputsVar,
        //because of their different signature. The type of a,b is inferred by the compiler.
        //multicastMethodWithInputsVar += (a, b) => printSomeInfo();

		//Same as .Invoke()
		multicastMethodWithInputsVar (3, 4);

		Debug.Log ("Removing Add, now the invoke should perform only Subtract:");
		multicastMethodWithInputsVar -= AddDelegateMethod;
		multicastMethodWithInputsVar (3, 4);
	}

    void printSomeInfo()
    {
        Debug.Log("printSomeInfo");
    }
}
