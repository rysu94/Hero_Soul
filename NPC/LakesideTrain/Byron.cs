using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Byron : NPC
{
    public GameObject dialoguePrompt;
    public Button yes;
    public Button no;
    public int decision = 0;

    public GameObject tutMessage;
    public Text tutText;

    public GameObject startFence;
    public GameObject endFence;

    public GameObject talkBubble;
    public GameObject arrowRight;

    public GameObject arcanaFrame;
    public GameObject playerDeck;

    public static bool tutorialMoveShown = false;

	// Use this for initialization
	void Start ()
    {
        ReturnPlayer();
        yes.onClick.AddListener(ChooseYes);
        no.onClick.AddListener(ChooseNo);



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
        if (TutorialDatabase.tut1)
        {
            talkBubble.SetActive(false);
        }
        if(!TutorialDatabase.tut8)
        {
            arcanaFrame.SetActive(false);
            playerDeck.SetActive(false);
        }
    }

    IEnumerator TalkRoutine()
    {
        TurnPlayer();
        if(decision == 0 && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);

            dialogue.GetComponent<DialogueController>().waitingDecision = true;

            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Are you ready for your first test?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            dialoguePrompt.SetActive(true);

        }
        else if(decision == 0 && LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);

            dialogue.GetComponent<DialogueController>().waitingDecision = true;

            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Do you wish to retry the course?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            dialoguePrompt.SetActive(true);
        }

        else if(decision == 3)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Feel free to try this test as many times are you want. Just enter the couse to start", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
        }

        while (decision == 0)
        {
            yield return null;
        }

        while (!dialogue.GetComponent<DialogueController>().dialogueDone && !dialogue.GetComponent<DialogueController>().waitingDecision)
        {

            yield return null;
        }
        dialoguePrompt.SetActive(false);

        if(decision == 1)
        {
            startFence.SetActive(false);
            endFence.SetActive(false);
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I'm ready.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Good! This is a fitness test. Run through the obstacle course before time runs out.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Enter the couse when you are ready and I'll start the time.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            decision = 3;
        }
        else if(decision == 2)
        {
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Not yet.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see, come back when you're ready.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            decision = 0;
        }

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        arrowRight.SetActive(true);
        if (decision == 3 && !TutorialDatabase.tut1)
        {
            TutorialDatabase.tut1 = true;
            tutText.text = "Basic Movement:\nYou can hold [LShift] while moving to cause your character to sprint.";
            tutMessage.SetActive(true);
        }
        
        ReturnPlayer();
    }

    void ChooseYes()
    {
        decision = 1;
        dialoguePrompt.SetActive(false);
    }

    void ChooseNo()
    {
        decision = 2;
        dialoguePrompt.SetActive(false);
    }
    
}
