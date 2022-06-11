using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RTBBControllerStart : MonoBehaviour {

    public RectTransform Player1, Player2, Radar;

    public float Speed = 1;
    public string H1AxisName, V1AxisName;
    public string H2AxisName, V2AxisName;
    public float MinExtension = 0.5f;
    public float MaxExtension = 3;

    bool worldSpace;
    float h1, h2, v1, v2;


    private void Start()
    {
        worldSpace = Player1.GetComponentInParent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay ? false : true;

       
    }

    void Update() {
        h1 = Input.GetAxis(H1AxisName) * Time.deltaTime * Speed;
        v1 = Input.GetAxis(V1AxisName) * Time.deltaTime * Speed;
        h2 = Input.GetAxis(H2AxisName) * Time.deltaTime * Speed;
        v2 = Input.GetAxis(V2AxisName) * Time.deltaTime * Speed;

        //Move Players
        //...
        Vector2 p1Pos = new Vector2(h1, v1);
        Vector2 p2Pos = new Vector2(h2, v2);

        Player1.anchoredPosition += p1Pos;
        Player2.anchoredPosition += p2Pos;

        Vector2 middlePoint = (Player1.anchoredPosition + Player2.anchoredPosition) * 0.5f;

        Radar.anchoredPosition = middlePoint;

        float scaleX = Mathf.Abs(Player1.anchoredPosition.x - Player2.anchoredPosition.x) + Player1.rect.width;
        float scaleY = Mathf.Abs(Player1.anchoredPosition.y - Player2.anchoredPosition.y)+Player1.rect.height;

        if (worldSpace)
        {
            scaleX = scaleX <= MinExtension ?  MinExtension : scaleX;
            scaleY = scaleY <= MinExtension ?  MinExtension : scaleY;

            scaleX = scaleX >= MaxExtension ? MaxExtension : scaleX;
            scaleY = scaleY >= MaxExtension ? MaxExtension : scaleY;

        }

        Radar.sizeDelta = new Vector2(scaleX,scaleY);

        //if(Player1.anchoredPosition.x>= Player2.anchoredPosition.x &&
        //    Player1.anchoredPosition.y >= Player2.anchoredPosition.y)
        //{
        //    Vector2 maxPos = Player1.anchoredPosition;
        //    Vector2 minPos = Player2.anchoredPosition;

        //    LocateRadar(maxPos, minPos, player1Offset, player2Offset);

        //}else if (Player1.anchoredPosition.x >= Player2.anchoredPosition.x &&
        //    Player1.anchoredPosition.y < Player2.anchoredPosition.y)
        //{
        //    Vector2 maxPos = new Vector2(Player1.anchoredPosition.x, Player2.anchoredPosition.y);
        //    Vector2 minPos = new Vector2(Player2.anchoredPosition.x, Player1.anchoredPosition.y);

        //    LocateRadar(maxPos, minPos, player1Offset, player2Offset);

        //}
        //else if (Player1.anchoredPosition.x < Player2.anchoredPosition.x &&
        //   Player1.anchoredPosition.y >= Player2.anchoredPosition.y)
        //{
        //    Vector2 maxPos = new Vector2(Player2.anchoredPosition.x, Player1.anchoredPosition.y);
        //    Vector2 minPos = new Vector2(Player1.anchoredPosition.x, Player2.anchoredPosition.y);

        //    LocateRadar(maxPos, minPos, player1Offset, player2Offset);
        //}
        //else if (Player1.anchoredPosition.x < Player2.anchoredPosition.x &&
        //  Player1.anchoredPosition.y < Player2.anchoredPosition.y)
        //{
        //    Vector2 maxPos = Player2.anchoredPosition;
        //    Vector2 minPos = Player1.anchoredPosition;

        //    LocateRadar(maxPos, minPos, player1Offset, player2Offset);
        //}



        
    }

    void LocateRadar(Vector2 maxPosition, Vector2 minPosition, Vector2 offsetMax, Vector2 offsetMin)
    {
        Radar.offsetMax = maxPosition + offsetMax;
        Radar.offsetMin = minPosition - offsetMin;
    }
}
