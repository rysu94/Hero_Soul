using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Start_2 : MonoBehaviour
{

    public GameObject dialogue;
    public GameObject systemMessage;
    public Text systemText;

    public GameObject NPC;

    public static bool finished = false;
    // Use this for initialization
    void Start ()
    {
        if(!LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            StartCoroutine(TalkRoutine());
            NPC.transform.position = new Vector2(-0.419f, 0.238f);
        }
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear && !finished)
        {
            finished = true;
            StartCoroutine(FinishRoutine());
        }
	}

    IEnumerator TalkRoutine()
    {     
        yield return new WaitForSeconds(.1f);
        TestCharController.attackEnabled = true;
        GameController.paused = true;
        TestCharController.inDialogue = true;
        TestCharController.player.GetComponent<Animator>().Play("TestRightWalk");
        NPC.GetComponent<Animator>().Play("M5_NPC_Left_Walk");
        for (int i = 0; i < 60; i++)
        {
            TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(.5f, 0);
            NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(-.25f, 0);
            yield return new WaitForSeconds(.02f);
        }
        NPC.GetComponent<Animator>().Play("M5_NPC_Right_Idle");
        NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        TestCharController.player.GetComponent<Animator>().Play("TestRightIdle");
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("M-Monsters! Someone please help!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "???", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hold on mister, I'll help you!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        GameController.paused = false;

        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Attack Basics:\nUse the [RT] to attack.";
        }
        else
        {
            systemText.text = "Attack Basics:\nUse the [RMB] to attack.";
        }
    }

    IEnumerator FinishRoutine()
    {
        GameController.paused = true;
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = true;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Thank you so much! I don't know how to thank you.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "???", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("No problem, mister. Glad I could help.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("The name is Maurice, alchemist apprentice.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Cecilia.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Say are you headed to Weiss?", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yeah! I'm on my way to the guild to take the exam.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Excellent! If you have the time, could you talk to Theo, the alchemist. Tell him I'm going to be a little late on my shipment. Oh, I'm sure he'll reward you as well for saving me.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Theo? Sure no problem.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Thanks you're a lifesaver and be careful there may be more monsters down the road!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 2));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        GameObject.Find("QuestManager").GetComponent<QuestManager>().AddQuest(QuestDatabase.ceQuest2);

        GameController.paused = false;
    }
}
