using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class Cecilia_Start_8 : MonoBehaviour {
    public GameObject systemMessage;
    public Text systemText;
    // Use this for initialization
    void Start () {
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Map Basics:\nRooms you have already visited will be outlined in green.";
        }
        else
        {
            systemText.text = "Map Basics:\nRooms you have already visited will be outlined in green.";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
