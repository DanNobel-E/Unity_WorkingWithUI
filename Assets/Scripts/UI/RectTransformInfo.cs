using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectTransformInfo : MonoBehaviour {
    public bool displayInfo = true;
    public Vector2 offset;
    string info;
    RectTransform rectT;
	Rect currentRect;

    void Start () {
		rectT = this.GetComponent<RectTransform>();
	}

    void PrintInfo()
    {
        info = "";
        info += "rectT.rect: [" + rectT.rect + "]\n";
        info += "rectT.anchoredPosition: [" + rectT.anchoredPosition + "]\n";
        info += "rectT.position: [" + rectT.position + "]\n";
        info += "rectT.anchorMax: [" + rectT.anchorMax + "]\n";
        info += "rectT.anchorMin: [" + rectT.anchorMin + "]\n";
        info += "rectT.offsetMax: [" + rectT.offsetMax + "]\n";
        info += "rectT.offsetMin: [" + rectT.offsetMin + "]\n";
        info += "rectT.pivot: [" + rectT.pivot + "]\n";
        info += "rectT.sizeDelta: [" + rectT.sizeDelta + "]\n";
        info += "---\n";
        info += "transform.position: [" + transform.position + "]\n";
    }

    void OnGUI()
    {
        if (!displayInfo) return;
        GUILayout.BeginHorizontal();
        GUILayout.Space(offset.x);
        GUILayout.BeginVertical();
        GUILayout.Space(offset.y);
        GUILayout.BeginVertical("box");
        GUILayout.Label(info);
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private void Update()
    {
        PrintInfo();
    }
}