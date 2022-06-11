using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DiscreteScrolling : MonoBehaviour
{
    public RectTransform Content;
    public Button RightB, LeftB;
    public float ScrollWidth=768;

    bool CanUpdate;
    Vector2 startPos;
    Vector2 endPos;
    float duration=0.5f;
    float timer;

    int currChildIndex=0;
    int maxChildIndex;

    private void Start()
    {
        maxChildIndex = Content.childCount-1;
    }

    public void Left()
    {
        if (currChildIndex > 0)
        {
            startPos = Content.anchoredPosition;
            endPos= new Vector2(Content.anchoredPosition.x + ScrollWidth, Content.anchoredPosition.y);
            CanUpdate = true;
            currChildIndex--;
            LeftB.interactable = false;
        }
    }

    private void Update()
    {
        if (CanUpdate)
        {
            timer += Time.deltaTime;

            float fraction = timer / duration;

            Content.anchoredPosition = Vector2.Lerp(startPos, endPos, fraction);

            if (timer >= duration)
            {
                Content.anchoredPosition = endPos;
                timer = 0;
                CanUpdate = false;
                LeftB.interactable = true;
                RightB.interactable = true;

            }
        }
    }


    public void Right()
    {
        if (currChildIndex < maxChildIndex)
        {

            startPos = Content.anchoredPosition;
            endPos = new Vector2(Content.anchoredPosition.x - ScrollWidth, Content.anchoredPosition.y);
            CanUpdate = true;
            currChildIndex++;
            RightB.interactable = false;

        }

    }
}
