using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cecilia_Start_Byron : MonoBehaviour
{
    public GameObject HUD, compHUD, menu;
    public GameObject blackMask;
    public GameObject systemMessage;
    public Text systemText;

    public GameObject dialogue;

    public GameObject byron;

    public static bool finished = false;

    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public Button yes, no;
    public int decision = -1;

    // Use this for initialization
    void Start()
    {
        yes.onClick.AddListener(Yes);
        no.onClick.AddListener(No);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, TestCharController.player.transform.position);
        if (distance < .5 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load<GameObject>("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            else
            {
                interactText = Instantiate(Resources.Load<GameObject>("Prefabs/Interact"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .5)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .5 && Input.GetKeyDown(KeyCode.F) && !TestCharController.inDialogue)
        {
            StartCoroutine(TalkRoutine());
        }
    }

    IEnumerator TalkRoutine()
    {
        bool apprenticeQuest = false;
        bool magicQuest = false;
        //Check if the apprentice quest is still active
        for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
        {
            if(QuestManager.activeMainQuests[i].questName == "The Apprentice")
            {
                apprenticeQuest = true;
                break;
            }
        }

        //Check if the magic quest is still active
        for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
        {
            if (QuestManager.activeMainQuests[i].questName == "It's Magic")
            {
                magicQuest = true;
                break;
            }
        }

        if (apprenticeQuest && !magicQuest)
        {
            dialogue.SetActive(true);
            dialogue.GetComponent<DialogueController>().resetChar = false;
            dialogue.GetComponent<DialogueController>().noFade = true;
            dialogue.GetComponent<DialogueController>().noMove = false;
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Ah, Cecilia glad you're here! Are you ready for the exam?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I sure am. I ready to join the guild just like dad!", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, 0));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("That's the spirit! There is still a little time before the exam starts, take care of any business you have in Weiss the exam will take the whole day.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Oh that reminds me, your father left you a little something.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("What? Dad did?", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, 0));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yup, I've been holding on to it waiting this day. Hehe, I guess 'ole Cyrus was on the mark, here you are 10 years later following in his stead.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Of course I am! Now, what did he leave me?", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, 0));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Let's see now... Here you go!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Ehh, a piece of paper?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, 0));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Not just any piece of paper this is an Arcana Voucher. This will get you a starter set of Arcana. Meet Fiona at the Magic Shop and she'll get you started.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));

            dialogue.GetComponent<DialogueController>().StartDialogue();

            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }

            //Add the Arcana Voucher
            bool itemFound = false;
            for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
            {
                if(InventoryManager.playerInventory[i].itemID == 29)
                {
                    itemFound = true;
                }
            }
            if(!itemFound)
            {
                //Find an empty slot
                int invSlot = 0;
                for(int i = 0; i < InventoryManager.playerInventory.Length; i++)
                {
                    if(InventoryManager.playerInventory[i] == GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(0))
                    {
                        invSlot = i;
                        break;
                    }
                }
                InventoryManager.playerInventory[invSlot] = GameObject.Find("InventoryController").GetComponent<ItemDatabase>().FindItem(29);
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
                tempObj.GetComponent<Text>().text = "Picked up " + GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[29].itemName;
                GameObject.Find("QuestManager").GetComponent<QuestManager>().AddQuest(QuestDatabase.ceQuest3);
            }


        }
        else if(!apprenticeQuest && !magicQuest)
        {
            dialogue.SetActive(true);
            dialogue.GetComponent<DialogueController>().resetChar = false;
            dialogue.GetComponent<DialogueController>().noFade = true;
            dialogue.GetComponent<DialogueController>().noMove = false;
            dialogue.GetComponent<DialogueController>().waitingDecision = true;
            dialogue.GetComponent<DialogueController>().choicePrompt.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();

            Dialogue tempDia = new Dialogue("The exam is about to start, are you ready?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2);
            tempDia.dialogueAction = AddDecision();

            dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);

            dialogue.GetComponent<DialogueController>().StartDialogue();

            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }

        }
        else
        {
            dialogue.SetActive(true);
            dialogue.GetComponent<DialogueController>().resetChar = false;
            dialogue.GetComponent<DialogueController>().noFade = true;
            dialogue.GetComponent<DialogueController>().noMove = false;
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You still have some time before the exam starts, make sure you take care of any business you have in town.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));

            dialogue.GetComponent<DialogueController>().StartDialogue();

            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }
        }
    }

    IEnumerator AddDecision()
    {
        //yes
        if (decision == 1)
        {
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I'm ready.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, 0));


            Dialogue tempDia = new Dialogue("Good! Alright everybody here for the Guild Exam follow me. We are about to start the written portion of the exam.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2);
            tempDia.dialogueAction = FadeIn();

            dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);

        }
        //no
        else if (decision == 2)
        {
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Not yet.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, 0));
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see, you still have a little time but hurry back.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));
        }
        yield return null;
    }

    void Yes()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        decision = 1;
        dialogue.GetComponent<DialogueController>().waitingDecision = false;
        dialogue.GetComponent<DialogueController>().choicePrompt.SetActive(false);
    }

    void No()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        decision = 2;
        dialogue.GetComponent<DialogueController>().waitingDecision = false;
        dialogue.GetComponent<DialogueController>().choicePrompt.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("FadeOut");
        for(float i = .1f; i > 0; i -= .01f)
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = i;
            yield return new WaitForSeconds(.4f);
        }
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("BlankScreen");
        SceneManager.LoadScene("Cecilia_Town_Evil");
        LevelCreator.playerStartX = -2.3f;
        LevelCreator.playerStartY = 0;
        LevelCreator.startTag = "Right";
    }
}
