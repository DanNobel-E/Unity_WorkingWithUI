using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandlerStart : MonoBehaviour, IPointerEnterHandler, IDragHandler, IPointerExitHandler
{
    public float pointerEnterScale = 2.0f;
    public float pointerExitScale = 1.0f;
    Canvas canvas;
    RectTransform rt;
    Camera cam;
    Vector3 posOnRect;
    public bool Drag;

    private void Start()
    {
        rt = GetComponentInParent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cam = Camera.main;
    }

    public void OnPointerEnter(PointerEventData eventData) {

        transform.localScale *= 1.1f;
    }
    public void OnDrag(PointerEventData eventData)
    {

        //We are a UI element
        if (GetComponentInParent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay)
            transform.position = eventData.position;
        else
        {
            Vector3 posOnRect;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponentInParent<RectTransform>(), eventData.position, cam, out posOnRect);
            transform.position = posOnRect;
        }
    }

    public void OnPointerExit (PointerEventData eventData){

        transform.localScale = Vector3.one;
	}

}
