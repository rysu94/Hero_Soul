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
        if (!InventoryController.inInv && !TestCharController.inDialogue && !TestCharController.player.GetComponent<TestCharController>().isSliding && !TestCharController.isSwinging)
        {
            


        }
        
        if (InventoryController.inInv)
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                objectController.Play("TestUpIdle");
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                objectController.Play("TestLeftIdle");
            }
            else if (TestCharController.player.GetComponent<TestCharController>().east)
            {
                objectController.Play("TestRightIdle");
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                objectController.Play("TestDownIdle");
            }

        }
        

    }
}
