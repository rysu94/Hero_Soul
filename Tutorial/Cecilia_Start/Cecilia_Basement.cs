using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Basement : MonoBehaviour
{
    public GameObject dialogue;

    public GameObject systemMessage;
    public Text systemText;

    // Use this for initialization
    void Start ()
    {
        Deck.ResetDeck();

        StartCoroutine(StartRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator StartRoutine()
    {
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = false;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Welcome to.. well, the basement of my shop. I've set up some dummies for you to hit with your spells. So first off let's start off with the basics of Arcana.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Arcana is the base elements everything in the world is made of. Fire, water, earth, wind, life, those are the 5 base elements of Arcana.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("When something is killed, it is broken down into its base Arcana. Using this Arcana, the ancients created Magick which harnessed the great innate power within the base Arcana elements.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("The ancients sealed this power in Cards which we can use to release this great power. The starter set of 15 Cards I gave you is called a Deck. You may edit your Deck with new Cards that you find or craft.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("For starters, your Deck size is limited to 15. This is a safty precaution because holding too many Cards when you're not ready can cause harm to the user.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Don't worry, I'm sure this number will increase as you get stronger! To use your spells, you first need to Prime your Cards. You may only have 3 Primed Cards at once and the order in which they Prime is random.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("To use a Primed Card you must use Mana and once a Primed Card is used it will be exhausted for a while, meaning you can't use it again for a little bit.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hmmm.. I guess those are the basics. Go ahead and try it out! If you would like to refresh your exhausted cards, you can use the rune over there on the floor to refresh them.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        StartCoroutine(EnwaterRoutine());
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Arcana Basics:\nYou can see your primed cards and remaining cards in the bottom left of your HUD.";
        }
        else
        {
            systemText.text = "Arcana Basics:\nYou can see your primed cards and remaining cards in the bottom left of your HUD.";
        }

        yield return new WaitForSeconds(11f);

        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Arcana Basics:\nYou can select which primed card you want to use by using the [LBump] & [RBump].";
        }
        else
        {
            systemText.text = "Arcana Basics:\nYou can select which primed card you want to use by using the [MMW] or the [1][2][3] keys.";
        }

        yield return new WaitForSeconds(11f);

        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Arcana Basics:\nTo cast your selected card press the [B] button.";
        }
        else
        {
            systemText.text = "Arcana Basics:\nTo cast your selected card press the [MMB] or [Q] button.";
        }

        yield return new WaitForSeconds(11f);

        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Arcana Basics:\nThe blue orbs in the top left of the HUD is your mana.";
        }
        else
        {
            systemText.text = "Arcana Basics:\nThe blue orbs in the top left of the HUD is your mana.";
        }

        yield return new WaitForSeconds(11f);

        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if (GameController.xbox360Enabled())
        {
            systemText.text = "Arcana Basics:\nThe cost of spells can be seen on the cards.";
        }
        else
        {
            systemText.text = "Arcana Basics:\nThe cost of spells can be seen on the cards.";
        }
    }

    IEnumerator EnwaterRoutine()
    {
        while(true)
        {
            GameObject.Find("States").GetComponent<StateManager>().AddState(30, 11, 6, false);
            yield return new WaitForSeconds(30f);
        }
    }
}
