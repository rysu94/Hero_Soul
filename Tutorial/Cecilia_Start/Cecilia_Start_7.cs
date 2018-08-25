using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Start_7 : MonoBehaviour {

    public GameObject systemMessage;
    public Text systemText;

	// Use this for initialization
	void Start () {
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Map Basics:\nThe minimap can be seen in the top right. You will always be in the center of the minimap.";
        }
        else
        {
            systemText.text = "Map Basics:\nThe minimap can be seen in the top right. You will always be in the center of the minimap.";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
