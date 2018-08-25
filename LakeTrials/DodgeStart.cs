using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DodgeStart : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject dialogueImg;
    public GameObject dialogueChoice;

    public Button yes;
    public Button no;

    public int decision = 0;

    public GameObject testRope;

    public GameObject tutMessage;
    public Text tutText;

    public bool started = false;
    public GameObject trialController;

    // Use this for initialization
    void Start ()
    {
        yes.onClick.AddListener(PickYes);
        no.onClick.AddListener(PickNo);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!started)
        {
            StartCoroutine(StartChallenge());
        }        
    }

    IEnumerator StartChallenge()
    {
        started = true;
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Are you ready to start?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        dialogue.GetComponent<DialogueController>().waitingDecision = true;
        dialogueChoice.SetActive(true);

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        if(decision == 1)
        {
            decision = 0;

            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I'm ready.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Good, the test will start shortly. Try to dodge everything you can. Get hit 3 times and you'll need to try again.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }

            if (!TutorialDatabase.tut10)
            {
                tutText.text = "Dashing Basics:\nEvery character has a unique movement skill which can be used by pressing [Space]. Using this skill costs stamina.";
                tutMessage.SetActive(true);
                TutorialDatabase.tut10 = true;
            }

            StartCoroutine(GameObject.Find("Trial_Manager").GetComponent<TrialDodgeController>().StartRoutine());
            testRope.SetActive(true);

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(decision == 2)
        {
            decision = 0;

            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Not yet.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see, enter the testing arena when you are ready.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }
            started = false;
        }

    }

    void PickYes()
    {
        decision = 1;
        dialogueChoice.SetActive(false);
        dialogue.GetComponent<DialogueController>().waitingDecision = false;
    }
    void PickNo()
    {
        decision = 2;
        TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        dialogueChoice.SetActive(false);
        dialogue.GetComponent<DialogueController>().waitingDecision = false;
    }
}
