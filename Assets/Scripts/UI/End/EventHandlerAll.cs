using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventHandlerAll : MonoBehaviour, IBeginDragHandler, ICancelHandler, IDeselectHandler, 
    IDragHandler, IDropHandler, IEndDragHandler, IMoveHandler, IPointerClickHandler, IPointerDownHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IScrollHandler, ISelectHandler,
    ISubmitHandler
{
    public bool PointerClick, Pointer, Drag, Drop, Scroll;
	public float pointerEnterScale 	= 2.0f;
	public float pointerExitScale 	= 1.0f;
    public float ZFor3DObjDrag;
    public Camera Cam;
    //Button b;
    //Image i;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!PointerClick) return;
        Debug.Log(name + " OnPointerClick");
    }
    public void OnPointerEnter (PointerEventData eventData){
        if (!Pointer) return;
		transform.localScale = new Vector3 (pointerEnterScale,pointerEnterScale,pointerEnterScale);
	}
	public void OnPointerExit (PointerEventData eventData){
        if (!Pointer) return;
        transform.localScale = new Vector3 (pointerExitScale,pointerExitScale,pointerExitScale);
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Pointer) return;
        Debug.Log(name + " OnPointerDown");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!Pointer) return;
        Debug.Log(name + " OnPointerUp");
    }

    public void OnDrag (PointerEventData eventData){
        if (!Drag) return;
        if(GetComponent<Graphic>() != null)
        {
            //We are a UI element
            if (GetComponentInParent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay)
                transform.position = eventData.position;
            else
            {
                Vector3 posOnRect;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponentInParent<RectTransform>(), eventData.position, Cam, out posOnRect);
                transform.position = posOnRect;
            }
        }
        else
            //x and y are 2D Screen point coords, z is the distance between Camera and the object. the obj will drag itself on a virtual plane at distance z from Camera
            transform.position = Cam.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, ZFor3DObjDrag));
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Drag) return;
        Debug.Log(name + " OnBeginDrag: " + eventData.position);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!Drag) return;
        Debug.Log(name + " OnEndDrag: " + eventData.position);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!Drop) return;
        if (eventData.pointerDrag != null)
            Debug.Log(name + " catched a dropped object: " + eventData.pointerDrag);
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (!Scroll) return;
        Debug.Log(name + " OnScroll.scrollDelta: " + eventData.scrollDelta);
    }

    public void OnCancel(BaseEventData eventData)
    {
        Debug.Log(name + " OnCancel");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(name + " OnDeselect");
    }
    public void OnMove(AxisEventData eventData)
    {
        Debug.Log(name + " OnMove.moveVector: " + eventData.moveVector);
    }
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(name + " OnSelect");
    }
    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log(name + " OnSubmit");
    }
}
