using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TransferInn : MonoBehaviour
{

    public AudioClip normal;

    public GameObject dialogueController;
    public GameObject dialogueIMG;

    public bool tutorialOn = false;
    public GameObject tutorial;
    public Text tutorialTitle;
    public Text tutorialText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InventoryManager.playerEquipment[0].itemID == 8 && InventoryManager.playerEquipment[3].itemID == 9 &&
                InventoryManager.playerEquipment[5].itemID == 10 && Town_Event_3.start == true)
            {
                SceneManager.LoadScene("Town_4");
                LevelCreator.playerStartX = 1.416f;
                LevelCreator.playerStartY = .469f;
                LevelCreator.startTag = "Down";
            }
            else
            {
                StartCoroutine(BackRoutine());
            }

            
            if(!Town_Event_3.start)
            {
                GameObject.Find("BGM").GetComponent<AudioSource>().clip = normal;
                GameObject.Find("BGM").GetComponent<AudioSource>().Play();
                GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
            }


        }
    }

    IEnumerator BackRoutine()
    {
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);

        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I should put on my equipment before I leave.", "Cecilia/Cecilia Grey_thigh_1", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
        TestCharController.player.GetComponent<Animator>().Play("TestDownIdle");
        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        if(!tutorialOn)
        {
            tutorial.SetActive(true);
            tutorialTitle.text = "Tip: Equipping Items";
            tutorialText.text = tutorial.GetComponent<TutorialDatabase>().tutorialList[5];
        }
        
    }
}
