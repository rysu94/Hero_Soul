using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DummyMarker : MonoBehaviour
{
    public GameObject inventory;

    public GameObject dialogue;
    public GameObject dialogueImg;

    public GameObject tutMessage;
    public Text tutText;

    public GameObject arrowRight;
    public GameObject rope;
    public static bool ropeOn = true;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!inventory.activeInHierarchy && TutorialDatabase.tut3 && !TutorialDatabase.tut3_A && InventoryManager.playerEquipment[0].itemID != 0)
        {
            TutorialDatabase.tut3_A = true;
            StartCoroutine(StartDialogue());
        }

        if (!ropeOn)
        {
            rope.SetActive(false);
        }
    }

    IEnumerator StartDialogue()
    {
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hmm, a spear huh? Interesting, go to the practice dummy and show me your skills.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        ropeOn = false;
        rope.SetActive(false);
        arrowRight.SetActive(true);
    }
}
