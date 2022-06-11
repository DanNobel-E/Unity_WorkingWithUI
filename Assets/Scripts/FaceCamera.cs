using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera Cam;

    private void Start()
    {
        Cam = Camera.main;
        GetComponentInParent<Canvas>().worldCamera = Cam;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = Cam.transform.position-transform.position;
        transform.rotation = Quaternion.LookRotation(-fwd);
    }
}
