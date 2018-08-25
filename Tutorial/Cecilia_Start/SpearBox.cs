using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpearBox : MonoBehaviour
{
    public GameObject dialogue;
    bool active = false;

    public GameObject systemMessage;
    public Text systemText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" && !active)
        {
            collider.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
            StartCoroutine(PlayerTalk());
        }
    }

    IEnumerator PlayerTalk()
    {
        active = true;
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I can't forget my spear. It should be on the table.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        active = false;
        systemMessage.SetActive(false);
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Quest Basics:\nYou can press the A button to interact with objects.";
        }
        else
        {
            systemText.text = "Interact Basics:\nYou can press the F button to interact with objects.";
        }
    }
}
