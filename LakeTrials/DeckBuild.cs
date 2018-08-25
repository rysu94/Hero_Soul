using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DeckBuild : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public GameObject dialogue;
    public GameObject dialogueImg;

    public bool deckUsed = false;
    // Use this for initialization
    void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        CheckDeckUsed();
        
        float distance = Vector3.Distance(transform.localPosition, TestCharController.player.transform.position);
        //print(distance);
        if (Byron2.phase3)
        {
            if (distance < .5 && !textMade)
            {
                interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
                textMade = true;
            }
            else if (distance >= .5)
            {
                Destroy(interactText);
                textMade = false;
            }

            if (distance < .5 && Input.GetKeyDown(KeyCode.F) && !TestCharController.inDialogue)
            {
                Destroy(interactText);
                textMade = false;
                Card_Interface.sceneName = "Training_2";
                Card_Interface.returnX = 1.01f;
                Card_Interface.returnY = -.784f;
                Card_Interface.startTag = "Up";
                SceneManager.LoadScene("DeckBuilder");
            }
        }
        else
        {
            if (interactText != null)
            {
                Destroy(interactText);
                textMade = false;
            }
        }

        if (deckUsed)
        {
            if (distance < .5 && !textMade)
            {
                interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
                textMade = true;
            }
            else if (distance >= .5)
            {
                Destroy(interactText);
                textMade = false;
            }

            if (distance < .5 && Input.GetKeyDown(KeyCode.F) && !TestCharController.inDialogue)
            {
                Destroy(interactText);
                textMade = false;
                Deck.ResetDeck();
                deckUsed = false;
            }
        }
    }

    void CheckDeckUsed()
    {
        int tempInt = 0;
        for(int i = 0; i < Deck.usedCards.Length; i++)
        {
            if(Deck.usedCards[i])
            {
                tempInt++;
            }
        }

        //if deck is all used
        if(Deck.cardsLeft == 0 && !deckUsed)
        {
            StartCoroutine(UsedAllCards());
        }
    }

    IEnumerator UsedAllCards()
    {
        deckUsed = true;

        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Looks like you used all your cards, you can recharge them at the spellbook, but please don't waste them. Usually you can only recharge them after leaving a dungeon. Please try to be more careful.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Sorry.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

    }
}
