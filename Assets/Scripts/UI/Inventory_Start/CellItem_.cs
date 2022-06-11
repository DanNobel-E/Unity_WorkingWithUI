using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


/*
 * Trigger this event
 * -EventMng.instance.OnItemRemoved
 */
public class CellItem_ : MonoBehaviour, IPointerClickHandler
{
    //public Item field to have a reference for the Item contained in this cell
    public Item_ Item;
    Image itemImage;
    public TextMeshProUGUI TextIndex { get; protected set; }

    void OnEnable()
    {
        itemImage = GetComponent<Image>();
        TextIndex = transform.parent.GetComponentInChildren<TextMeshProUGUI>(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button== PointerEventData.InputButton.Left)
            RemoveMyself();
        else if (eventData.button == PointerEventData.InputButton.Right)
            RemoveMyself(true);
    }

    public void SetItemImage(bool b, Sprite sprite = null)
    {
        itemImage.enabled = b;

        if (b)
            itemImage.sprite = sprite;

    }

    public void SetTextIndex(bool b, string s = null)
    {
        TextIndex.enabled = b;

        if (b)
            TextIndex.text = s;

    }

    public void RemoveMyself(bool removeAll=false){


            EventMng_.OnItemRemoved(Item, removeAll);

        
        // -Trigger EventMng.instance.OnItemRemoved(what parameters do you have to pass ?)
    }
}
