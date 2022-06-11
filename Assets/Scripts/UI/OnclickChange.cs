using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnclickChange : MonoBehaviour
{
    public Vector3 DeltaScale;
    public CubeEventMgr EventMgr;
    // Start is called before the first frame update
    void OnEnable()
    {
        EventMgr.OnMouseClick += OnClickMouseHandler;
    }

    // Update is called once per frame
    void OnDisable()
    {
        EventMgr.OnMouseClick -= OnClickMouseHandler;

    }

    void OnClickMouseHandler()
    {

        transform.localScale += DeltaScale;
    }
}
