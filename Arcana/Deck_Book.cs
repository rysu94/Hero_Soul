using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Deck_Book : MonoBehaviour
{
    public GameObject dialoguePrompt;
    public Button yes;
    public Button no;

    public GameObject dialogue;
    public GameObject dialogueImg;

    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public int decision = 0;

    // Use this for initialization
    void Start ()
    {
        yes.onClick.AddListener(Yes);
        no.onClick.AddListener(No);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), TestCharController.player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity) as GameObject;
            }
            else
            {
                interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .35 && InputManager.A_Button() && !TestCharController.inDialogue)
        {
            StartCoroutine(TalkRoutine());
        }
    }

    IEnumerator TalkRoutine()
    {
        decision = 0;
        dialogue.SetActive(true);
        //dialogueImg.SetActive(true);

        dialogue.GetComponent<DialogueController>().waitingDecision = true;

        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Would you like to edit your Arcana Deck?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        dialoguePrompt.SetActive(true);

        while (decision == 0)
        {
            yield return null;
        }

        while (!dialogue.GetComponent<DialogueController>().dialogueDone && !dialogue.GetComponent<DialogueController>().waitingDecision)
        {
            yield return null;
        }

        if(decision == 1)
        {
            decision = 0;
            Card_Interface.returnX = TestCharController.player.transform.position.x;
            Card_Interface.returnY = TestCharController.player.transform.position.y;
            Card_Interface.sceneName = SceneManager.GetActiveScene().name;
            if(TestCharController.player.GetComponent<TestCharController>().north)
            {
                Card_Interface.startTag = "Up";
            }
            else if(TestCharController.player.GetComponent<TestCharController>().south)
            {
                Card_Interface.startTag = "Down";
            }

            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            SceneManager.LoadScene("DeckBuilder");

        }
        else if(decision == 2)
        {
            decision = 0;
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Another time then.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", "Fiona", 1f, 2));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }
        }

    }

    void Yes()
    {
        decision = 1;
        dialoguePrompt.SetActive(false);
    }

    void No()
    {
        decision = 2;
        dialoguePrompt.SetActive(false);
    }
}
