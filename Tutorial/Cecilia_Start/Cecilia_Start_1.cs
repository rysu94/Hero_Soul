using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Start_1 : MonoBehaviour
{
    //1.828043
    public GameObject HUD, compHUD, menu;
    public GameObject playerChar;

    public GameObject sleepBubble;
    public GameObject ceciliaSleep;

    public GameObject blackMask;
    public GameObject systemMessage;
    public Text systemText;

    public GameObject dialogue, startWipe;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EventRoutine());   
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
    
    IEnumerator EventRoutine()
    {
        Camera.main.orthographicSize = 0.6810968f;
        Camera.main.transform.position = new Vector3(0, -.8f, -10);
        blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(.1f);
        HUD.SetActive(false);
        compHUD.SetActive(false);
        menu.SetActive(false);
        playerChar.SetActive(false);
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = 0;
       

        dialogue.SetActive(true);
        //dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I was always more of a tomboy growing up and never really fit in with other girls my age.", "Cecilia/Cecilia_kid", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You see I was never really interested in the traditional girly things, my dad was a pretty well known adventurer and ever since I was young I knew I wanted to be like him.", "Cecilia/Cecilia_kid", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("He would always bring smiles and happiness wherever he went. So, while other girls learned how to sew I learned how to wield a spear, and while they learned how to cook I learned how to dodge.", "Cecilia/Cecilia_Action_Thighup", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Dad, usually isn’t home but I hope to stand with him one day as a fellow member of the Guild.  Someday... I want to be a hero!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));

        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        HUD.SetActive(false);
        compHUD.SetActive(false);
        menu.SetActive(false);
        playerChar.SetActive(false);

        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        for (float i = 0; i < .1f; i += .01f)
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = i;
            yield return new WaitForSeconds(.25f);
        }
        
        startWipe.SetActive(true);
        blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        for (float i = -.4f; i < .25f; i += .001f)
        {
            Camera.main.transform.position = new Vector3(-0.51f, i, -10);
            float alphaColor = 1 - ((i + .4f) / .65f);
            blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaColor);
            yield return new WaitForSeconds(.01f);
        }
        startWipe.SetActive(false);
        sleepBubble.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(.5f);
        sleepBubble.SetActive(false);
        ceciliaSleep.GetComponent<Animator>().Play("Cecilia_Sleep_Awake");
        yield return new WaitForSeconds(1.5f);

        //Camera.main.orthographicSize = 1.828043f;
        //Camera.main.transform.position = new Vector3(0, 0, -10); 
        dialogue.SetActive(true);
        //dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Today is finally the day! Today, I take my first step in being an Adventurer just like dad! Just need to get ready and head to the guild.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        systemMessage.SetActive(false);
        systemMessage.SetActive(true);
        systemMessage.GetComponent<AudioSource>().Play();
        if(GameController.xbox360Enabled())
        {
            systemText.text = "Quest Basics:\nYou can check your current quests by pressing the Left DPad.";
        }
        else
        {
            systemText.text = "Interact Basics:\nYou can check your current quests by pressing the quest button in the top right corner.";
        }

        menu.SetActive(true);
        GameObject.Find("QuestManager").GetComponent<QuestManager>().AddQuest(QuestDatabase.ceQuest1);
        playerChar.SetActive(true);
        compHUD.SetActive(false);
        Camera.main.orthographicSize = 1.279958f;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Destroy(ceciliaSleep);

    }


}
