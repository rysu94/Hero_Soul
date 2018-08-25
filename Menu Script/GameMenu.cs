using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public Button gameButton;
    public Button graphicsButton;
    public Button soundButton;
    public Button controlButton;
    public Button exit1Button;
    public Button exit2Button;
    public Button returnButton;


    public GameObject blackMask;
	// Use this for initialization
	void Start ()
    {
        returnButton.onClick.AddListener(ReturnMenu);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void ReturnMenu()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        blackMask.SetActive(false);
        TestCharController.inDialogue = false;
        GameController.paused = false;
    }
}
