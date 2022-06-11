using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragMe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;
    Canvas canvas;


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas = transform.GetComponentInParent<Canvas>();
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();
        // The icon will be under the cursor.
        //  We want it to be ignored by the event system. If we don't perform [A] or [B],
        //  we wont be able to dragdrop the image properly
        // [A] If the icon has other Graphics UI elements under it, use CanvasGroup:
        //  var group = m_DraggingIcon.AddComponent<CanvasGroup>();
        //  group.blocksRaycasts = false;
        // [B] Otherwise, set the image.raycastTarget to false is enough:
        image.raycastTarget = false;
        
        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null)
            SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        //During dragging, if dragOnSurfaces==TRUE, we should update m_DraggingPlanes to the LAST worldSpace Panel that received pointerEnter event
        if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = eventData.pointerEnter.transform as RectTransform;
        else
            //On Begin Drag, the icon should reset its rotation to that of the screen canvas
            m_DraggingPlane = canvas.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();

        Vector3 globalMousePos;
        /* ScreenPointToWorldPointInRectangle
         * - dragOnSurfaces TRUE
         *      - Calculate mouse position inside the LAST worldSpace Panel that received pointerEnter event:
         *          globalMousePos will have Z component = hitted point on the last current panel,
         *          it will lie on the Canvas
         * - dragOnSurfaces FALSE
         *      - Calculate mouse position inside the ScreenSpace-Camera canvas:
         *          globalMousePos will have Z component = 0,
         *          it will lie on the Canvas
         *      
         * Requires a Camera, to cast a ray from it
         */
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos; //See previous comment to know globalMousePos val
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null)
            Destroy(m_DraggingIcon);

        m_DraggingIcon = null;
    }
}
