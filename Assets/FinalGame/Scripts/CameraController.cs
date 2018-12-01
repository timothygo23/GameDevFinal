using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] Camera cam;
    // [SerializeField] float keyPanSpeed;

    private const float keyPanSpeed = 150f;
    //private const float updownkeyPanSpeed = 100f;
    private const float mouseSensitivity = 100f;
    private const float clampAngle = 80.0f;

    private float rotY = 0.0f;
    private float rotX = 0.0f;

    float camSens = 0.15f; 
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    // Use this for initialization
    void Start () {
        //lastMouse = new Vector3(0, 0, 0);
        //lastMouse = cam.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;

        // key input
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, keyPanSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -keyPanSpeed * Time.deltaTime, 0));
        }

        /*
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(new Vector3(-updownkeyPanSpeed * Time.deltaTime , 0, 0));
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(new Vector3(updownkeyPanSpeed * Time.deltaTime, 0, 0));
        }
        */



    }
}
