using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tim : NPC
{

	// Use this for initialization
	void Start ()
    {
        ReturnPlayer();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);

        if (distance < .5 && !textMade)
        {
            interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            textMade = true;
        }
        else if (distance >= .5)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .5 && Input.GetKeyDown(KeyCode.F) && !TestCharController.inDialogue)
        {
            StartCoroutine(TalkRoutine());
        }

    }

    IEnumerator TalkRoutine()
    {
        TurnPlayer();
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("The guild always has jobs that need to be done. They pay pretty well too.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_None", name, .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        ReturnPlayer();
    }
}
