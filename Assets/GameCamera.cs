using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    const int CAMERA_Z_VALUE = -100;
    const int CAMERA_ZOOM_MIN_VALUE = 4;
    const int CAMERA_ZOOM_MAX_VALUE = 6;
    const float CAMERA_ZOOM_STEP = 0.2f;
    float cameraSize;

    GameObject traveler;
    bool canCameraFollowTraveler;

	// Use this for initialization
	void Start () {
        cameraSize = Camera.main.orthographicSize;
        canCameraFollowTraveler = true;
        UpdateTravelerPosition();

    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCameraPos();
        HandleZoom();
    }



    void UpdateTravelerPosition()
    {
        traveler = GameObject.FindGameObjectWithTag("traveler");
    }

    void UpdateCameraPos()
    {
        if (canCameraFollowTraveler)
            transform.position = new Vector3(traveler.transform.position.x, traveler.transform.position.y, CAMERA_Z_VALUE);
    }

    void HandleZoom()
    {
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue != 0)
        {
            int scrollDir = (scrollValue > 0) ? -1 : 1;
            bool isScrollPosible = ( (scrollDir == -1 && cameraSize - CAMERA_ZOOM_STEP > CAMERA_ZOOM_MIN_VALUE) || 
                                     (scrollDir == 1 && cameraSize + CAMERA_ZOOM_STEP < CAMERA_ZOOM_MAX_VALUE)
                                     ) ? true : false;
            if(isScrollPosible)
            Camera.main.orthographicSize = cameraSize = Camera.main.orthographicSize + CAMERA_ZOOM_STEP * scrollDir;
        }


    }

}
