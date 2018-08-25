using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHelp : MonoBehaviour
{
    public GameObject helpHUD;

    public GameObject controllerIMG, keyboardIMG;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameController.xbox360Enabled())
        {
            if (InputManager.J_Back())
            {
                if(helpHUD.activeInHierarchy)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GameController.paused = false;
                    TestCharController.inDialogue = false;
                    keyboardIMG.SetActive(false);
                    controllerIMG.SetActive(false);
                    helpHUD.SetActive(false);
                }
                else if(!TestCharController.inDialogue)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GameController.paused = true;
                    TestCharController.inDialogue = true;
                    helpHUD.SetActive(true);
                    controllerIMG.SetActive(true);
                }
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                if (helpHUD.activeInHierarchy)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GameController.paused = false;
                    TestCharController.inDialogue = false;
                    keyboardIMG.SetActive(false);
                    controllerIMG.SetActive(false);
                    helpHUD.SetActive(false);
                }
                else if(!TestCharController.inDialogue)
                {
                    GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
                    GameController.paused = true;
                    TestCharController.inDialogue = true;
                    helpHUD.SetActive(true);
                    keyboardIMG.SetActive(true);
                }
            }
        }
	}
}
