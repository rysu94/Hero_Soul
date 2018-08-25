using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Quest : MonoBehaviour
{
    public static bool questAccepted = false;
    public GameObject quest, dialogue;

	// Use this for initialization
	void Start ()
    {
        if(!questAccepted)
        {
            StartCoroutine(AddQuest());
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator AddQuest()
    {
        yield return new WaitForSeconds(2.01f);
        /*
        dialogue.SetActive(true);
        //dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Welcome to the Koros Forest!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "Puck/Puck_emotions_1", "Puck", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("So, this is what they call a \"Living Dungeon\", huh?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "Puck/Puck_emotions_1", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Somehow I was expecting something a bit more menacing. This just looks like normal forest.", "Cecilia/Cecilia Grey_thigh_7", "Leon/Leon Klein_thigh_1", "Puck/Puck_emotions_1", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Well, I wouldn’t say normal. We’ve got reports from merchants that the monsters in this forest have grown agitated recently. We should proceed with caution.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "Puck/Puck_emotions_1", "Leon", .9f, 1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Not to mention that dungeon’s layout is always changing.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "Puck/Puck_emotions_1", "Puck", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see, so that’s why it’s called a \"living dungeon\"!", "Cecilia/Cecilia Grey_thigh_7", "Leon/Leon Klein_thigh_1", "Puck/Puck_emotions_1", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yup! It won’t be same place every time we come here.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "Puck/Puck_emotions_7", "Puck", 1f, 2));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        */
        questAccepted = true;
        quest.GetComponent<QuestManager>().AddQuest(QuestDatabase.testQuest);
    }
}
