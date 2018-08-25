using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject dialogueImg;

    // Use this for initialization
    void Start ()
    {
        if(!LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            StartCoroutine(StartDialogue2());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator StartDialogue2()
    {
        yield return new WaitForSeconds(2.15f);
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hello and welcome to the annual adventurer guild exam. My name is Byron, I'll be your proctor today. Could the first applicant please step up to take the first test.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("This is it. I'm going to pass this exam and get into the guild!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

    }
}
