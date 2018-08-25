using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp_Manager : MonoBehaviour {

    public GameObject TopLeft;
    public GameObject Bottom;
    public GameObject BottomRight;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        TopLeft.SetActive(false);
        Bottom.SetActive(false);
        BottomRight.SetActive(false);
    }
}
