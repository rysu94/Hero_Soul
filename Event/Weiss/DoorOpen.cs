using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject north1;
    public GameObject north2;
    public GameObject north3;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Opens the doors if the room has been cleared
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear && !Town_Event_4.start)
        {
            north1.SetActive(false);
            north2.SetActive(false);
            north3.SetActive(false);
        }
    }
}
