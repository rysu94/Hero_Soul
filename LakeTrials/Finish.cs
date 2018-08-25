using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Finish : MonoBehaviour
{

    public GameObject dialogue;
    public GameObject dialogueImg;

    public Text timer;

    public GameObject arrowRight;
    public GameObject arrowRight_2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Timer.started && collision.gameObject.tag == "Player")
        {
            StartCoroutine(SlidePlayer());
        }
        else if(Timer.started)
        {
            Timer.finished = true;
            Timer.started = false;
            timer.text = "";
            if (Timer.time > 0)
            {
                GameObject.Find("PassNoise").GetComponent<AudioSource>().Play();
                StartCoroutine(PlayerPass());
            }
            else
            {
                GameObject.Find("FailNoise").GetComponent<AudioSource>().Play();
                StartCoroutine(PlayerFail());
            }

            
        }
    }

    IEnumerator SlidePlayer()
    {
        TestCharController.inDialogue = true;
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Please enter the course up here.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        arrowRight_2.SetActive(true);
        TestCharController.inDialogue = false;
        
    }

    IEnumerator PlayerPass()
    {
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You finished the course in " + (10 - Timer.time).ToString("F1") + " seconds. Congratulations you passed the first test, go ahead and move on to the next one.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yes! One test down, two more to go.", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Next applicant please!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = true;
        GameObject.Find("RoomDone").GetComponent<AudioSource>().Play();
        arrowRight.SetActive(true);
    }

    IEnumerator PlayerFail()
    {
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You finished the course a little bit too slow. Try again.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
    }
}
