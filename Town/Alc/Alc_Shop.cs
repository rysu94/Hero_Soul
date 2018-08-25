using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Alc_Shop : MonoBehaviour
{
    public GameObject dialoguePrompt;
    public Button yes;
    public Button no;

    public GameObject dialogue;
    public GameObject dialogueImg;
    public GameObject dialogueBackImg;
    public GameObject dialogueNPC;
    public GameObject dialogueFade;

    public GameObject interactText;
    public GameObject interactPrefab;
    public bool textMade = false;

    public int decision = 0;

    public GameObject shopHud;

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
            StartCoroutine(TalkRoutine());
        }
    }

    IEnumerator TalkRoutine()
    {
        decision = 0;
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);

        dialogue.GetComponent<DialogueController>().waitingDecision = true;

        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Would you like to browse my wares?", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo", 1f, -1));
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

        if (decision == 1)
        {
            decision = 0;
            Shop_Controller.shopIndex = 2;
            shopHud.SetActive(true);
            GameObject.Find("ShopController").GetComponent<Shop_Controller>().UpdateShop();
            dialogue.GetComponent<DialogueController>().waitingDecision = false;

        }
        else if (decision == 2)
        {
            decision = 0;
            dialogue.SetActive(true);
            dialogueImg.SetActive(true);
            dialogue.GetComponent<DialogueController>().Clear();
            dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Another time then.", "Cecilia/Cecilia Grey_thigh_1", "Leon/Leon Klein_thigh_1", "NPC/NPC_Alc", "Theo", 1f, -1));
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
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        decision = 1;
        dialoguePrompt.SetActive(false);
        dialogue.SetActive(false);
        dialogueImg.SetActive(false);
        dialogueNPC.SetActive(false);
        dialogueBackImg.SetActive(false);
        dialogueFade.SetActive(false);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
    }

    void No()
    {
        GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
        decision = 2;
        dialoguePrompt.SetActive(false);
    }
}
