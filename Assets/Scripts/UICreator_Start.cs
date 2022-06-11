using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICreator_Start : MonoBehaviour {
	///
	public static UICreator_Start instance;

	Canvas newCanvas;
	public string spriteResourcePath = "sprite";

    private void Awake()
    {
        ///
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start () {
        //Create a new Canvas with its standard components
        GameObject newCanvas = new GameObject("newCanvas");
        this.newCanvas= newCanvas.AddComponent<Canvas>();
        this.newCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        newCanvas.AddComponent<CanvasScaler>();
        newCanvas.AddComponent<GraphicRaycaster>();
        //Create a new EventSystem
        GameObject newES = new GameObject("newEventSystem");
        newES.AddComponent<EventSystem>();
        newES.AddComponent<StandaloneInputModule>();
        //Create a Sprite and Align it
        CreateSprite(new Vector2(0,0), spriteResourcePath);
	}

    //Create a Sprite and Align it
    public GameObject CreateSprite(Vector2 pos, string resourcePath)
    {
        GameObject spriteGO = new GameObject("newSprite");
        Sprite sprite = Resources.Load<Sprite>(resourcePath);
        spriteGO.AddComponent<Image>().sprite=sprite;

        spriteGO.transform.parent = newCanvas.transform;
        RectTransform spriteRect = spriteGO.GetComponent<RectTransform>();
        spriteRect.anchoredPosition = pos;
        Vector2 size = new Vector2(sprite.rect.width, sprite.rect.height);

        // - Create an Empty Game Object spriteGO
        // - Create a New Sprite and load it from Resources folder
        // - Add an Image Component to the new spriteGO and set its sprite

        //- Move spriteGO using pos as a relative position

        //- Set spriteGO position relative to parent edges (TOP LEFT)
        //If we don't specify the newSprite Size, it will be be 100x100 by default
        //Set the pos 50 units from the left parent edge and with width same as its original width
        //Eg. : rt.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Left, distanceFromLeft, horizontalSize);
        spriteRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, size.x);
        spriteRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, size.y);
        spriteGO.AddComponent<SpriteEvents_Start>().uICreator=this;

        //- Add interaction 

        return spriteGO;
    }
}
