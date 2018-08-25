using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Start_3 : MonoBehaviour
{
    public GameObject HUD, compHUD, menu;
    public GameObject blackMask;
    public GameObject systemMessage;
    public Text systemText;

    public GameObject dialogue, startWipe;

    public static bool finished = false;

    //1.250357
    //1.828043

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
        Camera.main.orthographicSize = 1.250357f;
        Camera.main.transform.position = new Vector3(-0.41f, 0, -10);
        blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(.1f);
        HUD.SetActive(false);
        compHUD.SetActive(false);
        menu.SetActive(false);
        startWipe.SetActive(true);
        for(float i = -.41f; i < 0; i += .001f)
        {
            Camera.main.transform.position = new Vector3(i, 0, -10);
            float alphaColor = 1 - ((i + .41f) / .41f);
            blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaColor);
            yield return new WaitForSeconds(.01f);
        }
        startWipe.SetActive(false);

        dialogue.SetActive(true);

        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("It's been a while since I've been here. Let's see, I need to visit the guild to sign up for the exam... Oh! That's right, I should also go talk to Theo about his apprentice too.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }


        Camera.main.orthographicSize = 1.828043f;
        //Update Quests
        HUD.SetActive(true);
        menu.SetActive(true);
        GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("New Objective added!\n<color=yellow>-Guild Exam</color>");
        //Add a new objective
        for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
        {
            if (QuestManager.activeMainQuests[i].questName == "Guild Exam")
            {
                QuestManager.activeMainQuests[i].objComplete[0] = true;
                QuestManager.activeMainQuests[i].questObj.Add("Talk to Byron at the Guild.");
                QuestManager.activeMainQuests[i].objComplete.Add(false);
            }
        }
        finished = true;
    }
}
