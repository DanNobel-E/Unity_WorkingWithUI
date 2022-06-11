using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public delegate void ourEventDelegate(GameObject sender, string eventMessage);

public class CustomEvents : MonoBehaviour {

	public event ourEventDelegate PointerInside; 	//This is an event of type ourEventDelegate
	public ourEventDelegate DelegateVar;			//This is a delegate of type ourEventDelegate - used by ListenerTest to call CustomEvents.delegateVar.Invoke()
	public Canvas RootCanvas;
    public Camera Cam;
    public bool UseIsPointerOverGameObject = false;

    RectTransform rectT;

    private void Start()
    {
        //Take the rect transform of this component
        rectT = this.GetComponent<RectTransform>();
    }
    void Update () {
        //Here we use 'IsPointerOverGameObject', this function returns True if the pointer is over an EventSystem object
        //  (Every UI Element)
        if (UseIsPointerOverGameObject)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                CallPointerInside();
        }
        else
        {
            if (RootCanvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                //Using RectTransformUtility.RectangleContainsScreenPoint() Mode: Since we are in ScreenOverlay, the last parameter (Cam) is null
                if (RectTransformUtility.RectangleContainsScreenPoint(rectT, Input.mousePosition, null))
                    CallPointerInside();
            }
		    else {
                //Screen Space Camera OR World Space
                //Find a way to know if we are on this UI gameObject or not
                if (RectTransformUtility.RectangleContainsScreenPoint(rectT, Input.mousePosition, Cam))
                        CallPointerInside();
            }
        }
    }
    void CallPointerInside()
    {
        if (PointerInside != null)
            PointerInside(this.gameObject, "Mouse pointer is on UI element " + this.name);
        //We could also invoke the delegate DelegateVar, but the risk is that DelegateVar.Invoke() could be called also
        //  from other classes, not only this one 
        //if(DelegateVar != null)
        //    DelegateVar(this.gameObject, "Mouse pointer is on UI element " + this.name);
    }
}
