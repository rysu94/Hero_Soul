using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Town_Event_2 : MonoBehaviour
{
    public static bool start = false;
    public static bool running = false;
    public GameObject cam;

    public GameObject dialogueController;
    public GameObject dialogueIMG;

    public GameObject wipe;

    public GameObject bell;

    public AudioClip normal;

    public GameObject tutorial;
    public Text tutorialTitle;
    public Text tutorialText;

	// Use this for initialization
	void Start ()
    {
        if(Town_Event_1.start)
        {
            Town_Event_1.start = false;
            SceneManager.LoadScene("Town_5_Instance");
        }

	    if(start)
        {
            start = false;
            if(!running)
            {
                StartCoroutine(StartEvent());
                running = false;
            }
        }
        else
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().clip = normal;
            GameObject.Find("BGM").GetComponent<AudioSource>().Play();
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if(hit.collider != null && hit.collider.tag == "Tut_Close")
        {
            PotionController.hoverPotion = true;
            if(Input.GetMouseButtonDown(0))
            {
                tutorial.SetActive(false);
            }
        }
        else if(hit.collider == null || hit.collider.tag != "Tut_Close")
        {
            PotionController.hoverPotion = false;
        }
    }

    IEnumerator CamPan()
    {
        cam.transform.position = new Vector3(0, -.75f, -10);
        for (float i = -.75f; i < 0; i += .001f)
        {
            cam.transform.position = new Vector3(0, i, -10);
            yield return new WaitForSeconds(.005f);
        }
    }

    IEnumerator StartEvent()
    {
        wipe.SetActive(true);
        bell.SetActive(true);
        StartCoroutine(CamPan());
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);

        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Help they're everywhere!", "NPC/NPC_None", "NPC/NPC_None", "Cecilia/Cecilia Grey_thigh_1", "Villager", 0.9f, -1));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Everyone please stay calm! The guard have this under control!", "NPC/NPC_None", "NPC/NPC_None", "Cecilia/Cecilia Grey_thigh_1", "Guard", 0.9f, -1));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hmm, looks like something is happening in town. I should should probably see what's going on, even if I'm not an official member of the Guild yet.", "NPC/NPC_None", "NPC/NPC_None", "Cecilia/Cecilia Grey_thigh_1", "Cecilia", 1f, -1));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Oh, right but before that, I stored by gear in the stash here at the Inn. I should equip it before getting into trouble.", "NPC/NPC_None", "NPC/NPC_None", "Cecilia/Cecilia Grey_thigh_1", "Cecilia", 1f, -1));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        tutorial.SetActive(true);
        tutorialTitle.text = "Tip: Player Stash";
        tutorialText.text = tutorial.GetComponent<TutorialDatabase>().tutorialList[0];
        

        Town_Event_3.start = true;
    }
}
