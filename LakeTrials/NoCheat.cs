using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NoCheat : MonoBehaviour
{

    public GameObject dialogue;
    public GameObject dialogueImg;

    public Text timer;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Timer.started)
        {
            StartCoroutine(WrongWay());
        }
    }

    IEnumerator WrongWay()
    {
        TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        Timer.finished = true;
        Timer.started = false;
        timer.text = "";
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You're going the wrong way try again.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

    }
}
