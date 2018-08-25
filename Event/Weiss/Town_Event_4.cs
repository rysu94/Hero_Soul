using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town_Event_4 : MonoBehaviour
{
    public static bool start;

    public static int clearCount;

    public GameObject enemy;

    public GameObject bossDoor1;
    public GameObject bossDoor2;
    public GameObject bossDoor3;

    public GameObject dialogueController;
    public GameObject dialogueIMG;

    public static bool talk = false;

    void Awake()
    {
        if(start && !LevelCreator.levelGrid[LevelCreator.playerCurrentX,LevelCreator.playerCurrentY].roomClear)
        {
            enemy.SetActive(true);
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyList.Clear();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyList.Add(enemy);
            }
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyCount = LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].enemyList.Count;
        }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(start)
        {
            clearCount = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (LevelCreator.levelGrid[i, j].roomClear)
                    {
                        clearCount++;
                    }
                }
            }

            if (clearCount < 8 && bossDoor1 != null)
            {
                bossDoor1.SetActive(true);
                bossDoor2.SetActive(true);
                bossDoor3.SetActive(true);
            }
            else if (clearCount >= 8 && bossDoor1 != null)
            {
                if(!talk)
                {
                    StartCoroutine(TalkRoutine());
                    talk = true;
                }
                bossDoor1.SetActive(false);
                bossDoor2.SetActive(false);
                bossDoor3.SetActive(false);
            }
        }
    }

    IEnumerator TalkRoutine()
    {
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Sounds like something is going on in the town square. I should go check it out.", "Cecilia/Cecilia Grey_thigh_2", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, -1));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        Town_Event_5.start = true;
    }
}
