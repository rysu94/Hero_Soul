using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Magic_Shop : MonoBehaviour
{
    public GameObject dialoguePrompt;
    public Button shop, talk;
    public Button back;

    public GameObject decisionPrompt;
    public Button yes, no;

    public GameObject questBubble, shopBubble;

    //Card Collector I not started
    public static bool phase0 = true;
    //Card Collector I accepted
    public static bool phase1 = false;
    //Card Collector II not started, I complete
    public static bool phase2 = false;
    //Card Collector II accepted
    public static bool phase3 = false;
    //Card Collector III not started, II complete
    public static bool phase4 = false;
    //Card Collector III accepted
    public static bool phase5 = false;

    public GameObject dialogue;
    public GameObject dialogueImg;
    public GameObject dialogueBackImg;
    public GameObject dialogueNPC;
    public GameObject dialogueFade;

    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public int decision = 0;

    public GameObject shopHud, quest;

    public GameObject magicBook, NPC, dialogueBox;

    public GameObject doorBlocker;

    // Use this for initialization
    void Start()
    {
        UpdateBubble();
        CheckQuest();

        shop.onClick.AddListener(Shop);
        back.onClick.AddListener(Back);
        talk.onClick.AddListener(Talk);

        yes.onClick.AddListener(Yes);
        no.onClick.AddListener(No);

        //Add a new objective
        for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
        {
            if (QuestManager.activeMainQuests[i].questName == "It's Magic")
            {
                if(QuestManager.activeMainQuests[i].objComplete.Count > 1 && QuestManager.activeMainQuests[i].objComplete[1])
                {
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("Quest Complete!\n<color=yellow>- It's Magic</color>");
                    QuestManager.activeMainQuests[i].DistributeReward();
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().RemoveQuest("It's Magic");
                    GameObject.Find("QuestManager").GetComponent<QuestManager>().UpdateQuestList();
                    StartCoroutine(TutorialFinishRoutine());
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), TestCharController.player.transform.position);
        //print(distance);
        if (distance < .5 && !textMade)
        {
            if(GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox"), new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity) as GameObject;
            }
            else
            {
                interactText = Instantiate(interactPrefab, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            
            textMade = true;
        }
        else if (distance >= .5)
        {
            Destroy(interactText);
            textMade = false;
        }

        if (distance < .5 && InputManager.A_Button() && !TestCharController.inDialogue)
        {
            bool magicQuest = false;
            //Check if the magic quest is still active
            for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
            {
                if (QuestManager.activeMainQuests[i].questName == "It's Magic")
                {
                    magicQuest = true;
                    break;
                }
            }

            if(!magicQuest)
            {
                StartCoroutine(ShopRoutine());
            }
            else
            {
                StartCoroutine(CeciliaTalkRoutine());
            }

            
        }
    }

    IEnumerator CeciliaTalkRoutine()
    {
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = false;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("How can I help you?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hi, uh, I recieved this voucher for a starter set or Arcana?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Voucher? Sorry we don't accept vouchers.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Uh, Byron from the Adventurer Guild gave it to me, said it was a gift from my father.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hmm... Ah! I remember giving one out to Cyrus years ago... Ah! Then you must be-", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Cecilia, his daughter, yup that's me.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("To think Cyrus would settle down and have a squirt like you... Ha! Sure I'll give you a starter set, I owe your old man a favor anyways.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Since you're new to Arcana, let me explain the basics to you. Come, step over to the Arcana Book with me.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        magicBook.SetActive(true);
        Destroy(interactText);
        textMade = false;
        NPC.transform.position = new Vector2(1.435f, -0.734f);
        GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("New Objective added!\n<color=yellow>-It's Magic</color>");
        //Add a new objective
        for (int i = 0; i < QuestManager.activeMainQuests.Count; i++)
        {
            if (QuestManager.activeMainQuests[i].questName == "It's Magic")
            {
                QuestManager.activeMainQuests[i].objComplete[0] = true;
                QuestManager.activeMainQuests[i].questObj.Add("Use the Arcana Book.");
                QuestManager.activeMainQuests[i].objComplete.Add(false);
            }
        }
        doorBlocker.SetActive(true);
        dialogueBox.SetActive(false);
        gameObject.SetActive(false);
    }

    IEnumerator TalkRoutine()
    {
        decision = 0;
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);

        CheckQuest();

        dialogue.GetComponent<DialogueController>().Clear();
        //Card Collector 1
        if (phase0)
        {
            //decisionPrompt.SetActive(true);
            dialogue.GetComponent<DialogueController>().waitingDecision = true;
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Sigh, I wish I could study Arcana more. If only I wasn't stuck in this shop... Wait you adventurer! Could you show me the new Arcana Cards you discover in your travels? What do you say?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", "Fiona", .9f, 2));
        }
        else if(phase1)
        {
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Have you found 10 unique Arcana Cards yet? I'm dying to see what you find!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", "Fiona", .9f, 2));
        }
        else if(phase2)
        {
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You have the Arcana? Good let me see... Excellent work!", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", "Fiona", .9f, 2));
        }
        dialogue.GetComponent<DialogueController>().showShop = false;
        dialogue.GetComponent<DialogueController>().showChoice = true;

        dialogue.GetComponent<DialogueController>().choicePrompt.GetComponent<Dialogue_Choice>().main = gameObject;
        dialogue.GetComponent<DialogueController>().StartDialogue();
        

        while (decision == 0 && (phase0 || phase3 || phase4))
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
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You'll help me? Thanks!", "NPC/NPC_Magic", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", .9f, 2));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            
            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }

            if(phase0)
            {
                phase0 = false;
                phase1 = true;              
                quest.GetComponent<QuestManager>().AddQuest(QuestDatabase.testSideQuest2);
                UpdateBubble();
            }

        }
        else if(decision == 2)
        {
            decision = 0;
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see, come back if you change your mind.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", "Fiona", .9f, 2));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            
            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }
            StartCoroutine(ShopRoutine());
        }



        if (phase2)
        {
            phase2 = false;
            phase3 = true;
            UpdateBubble();

            GameObject.Find("QuestManager").GetComponent<QuestManager>().PrintSystemMessage("Quest Complete!\n<color=yellow>-Card Collector I</color>");

            for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
            {
                if (QuestManager.activeSideQuests[i].questName == "Card Collector I")
                {
                    QuestManager.activeSideQuests[i].DistributeReward();
                }
            }

            GameObject.Find("QuestManager").GetComponent<QuestManager>().RemoveQuest("Card Collector I");
            GameObject.Find("QuestManager").GetComponent<QuestManager>().UpdateQuestList();
        }

    }


    IEnumerator ShopRoutine()
    {
        decision = 0;
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);

        dialogue.GetComponent<DialogueController>().waitingDecision = true;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("How Can I help you?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Magic", "Fiona", .9f, 2));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        dialogue.GetComponent<DialogueController>().showChoice = false;
        dialogue.GetComponent<DialogueController>().showShop = true;

        dialogue.GetComponent<DialogueController>().shopPrompt.GetComponent<Dialogue_Choice>().main = gameObject;

        while (decision == 0)
        {
            yield return null;
        }

        while (!dialogue.GetComponent<DialogueController>().waitingDecision && !dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        if (decision == 1)
        {
            decision = 0;
            Shop_Controller.shopIndex = 1;
            shopHud.SetActive(true);
            GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;

        }
        else if (decision == 2)
        {
            decision = 0;
            yield return StartCoroutine(TalkRoutine());
        }

        else if (decision == 3)
        {
            decision = 0;
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Another time then.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", .9f, -1));
            dialogue.GetComponent<DialogueController>().StartDialogue();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;
            while (!dialogue.GetComponent<DialogueController>().dialogueDone)
            {
                yield return null;
            }
        }


    }

    void Shop()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        decision = 1;
        dialoguePrompt.SetActive(false);
        StartCoroutine(dialogue.GetComponent<DialogueController>().EndDialogue());
    }

    void Talk()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        decision = 2;
        dialoguePrompt.SetActive(false);
    }

    void Back()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        decision = 3;
        dialoguePrompt.SetActive(false);
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


    void UpdateBubble()
    {
        if (phase0 || phase3)
        {
            questBubble.SetActive(true);
            shopBubble.SetActive(false);
        }
        else
        {
            questBubble.SetActive(false);
            shopBubble.SetActive(true);
        }
    }


    void CheckQuest()
    {
        for (int i = 0; i < QuestManager.activeSideQuests.Count; i++)
        {
            if (QuestManager.activeSideQuests[i].questName == "Card Collector I")
            {
                if(QuestManager.activeSideQuests[i].questObj.Count >= 2)
                {
                    phase1 = false;
                    phase2 = true;
                }
            }
            else if(QuestManager.activeSideQuests[i].questName == "Card Collector II")
            {
                if (QuestManager.activeSideQuests[i].questObj.Count >= 2)
                {

                }
            }
            else if (QuestManager.activeSideQuests[i].questName == "Card Collector III")
            {
                if (QuestManager.activeSideQuests[i].questObj.Count >= 2)
                {

                }
            }
        }


    }

    IEnumerator TutorialFinishRoutine()
    {
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = false;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().waitingDecision = true;
        dialogue.GetComponent<DialogueController>().choicePrompt.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();

        Dialogue tempDia = new Dialogue("There you have it you've made your first Arcana Deck! Since it's your first time using Arcana would you like me to give you some pointers?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2);
        tempDia.dialogueAction = ShowTutorialChoices();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);
       
       
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
    }

    IEnumerator ShowTutorialChoices()
    {
        //yes
        if (decision == 1)
        {
            Dialogue tempDia = new Dialogue("Great, let's go downstairs.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2);
            tempDia.dialogueAction = TransferToTutorial();

            dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = false;
            TestCharController.arcanaEnabled = true;
            TestCharController.inDialogue = false;
            SceneManager.LoadScene("Cecilia_Town_MageBase");
            LevelCreator.playerStartX = 1.018f;
            LevelCreator.playerStartY = 0.979f;
            LevelCreator.startTag = "Down";
        }
        //no
        else if (decision == 2)
        {
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see more of a try and see type, that's fine too. Good luck on your exam.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Magic", "Fiona", 1f, 2));           
        }
        yield return null;
    }

    IEnumerator TransferToTutorial()
    {
        yield return null;
    }
}
