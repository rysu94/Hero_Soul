using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBox_Controller : MonoBehaviour
{
    public bool controller = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameController.xbox360Enabled())
        {
            /*
            if (Mathf.Abs(InputManager.CursorJoystick().magnitude) > 0)
            {
                HardwareCursor.SimulateController(Input.GetAxis("J_CursorHorizontal"), Input.GetAxis("J_CursorVertical"), 8);
            }


            if (InputManager.A_Button())
            {
                controller = true;
                HardwareCursor.LeftClick();
                StartCoroutine(ControllerBuffer());
            }
            if (InputManager.B_Button())
            {
                controller = true;
                HardwareCursor.RightClick();
                StartCoroutine(ControllerBuffer());
            }
            */
            if (InputManager.L_Bumper() && Card_Interface.canClick)
            {
                GetComponent<AudioSource>().Play();
                Card_Interface.spellTabNum--;
                if (Card_Interface.spellTabNum < 0)
                {
                    Card_Interface.spellTabNum = 4;
                }
            }
            else if (InputManager.R_Bumper() && Card_Interface.canClick)
            {
                GetComponent<AudioSource>().Play();
                Card_Interface.spellTabNum++;
                if (Card_Interface.spellTabNum > 4)
                {
                    Card_Interface.spellTabNum = 0;
                }
            }
        }
    }

    IEnumerator ControllerBuffer()
    {
        yield return new WaitForSeconds(.25f);
        controller = false;
    }
}
