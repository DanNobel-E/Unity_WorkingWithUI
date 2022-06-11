using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Implement BeginDrag, Drag, EndDrag
public class SpriteEvents_Start : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public UICreator_Start uICreator;
	Vector3 startPos;
	GameObject dragGO;

	public void OnBeginDrag(PointerEventData eventData){
        startPos = transform.position;
        transform.GetComponent<Image>().color= new Color(1,1,1,0.5f);
        dragGO= uICreator.CreateSprite(startPos, uICreator.spriteResourcePath);
        //Create a new sprite dragGO (Use UICreator_ for it) at startPos
        //Set the Image color alpha on This GO to 0.5f
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragGO.transform.position = eventData.position;

        
	}

	public void OnEndDrag(PointerEventData eventData){

            transform.GetComponent<Image>().color= new Color(1,1,1,1);

        if (!Input.GetKey(KeyCode.Space))
        {
            transform.position = dragGO.transform.position;
            Destroy(dragGO);
        }


        //If SpaceBar is not pressed
        //  - move This GO where dragGO is
        //  - destroy dragGO
        //Else
        //  - Set the Image color alpha on This GO to 1.0f
    }
}
