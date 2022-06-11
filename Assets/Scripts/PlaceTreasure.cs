using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlaceTreasure : MonoBehaviour
{
    public RectTransform Treasure;
    public RectTransform Radar, TreasureRadar;


    void Start()
    {
        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        RectTransform canvas = (RectTransform)transform;

        float posX = MathUtils.Remap(randomX, 0, 1, 0, canvas.sizeDelta.x);
        float posY = MathUtils.Remap(randomY, 0, 1, 0, canvas.sizeDelta.y);

        float radarPosX = MathUtils.Remap(randomX, 0, 1, 0, Radar.rect.size.x);
        float radarPosY = MathUtils.Remap(randomY, 0, 1, 0, Radar.rect.size.y);



        Treasure.anchoredPosition = new Vector2(posX, posY);
        TreasureRadar.anchoredPosition = new Vector2(radarPosX, radarPosY);



    }


}
