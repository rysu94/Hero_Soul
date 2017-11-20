using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransferTown : MonoBehaviour
{
    public AudioClip worldMapBGM;

    public GameObject dialogueController;
    public GameObject dialogueIMG;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!Town_Event_4.start)
            {
                SceneManager.LoadScene("World Map");
                GameObject.Find("BGM").GetComponent<AudioSource>().clip = worldMapBGM;
                GameObject.Find("BGM").GetComponent<AudioSource>().Play();
            }
            else
            {
                dialogueController.SetActive(true);
                dialogueIMG.SetActive(true);

                dialogueController.GetComponent<DialogueController>().Clear();
                dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I can't leave town right now!", "Cecilia/Cecilia Grey_thigh_3", "Cecilia", 1f));
                dialogueController.GetComponent<DialogueController>().StartDialogue();

                if(TestCharController.player.GetComponent<TestCharController>().north)
                {
                    TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
                }
                else if(TestCharController.player.GetComponent<TestCharController>().south)
                {
                    TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
                }
                else if(TestCharController.player.GetComponent<TestCharController>().east)
                {
                    TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
                }
                else if (TestCharController.player.GetComponent<TestCharController>().west)
                {
                    TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
                }
            }

        }
    }


}
