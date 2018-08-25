using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Byron2 : NPC
{
    public GameObject dialoguePrompt;

    public GameObject tutMessage;
    public Text tutText;

    public static bool phase1 = false;
    public static bool phase2 = false;
    public static bool phase3 = false;
    public static bool phase4 = false;

    public GameObject talkBubble;
    public GameObject arrowRight;

    public GameObject arcanaFrame;
    public GameObject playerDeck;

    // Use this for initialization
    void Start ()
    {
        ReturnPlayer();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        float distance = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y + .4f), TestCharController.player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade)
        {
            interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .35 && Input.GetKeyDown(KeyCode.F) && !TestCharController.inDialogue)
        {
            StartCoroutine(TalkRoutine());
        }

        if(TutorialDatabase.tut2)
        {
            talkBubble.SetActive(false);
        }
        if (!TutorialDatabase.tut8)
        {
            arcanaFrame.SetActive(false);
            playerDeck.SetActive(false);
        }
        else
        {
            arcanaFrame.SetActive(true);
            playerDeck.SetActive(true);
        }


    }

    IEnumerator TalkRoutine()
    {
        TurnPlayer();

        if(phase1)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("When you are ready, grab a weapon and start hitting a dummy.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
        }
        if (phase2)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Show me what you got, try hitting the dummy with your best attack.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
        }
        if (phase3 || phase4)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Please move on to the next test.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
        }
        if (!phase1 && !phase2 && !phase3 && !phase4 && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("The next test is in combat. When you are ready, grab a weapon and start hitting a dummy.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Gotcha.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            phase1 = true;
        }

        if (!phase1 && !phase2 && !phase3 && !phase4 && LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Please move on to the next test.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
        }

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        if(phase1)
        {
            arrowRight.SetActive(true);
        }

        talkBubble.SetActive(false);

        ReturnPlayer();
    }
}
