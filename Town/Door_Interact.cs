using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Door_Interact : MonoBehaviour
{
    //The Canvas object prefab for the interact text
    public GameObject interactTextPrefab;

    public GameObject interactText;

    //Inn
    public GameObject shopHUD;
    public GameObject shopDialogue;
    //Alc
    public GameObject alcHUD;
    public GameObject alcDialogue;

    public Text shopkeeperName;
    public GameObject wipeScreen;


    public bool textMade = false;


    //1 = INN
    //2 = Alc Shop
    public int shopTag;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if(distance < .25 && !textMade)
        {
            interactText = Instantiate(interactTextPrefab, new Vector2(transform.position.x + .35f, transform.position.y+ .15f), Quaternion.identity);
            textMade = true;
        }
        else if(distance >= .25)
        {
            Destroy(interactText);
            textMade = false;
        }

        //Store Processing, INN
        if (distance < .25 && Input.GetKeyDown(KeyCode.F) && shopTag == 1 && !TestCharController.inDialogue)
        {
            wipeScreen.GetComponent<Animator>().Play("FadeIn");
            shopHUD.SetActive(true);
            shopDialogue.SetActive(true);
            TestCharController.inDialogue = true;
            shopkeeperName.text = "Innkeeper";
            shopDialogue.GetComponent<ShopDialogue>().dialogueList.Clear();
            shopDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Welcome to the Inn! What can I do for you?", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f));
            shopDialogue.GetComponent<ShopDialogue>().StartDialogue();
        }
        //store processing, Alc
        else if (distance < .25 && Input.GetKeyDown(KeyCode.F) && shopTag == 2 && !TestCharController.inDialogue)
        {
            wipeScreen.GetComponent<Animator>().Play("FadeIn"); wipeScreen.GetComponent<Animator>().Play("FadeIn");
            alcHUD.SetActive(true);
            alcDialogue.SetActive(true);
            TestCharController.inDialogue = true;
            shopkeeperName.text = "Alchemist";
            alcDialogue.GetComponent<ShopDialogue>().dialogueList.Clear();
            alcDialogue.GetComponent<ShopDialogue>().dialogueList.Add(new Dialogue("Welcome to my shop! How can I help you?", "NPC/NPC_Shopkeeper", "Shopkeeper", 0.9f));
            alcDialogue.GetComponent<ShopDialogue>().StartDialogue();
        }


    }
}
