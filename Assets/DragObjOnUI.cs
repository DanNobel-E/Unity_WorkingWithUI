using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public enum ObjType { Cylinder, Capsule,Sphere, Cube}
public class DragObjOnUI : MonoBehaviour, IDragHandler, IDropHandler
{
    public ObjType Type;
    public RectTransform Panel;
    public Image UIBG;
    public Camera Cam;
    Dictionary<ObjType, TextMeshProUGUI> GUIDic= new Dictionary<ObjType, TextMeshProUGUI>();
    Vector3 startPos;
    Quaternion startRot;



    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        for (int i = 0; i < UIBG.transform.childCount; i++)
        {
            GUIDic[(ObjType)i]= (UIBG.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>());
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(Panel, eventData.position, Cam))
        {
            Vector3 inWorldPos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(Panel, eventData.position, Cam, out inWorldPos);

            transform.position = inWorldPos;
            transform.rotation = Quaternion.LookRotation(-Panel.forward);
            return;
        }

        Vector3 inScreenPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle((RectTransform)Panel.parent, eventData.position, Cam, out inScreenPos);

        transform.position = inScreenPos;

    }

    public void OnDrop(PointerEventData eventData)
    {
        for (int i = 0; i < Panel.childCount; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)Panel.GetChild(i).transform, eventData.position, Cam))
            {
                transform.position = Panel.GetChild(i).position;
                int currNum = int.Parse(GUIDic[Type].text);
                currNum++;
                GUIDic[Type].text = currNum.ToString();
                return;
            }
           
        }

        transform.position = startPos;
        transform.rotation = startRot;
        int num = int.Parse(GUIDic[Type].text);
        num--;
        GUIDic[Type].text = num.ToString();
    }

    
    
}
