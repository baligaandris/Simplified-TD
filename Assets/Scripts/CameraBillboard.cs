using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBillboard : MonoBehaviour {
    //Script that makes the health bars face the camera at all times

    Camera mainCamera; //adding the main camera directly and not with a public variable since we will have only one

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main; 
	}

    void Update()
    {
        //makes each healthbar face the camera each frame. Not quite sure how this works since it came straigh from the unity site.
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}
