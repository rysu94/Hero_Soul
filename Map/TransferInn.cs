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

    public string sceneName;
    public string direction;
    public float xCoord;
    public float yCoord;

    public string addAction = "";

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
            SceneManager.LoadScene(sceneName);
            LevelCreator.playerStartX = xCoord;
            LevelCreator.playerStartY = yCoord;
            LevelCreator.startTag = direction;
            GameObject.Find("BGM").GetComponent<AudioSource>().clip = normal;
            GameObject.Find("BGM").GetComponent<AudioSource>().Play();
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
            if(addAction != "")
            {
                StartCoroutine(addAction);
            }

        }

    }

    IEnumerator DisableArcana()
    {
        TestCharController.arcanaEnabled = false;
        yield return null;
    }

    IEnumerator BackRoutine()
    {
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);

        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I should put on my equipment before I leave.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
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
