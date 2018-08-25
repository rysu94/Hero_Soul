using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Alc : MonoBehaviour
{
    public GameObject HUD, compHUD, menu;
    public GameObject blackMask;
    public GameObject systemMessage;
    public Text systemText;

    public GameObject dialogue;

    public GameObject theo;

    public static bool finished = false;

    // Use this for initialization
    void Start ()
    {
		if(!finished)
        {
            StartCoroutine(EventRoutine());
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    IEnumerator EventRoutine()
    {
        TestCharController.inDialogue = true;
        Camera.main.orthographicSize = 0.8446903f;
        Camera.main.transform.position = new Vector3(0.952f, -1.013f, -10);


        yield return new WaitForSeconds(.1f);

        HUD.SetActive(false);
        compHUD.SetActive(false);
        menu.SetActive(false);

        StartCoroutine(PlayerWalk());
        //From (.952,-.926) to (-0.924, -0.505)
        for (float i = .952f; i > -.924; i -= .0025f)
        {
            Camera.main.transform.position = new Vector3(i, (-.926f) + (Mathf.Abs(i-.952f)/1.876f)*.421f, -10);
            yield return new WaitForSeconds(.01f);
        }
        TestCharController.player.GetComponent<TestCharController>().north = false;
        TestCharController.player.GetComponent<TestCharController>().south = false;
        TestCharController.player.GetComponent<TestCharController>().east = false;
        TestCharController.player.GetComponent<TestCharController>().west = true;
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = false;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Excuse me.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("...", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Theo", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Uh, excuse me.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yes, yes what is it. I'm the middle in an experiement.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Theo", .9f, 2));

        Dialogue tempDia3 = new Dialogue("It's about your apprentice, Maurice.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Cecilia", 1f, 0);
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia3);
        tempDia3.dialogueAction = MakeQuestion();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Who?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Theo", .9f, 2));

        Dialogue tempDia = new Dialogue("Maurice? I met him on road on the way to Weiss.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Cecilia", 1f, 0);
        tempDia.dialogueAction = TurnTheo();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Oh, Maurice! Say, where is that lad? He was supposed to be here with a new shipment of ingredients.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Theo", .9f, 2));
        
        Dialogue tempDia2 = new Dialogue("About that...", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Cecilia", 1f, 0);
        tempDia2.dialogueAction = FadeIn();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia2);

        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see, well that's unfortunate. Thank you for helping my dear apprentice.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Theo", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("No problem glad I could help.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Alc", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Thanks for your help Adventurer, feel free to take some of the potions I've brewed as a thanks.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Theo", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Wow! I'm not an Adventurer yet, but thanks!", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Alc", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hohoho! Then you're here for the exam? I'm sure you'll make a fine one! Good luck! If you ever need to refill your potions feel free to see me again!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Alc", "Theo", .9f, 2));

        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        TestCharController.player.GetComponent<Animator>().Play("TestLeftIdle");
        HUD.SetActive(true);
        compHUD.SetActive(false);
        menu.SetActive(true);
        LevelCreator.potionEnabled = true;
        Camera.main.orthographicSize = 1.628882f;

        GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("Quest Complete!\n<color=yellow>-The Apprentice</color>");
        //Add a new objective
        for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
        {
            if (QuestManager.activeMainQuests[i].questName == "The Apprentice")
            {
                QuestManager.activeMainQuests[i].objComplete[0] = true;
                QuestManager.activeMainQuests[i].DistributeReward();
                GameObject.Find("QuestManager").GetComponent<QuestManager>().RemoveQuest("The Apprentice");
            }
        }

        systemMessage.SetActive(false);
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Potion Basics:\nYou've unlocked combat potions. To use them, press X and Y respectively.";
        }
        else
        {
            systemText.text = "Potion Basics:\nYou've unlocked combat potions. To use them, press E and R respectively.";
        }

        finished = true;
    }

    IEnumerator PlayerWalk()
    {
        for(int i = 0; i < 45; i++)
        {
            TestCharController.player.GetComponent<Animator>().Play("TestUpWalk");
            TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, .25f);
            yield return new WaitForSeconds(.1f);
        }
        for (int i = 0; i < 75; i++)
        {
            TestCharController.player.GetComponent<Animator>().Play("TestLeftWalk");
            TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(-.25f, 0);
            yield return new WaitForSeconds(.1f);
        }
        TestCharController.player.GetComponent<Animator>().Play("TestLeftIdle");
    }

    IEnumerator TurnTheo()
    {
        theo.GetComponent<Animator>().Play("Theo_Right_Idle");
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator FadeIn()
    {
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("BlankScreen");
        yield return new WaitForSeconds(1f);
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("FadeIn");
    }

    IEnumerator MakeQuestion()
    {
        Instantiate(Resources.Load("Prefabs/Dialogue/Question_Mark"), theo.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
    }
}
