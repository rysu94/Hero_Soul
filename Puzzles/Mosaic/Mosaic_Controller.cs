using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosaic_Controller : MonoBehaviour
{
    //None - 0
    //Koros - 1
    public int levelID;

    public GameObject[] mosaic = new GameObject[9];

    public GameObject treasure, dialogue;

    public GameObject interactText;
    public bool textMade = false;
    bool active = false;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //Koros Forest
        if (levelID == 1)
        {
            int count = 0;
            for (int i = 0; i < Mosaic_Manager.korosMosaic.Length; i++)
            {
                if (Mosaic_Manager.korosMosaic[i])
                {
                    mosaic[i].SetActive(true);
                    count++;
                }
            }
            if(count >= 9)
            {
                treasure.SetActive(true);
            }
        }
        //Mier Forest
        else if(levelID == 2)
        {
            int count = 0;
            for(int i = 0; i < Mosaic_Manager.mierMosaic.Length; i++)
            {
                if(Mosaic_Manager.mierMosaic[i])
                {
                    mosaic[i].SetActive(true);
                    count++;
                }
            }
            if(count >= 9)
            {
                treasure.SetActive(true);
            }
        }
        //Galahad Tomb
        else if(levelID == 3)
        {
            int count = 0;
            for(int i = 0; i < Mosaic_Manager.tombMosaic.Length; i++)
            {
                if(Mosaic_Manager.tombMosaic[i])
                {
                    mosaic[i].SetActive(true);
                    count++;
                }
                if(count >= 9)
                {
                    treasure.SetActive(true);
                }
            }
        }
        //Dark Forest
        else if(levelID == 4)
        {
            int count = 0;
            for(int i = 0; i < Mosaic_Manager.darkMosaic.Length; i++)
            {
                mosaic[i].SetActive(true);
                count++;
            }
            if(count >= 9)
            {
                treasure.SetActive(true);
            }
        }

        float distance = Vector3.Distance(transform.position, TestCharController.player.transform.position);
        //print(distance);
        if (distance < .55 && !textMade)
        {
            interactText = Instantiate(Resources.Load("Prefabs/QuestionBubble") as GameObject, TestCharController.player.Find("Player_States_Panel").transform);
            textMade = true;
        }
        else if (distance >= .55)
        {
            Destroy(interactText);
            textMade = false;
        }

        //If interact is pressed
        if (distance < .55 && (InputManager.A_Button() || Input.GetKeyDown(KeyCode.F)) && !active && !TestCharController.inDialogue)
        {
            GameObject.Find("GenNoise").GetComponent<AudioSource>().Play();
            StartCoroutine(MosaicTut());
        }

        


    }
    IEnumerator MosaicTut()
    {
        active = true;
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("This looks like some sort of picture, maybe something will happen if I complete it.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        active = false;
    }
}
