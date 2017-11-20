using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Town_Event_3_1 : MonoBehaviour
{
    public GameObject dialogueController;
    public GameObject dialogueIMG;

    public GameObject tutorial;
    public Text tutorialTitle;
    public Text tutorialText;

    public static bool done = false;

    // Use this for initialization
    void Start ()
    {
	    if(Town_Event_3.start && !done)
        {
            StartCoroutine(StartEvent());
            done = true;
            TestCharController.arcanaEnabled = true;
            Town_Event_3.start = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = Camera.main.transform.position.z;
        Ray ray = new Ray(worldPoint, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.tag == "Tut_Close")
        {
            PotionController.hoverPotion = true;
            if (Input.GetMouseButtonDown(0))
            {
                tutorial.SetActive(false);
            }
        }
        else if (hit.collider == null || hit.collider.tag != "Tut_Close")
        {
            PotionController.hoverPotion = false;
        }
    }

    IEnumerator StartEvent()
    {
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Wow, I've never seen a monster so big.", "Cecilia/Cecilia Grey_thigh_2", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I may need to use some of my Arcana Cards on it.", "Cecilia/Cecilia Grey_thigh_1", "Cecilia", 1f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();
        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        tutorial.SetActive(true);
        tutorialTitle.text = "Tip: Arcana Combat";
        tutorialText.text = tutorial.GetComponent<TutorialDatabase>().tutorialList[4];
    }
}
