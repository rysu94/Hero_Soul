using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;

    public static bool lockCamera = false;

	// Use this for initialization
	void Start ()
    {
        //Change the mouse cursor
        Cursor.SetCursor((Texture2D)Resources.Load("Cursor"), Vector2.zero, CursorMode.Auto);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.A) && camera.transform.position.x > -4.68f && !lockCamera)
        {
            camera.transform.Translate(Vector2.left * .0125f);
        }
        else if (Input.GetKey(KeyCode.D) && camera.transform.position.x < -3.41 && !lockCamera)
        {
            camera.transform.Translate(Vector2.right * .0125f);
        }
        /*
        else if (Input.GetKey(KeyCode.W) && camera.transform.position.y < 1.92f && !lockCamera)
        {
            camera.transform.Translate(Vector2.up * .0125f);
        }
        else if (Input.GetKey(KeyCode.S) && camera.transform.position.y > -2.28 && !lockCamera)
        {
            camera.transform.Translate(Vector2.down * .0125f);
        }
        */
    }
}
