using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharAnimation : MonoBehaviour
{

    public Animator objectController;
    public string dir = "right";


	// Use this for initialization
	void Start ()
    {
        objectController = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!InventoryController.inInv && !TestCharController.inDialogue && !TestCharController.player.GetComponent<TestCharController>().isSliding)
        {
            if (Input.GetKey(KeyCode.W))
            {
                dir = "up";
                if(!TestCharController.isSprinting)
                {
                    objectController.Play("TestUpWalk");
                }
                else if(TestCharController.isSprinting)
                {
                    objectController.Play("Sprint_Up");
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                dir = "left";
                if(!TestCharController.isSprinting)
                {
                    objectController.Play("TestLeftWalk");
                }
                else if (TestCharController.isSprinting)
                {
                    objectController.Play("Sprint_Left");
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir = "right";
                if(!TestCharController.isSprinting)
                {
                    objectController.Play("TestRightWalk");
                }
                if (TestCharController.isSprinting)
                {
                    objectController.Play("Sprint_Right");
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir = "down";
                if(!TestCharController.isSprinting)
                {
                    objectController.Play("TestWalkDown");
                }
                if (TestCharController.isSprinting)
                {
                    objectController.Play("Sprint_Down");
                }
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                objectController.Play("TestUpIdle");
                dir = "up";
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                objectController.Play("TestLeftIdle");
                dir = "left";
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                objectController.Play("TestRightIdle");
                dir = "right";
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                objectController.Play("TestDownIdle");
                dir = "down";
            }

        }
        if (InventoryController.inInv)
        {
            if (dir == "up")
            {
                objectController.Play("TestUpIdle");
            }
            else if (dir == "left")
            {
                objectController.Play("TestLeftIdle");
            }
            else if (dir == "right")
            {
                objectController.Play("TestRightIdle");
            }
            else if (dir == "down")
            {
                objectController.Play("TestDownIdle");
            }

        }

    }
}
