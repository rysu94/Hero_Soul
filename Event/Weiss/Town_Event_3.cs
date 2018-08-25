using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Town_Event_3 : MonoBehaviour
{
    public GameObject dialogueController;
    public GameObject dialogueIMG;

    public static bool start = false;

    public GameObject enemies;

    public GameObject wipe;

    public GameObject tutorial;
    public Text tutorialTitle;
    public Text tutorialText;

    void Awake()
    {
        if (start)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    LevelCreator.levelGrid[i, j].roomClear = false;
                }
            }
            wipe.SetActive(true);

            StartCoroutine(StartEvent());

            enemies.SetActive(true);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyList.Add(enemy);
            }
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyList.Count;
            
            Town_Event_4.start = true;




        }
    }

	// Use this for initialization
	void Start ()
    {

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
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Monsters? No way, how did monsters get into town?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, -1));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("No time for that, got to get rid of them!", "Cecilia/Cecilia Grey_thigh_3", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, -1));
        dialogueController.GetComponent<DialogueController>().StartDialogue();
        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        tutorial.SetActive(true);
        tutorialTitle.text = "Tip: Basic Combat";
        tutorialText.text = tutorial.GetComponent<TutorialDatabase>().tutorialList[3];

    }


}
