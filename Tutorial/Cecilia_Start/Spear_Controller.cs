using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Spear_Controller : MonoBehaviour
{
    public GameObject interactText;
    public GameObject interactPrefab;

    public bool textMade = false;
    public GameObject player, spearBox;

    public GameObject dialogue;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //print(distance);
        if (distance < .35 && !textMade)
        {
            if (GameController.xbox360Enabled())
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact_XBox") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            else
            {
                interactText = Instantiate(Resources.Load("Prefabs/Interact") as GameObject, new Vector2(transform.position.x + .35f, transform.position.y + .15f), Quaternion.identity);
            }
            textMade = true;
        }
        else if (distance >= .35)
        {
            Destroy(interactText);
            textMade = false;
        }

        //If interact is pressed
        if (distance < .35  && (InputManager.A_Button() || Input.GetKeyDown(KeyCode.F)))
        {
            GetComponent<AudioSource>().Play();
            Destroy(interactText);
            spearBox.SetActive(false);
            StartCoroutine(SpearTalk());          
        }
    }

    IEnumerator SpearTalk()
    {
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = true;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = false;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("This was the spear my father gave me. I feel with this I can accomplish anything.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_None", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Console Output"), GameObject.Find("Console").transform, false);
        tempObj.GetComponent<Text>().text = "Obtained <color=yellow>Worn Spear</color>";
        Destroy(gameObject);
    }
}
