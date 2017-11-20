using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public GameObject stoneN;
    public GameObject stoneS;
    public GameObject stoneE;
    public GameObject stoneW;


	// Use this for initialization
	void Start ()
    {
		if(TestCharController.player.GetComponent<TestCharController>().north)
        {
            stoneN.SetActive(true);
        }
        else if(TestCharController.player.GetComponent<TestCharController>().south)
        {
            stoneS.SetActive(true);
        }
        else if(TestCharController.player.GetComponent<TestCharController>().east)
        {
            stoneE.SetActive(true);
        }
        else if(TestCharController.player.GetComponent<TestCharController>().west)
        {
            stoneW.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
