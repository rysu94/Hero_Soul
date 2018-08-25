using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Start_6 : MonoBehaviour {

    public GameObject systemMessage;
    public Text systemText;
	// Use this for initialization
	void Start () {
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Map Basics:\nWhen there are enemies, doors will be locked. Defeat them to unlock them!";
        }
        else
        {
            systemText.text = "Map Basics:\nWhen there are enemies, doors will be locked. Defeat them to unlock them!";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
