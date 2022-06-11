using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotationFix : MonoBehaviour {
	public bool cameraFacing;
	public bool avoidBackFace;
    public Camera Cam;

    void Update () {
		if(cameraFacing){
			transform.rotation 	= Cam.transform.rotation;
			//transform.forward 	= Camera.main.transform.forward;
		}
		else if(avoidBackFace){
			float dot = Vector3.Dot (Cam.transform.position - transform.position,
				            transform.forward);
            //dot >= 0 only if canvas is facing the Camera (or is perpendicular to it)
            if (dot >= 0) {
				transform.Rotate (Vector3.up - new Vector3 (0, 180, 0));
			}
		}
	}
}
