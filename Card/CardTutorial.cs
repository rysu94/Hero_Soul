using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CardTutorial : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject dialogueImg;

    public GameObject tutMessage;
    public Text tutText;

    public static bool tut1 = false;

    // Use this for initialization
    void Start ()
    {
	    if(!Card_Interface.starterPack)
        {
            StartCoroutine(StartDialogue());
        }	
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator StartDialogue()
    {
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();

        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Welcome to my shop! I see you Guild voucher, congratulations for passing the exam. Passing the guild's exam grants you access to Arcana.", "NPC/NPC_Magic", "NPC/NPC_None", "NPC/NPC_None", "Shopkeeper", 1f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Arcana? What's that?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Eh? You don't know about Arcana? Sigh, allow me to explain.", "NPC/NPC_Magic", "NPC/NPC_None", "NPC/NPC_None", "Shopkeeper", 1f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("", "NPC/NPC_Magic", "NPC/NPC_None", "NPC/NPC_None", "Shopkeeper", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
    }
}
