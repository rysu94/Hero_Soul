using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grey_Sign : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;

    bool active = false;
    public bool textMade = false;

    public GameObject dialogue, player;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade)
        {
            interactText = Instantiate(Resources.Load("Prefabs/QuestionBubble") as GameObject, TestCharController.player.Find("Player_States_Panel").transform);
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }

        //If interact is pressed
        if (distance < .35 && (InputManager.A_Button() || Input.GetKeyDown(KeyCode.F)) && !active && !TestCharController.inDialogue)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            StartCoroutine(CatTalk());
        }
    }

    IEnumerator CatTalk()
    {
        active = true;
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().noMove = false;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().resetChar = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("\"Grey Residence\"", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Sign", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        active = false;
    }
}
