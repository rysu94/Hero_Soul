using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Byron3 : NPC
{
    public GameObject tutMessage;
    public Text tutText;

    public GameObject talkBubble;

    public GameObject[] westWall = new GameObject[3];
    public GameObject[] eastWall = new GameObject[3];

    public GameObject arrow_Right;
    public GameObject testRope;
    public GameObject start;

    public static bool testPassed = false;

    // Use this for initialization
    void Start()
    {
        ReturnPlayer();
        
    }

    // Update is called once per frame
    void Update()
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

        if (TutorialDatabase.tut10)
        {
            talkBubble.SetActive(false);
        }

        if(!testPassed)
        {
            eastWall[0].SetActive(true);
            eastWall[1].SetActive(true);
            eastWall[2].SetActive(true);
            westWall[0].SetActive(true);
            westWall[1].SetActive(true);
            westWall[2].SetActive(true);
        }
        else
        {
            eastWall[0].SetActive(false);
            eastWall[1].SetActive(false);
            eastWall[2].SetActive(false);
            westWall[0].SetActive(false);
            westWall[1].SetActive(false);
            westWall[2].SetActive(false);
        }
    }

    IEnumerator TalkRoutine()
    {
        TurnPlayer();

        if(!testPassed)
        {
            start.GetComponent<BoxCollider2D>().enabled = true;
            start.GetComponent<DodgeStart>().started = false;
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("If you've gotten this far, that means your quick on your feet and are skilled enough to deal some real damage. But how are your skills at avoiding attacks?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("This last test will test your dodging skills as well as your abilities to counter. Please enter the testing grounds when you are ready.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
        }
        else
        {
            start.GetComponent<BoxCollider2D>().enabled = true;
            start.GetComponent<DodgeStart>().started = false;
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You can try this trial as many times as you like. Enter when you are ready.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", name, .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
        }

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        testRope.SetActive(false);
        arrow_Right.SetActive(true);
        ReturnPlayer();
    }
}

