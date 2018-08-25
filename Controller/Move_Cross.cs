using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Move_Cross : MonoBehaviour
{
    public Image crosshair;
    public static Vector2 crossPos;

	// Use this for initialization
	void Start ()
    {
        crosshair.transform.position = crossPos;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GameController.xbox360Enabled() && !GameController.paused && !InventoryController.inInv && !TestCharController.inDialogue && (ArcanaController.isTarget || ArcanaController.isCasting))
        {
            crosshair.gameObject.SetActive(true);
            if (InputManager.CursorHorizontal() != 0 || InputManager.CursorVertical() != 0)
            {
                crosshair.GetComponent<Rigidbody2D>().velocity = new Vector2(InputManager.CursorHorizontal(), InputManager.CursorVertical()) * 1.5f;
                crossPos = crosshair.transform.position;
                //print(crossPos);
            }
            else
            {
                crosshair.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);                
            }
        }
        else
        {
            crosshair.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            crosshair.gameObject.SetActive(false);
        }

        if (crossPos.x < -2.4f)
        {
            crosshair.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
            crosshair.transform.position = crossPos;
        }
        else if(crossPos.x > 2.4f)
        {
            crosshair.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
            crosshair.transform.position = crossPos;
        }
        else if(crossPos.y > 1.2f)
        {
            crosshair.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
            crosshair.transform.position = crossPos;
        }
        else if(crossPos.y < -1.45f)
        {
            crosshair.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        }
	}
}
