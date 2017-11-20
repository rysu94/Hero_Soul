using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Text dialogueText;
    public Text dialogueName;
    public Image dialogueIMG;
    public Image scrollArrow;

    public List<Dialogue> dialogueList = new List<Dialogue>();

    public Coroutine writeText;

    public string dialogue;

    public AudioSource dialogueNoise;

    //Flag if player chooses to skip dialogue
    public bool dialogueSkip = false;
    //Flag for if the dialogue is waiting for the player
    public bool dialogueWait = false;

    public float speedFactor;

    //Flag for whether or not the dialogue is done writing
    public bool dialogueDone = false;

    // Use this for initialization
    void Start ()
    {
        //StartDialogue();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Checks if the player wants to skip dialogue
        if(Input.GetMouseButtonDown(0) && !dialogueSkip && !dialogueWait)
        {
            dialogueSkip = true;
        }

        if (Input.GetMouseButton(1))
        {
            speedFactor = 0.25f;
        }
        else
        {
            speedFactor = 1;
        }
    }

    IEnumerator WriteText(List<Dialogue> dialogueList)
    {
        dialogueDone = false;
        dialogueIMG.gameObject.SetActive(true);
        TestCharController.inDialogue = true;
        for (int i = 0; i < dialogueList.Count; i++)
        {
            scrollArrow.gameObject.SetActive(false);
            dialogueIMG.sprite = Resources.Load<Sprite>("Faces/" + dialogueList[i].dialogueIMG);
            dialogue = dialogueList[i].dialogueText;
            dialogueText.text = "";
            dialogueName.text = dialogueList[i].dialogueName;

            //start writing the dialogue
            for (int j = 0; j < dialogue.Length; j++)
            {
                dialogueSkip = false;
                dialogueName.text = dialogueList[i].dialogueName;
                dialogueText.text += dialogue[j];
                dialogueNoise.pitch = dialogueList[i].dialoguePitch;
                dialogueNoise.Play();




                if (dialogue[j].ToString() == "." || dialogue[j].ToString() == "?" || dialogue[j].ToString() == "!")
                {
                    yield return new WaitForSeconds(.5f * speedFactor);
                }
                else if (dialogue[j].ToString() == ";" || dialogue[j].ToString() == ":" || dialogue[j].ToString() == ",")
                {
                    yield return new WaitForSeconds(.25f * speedFactor);
                }
                else
                {
                    yield return new WaitForSeconds(.035f * speedFactor);
                }

                //check if the dialogue has been skipped.
                if (dialogueSkip && !dialogueWait)
                {
                    dialogueText.text = dialogue;
                    dialogueSkip = false;
                    dialogueWait = true;
                    break;
                }

            }

            yield return new WaitForSeconds(.5f);
            scrollArrow.gameObject.SetActive(true);
            while(!Input.GetMouseButtonDown(0))
            { 
                yield return null;
            }
            dialogueWait = false;
            dialogueSkip = false;
        }
        TestCharController.inDialogue = false;
        dialogueIMG.gameObject.SetActive(false);
        scrollArrow.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        dialogueDone = true;

    }

    public void StartDialogue()
    {
        writeText = StartCoroutine(WriteText(dialogueList));
    }

    public void Clear()
    {
        if(dialogueList.Count > 0)
        {
            StopCoroutine(writeText);
            dialogueList.Clear();
        }
    }
}
