using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Text dialogueText;
    public Text dialogueName;

    public Image dialogueIMG;
    public Image dialogueBackIMG;
    public Image dialogueNPC;

    public Image scrollArrow;
    public Image dialogueFade;
    public bool noFade = false;
    public bool noMove = false;
    public bool resetChar = false;
    public bool noSkip = false;

    public GameObject playerHUD, compHUD;

    public List<Dialogue> dialogueList = new List<Dialogue>();

    public Coroutine writeText;
    public Coroutine moveCam;

    public string dialogue;

    public AudioSource dialogueNoise;

    //Flag if player chooses to skip dialogue
    public bool dialogueSkip = false;
    //Flag for if the dialogue is waiting for the player
    public bool dialogueWait = false;

    public float speedFactor;

    //Flag for whether or not the dialogue is done writing
    public bool dialogueDone = false;

    public bool waitingDecision = false;

    public bool skipDialogue = false;

    public GameObject skipText;

    public GameObject shopPrompt, choicePrompt;
    public bool showShop = false;
    public bool showChoice = false;

    // Use this for initialization
    void Start ()
    {

        //StartDialogue();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Checks if the player wants to skip dialogue
        if(((Input.GetMouseButtonDown(0)) || InputManager.A_Button()) && !dialogueSkip && !dialogueWait)
        {
            dialogueSkip = true;
        }

        //Skip Entire Dialogue
        if((Input.GetKeyDown(KeyCode.Space) || InputManager.B_Button()) && !waitingDecision && !noSkip)
        {
            skipDialogue = true;
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

    IEnumerator MoveCamera(Vector3 end)
    {
        float xInc = (end.x - Camera.main.transform.position.x) / 25f;
        float yInc = (end.y - Camera.main.transform.position.y) / 25f;

        for(float i = 1; i < 25; i += .25f)
        {
            Camera.main.transform.position = new Vector3(xInc * i, yInc * i, -10);
            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator WriteText(List<Dialogue> dialogueList)
    {
        if(resetChar)
        {
            if (TestCharController.player.GetComponent<TestCharController>().north)
            {
                TestCharController.player.GetComponent<Animator>().Play("TestUpIdle");
            }
            else if (TestCharController.player.GetComponent<TestCharController>().south)
            {
                TestCharController.player.GetComponent<Animator>().Play("TestDownIdle");
            }
            else if (TestCharController.player.GetComponent<TestCharController>().west)
            {
                TestCharController.player.GetComponent<Animator>().Play("TestLeftIdle");
            }
            else
            {
                TestCharController.player.GetComponent<Animator>().Play("TestRightIdle");
            }
        }

        if (GameController.xbox360Enabled())
        {
            skipText.GetComponent<Text>().text = "Press B to Skip";
        }

        if(noSkip)
        {
            skipText.SetActive(false);
        }

        dialogueDone = false;
        dialogueIMG.gameObject.SetActive(true);
        dialogueBackIMG.gameObject.SetActive(true);
        dialogueNPC.gameObject.SetActive(true);

        playerHUD.SetActive(false);
        compHUD.SetActive(false);

        if(!noMove)
        {
            moveCam = StartCoroutine(MoveCamera(TestCharController.player.transform.position));
        }
        
        if (!noFade)
        {
            dialogueFade.gameObject.SetActive(true);
        }     
        TestCharController.inDialogue = true;
        for (int i = 0; i < dialogueList.Count; i++)
        {
            scrollArrow.gameObject.SetActive(false);
            
            //entire dialogue skipped?
            if(skipDialogue)
            {
                break;
            }

            dialogueIMG.sprite = Resources.Load<Sprite>("Faces/" + dialogueList[i].dialogueForeIMG);
            if(dialogueList[i].activeIMG == 0)
            {
                dialogueIMG.color = new Color(1, 1, 1);
            }
            else
            {
                dialogueIMG.color = new Color(.5f, .5f, .5f);
            }
            dialogueBackIMG.sprite = Resources.Load<Sprite>("Faces/" + dialogueList[i].dialogueBackIMG);
            if (dialogueList[i].activeIMG == 1)
            {
                dialogueBackIMG.color = new Color(1, 1, 1);
            }
            else
            {
                dialogueBackIMG.color = new Color(.5f, .5f, .5f);
            }
            dialogueNPC.sprite = Resources.Load<Sprite>("Faces/" + dialogueList[i].dialogueNPC);
            if (dialogueList[i].activeIMG == 2)
            {
                dialogueNPC.color = new Color(1, 1, 1);
            }
            else
            {
                dialogueNPC.color = new Color(.5f, .5f, .5f);
            }
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

                //entire dialogue skipped?
                if (skipDialogue)
                {
                    break;
                }

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




            yield return new WaitForSeconds(1f);
            scrollArrow.gameObject.SetActive(true);
            if(showShop)
            {
                shopPrompt.SetActive(true);
                if(GameController.xbox360Enabled())
                {
                    shopPrompt.GetComponent<Dialogue_Choice>().padY = 0;
                    shopPrompt.GetComponent<Dialogue_Choice>().UpdateCursor();
                }
            }
            else if(showChoice)
            {
                choicePrompt.SetActive(true);
                if (GameController.xbox360Enabled())
                {
                    shopPrompt.GetComponent<Dialogue_Choice>().padY = 0;
                    shopPrompt.GetComponent<Dialogue_Choice>().UpdateCursor();
                }
            }
            while(((!Input.GetMouseButtonDown(0) && !InputManager.A_Button()) || waitingDecision) && !skipDialogue)
            { 
                yield return null;
            }

            //Do the dialogue action if there is one
            if (dialogueList[i].dialogueAction != null)
            {
                yield return StartCoroutine(dialogueList[i].dialogueAction);
            }

            dialogueWait = false;
            dialogueSkip = false;
            
        }

        if (!noMove)
        {
            StopCoroutine(moveCam);
        }
        
        yield return new WaitForSeconds(.5f);
        skipDialogue = false;
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("FadeIn");
        playerHUD.SetActive(true);
        if(TestCharController.companionID != 0)
        {
            compHUD.SetActive(true);
        }        
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        TestCharController.inDialogue = false;
        dialogueIMG.gameObject.SetActive(false);
        dialogueBackIMG.gameObject.SetActive(false);
        dialogueNPC.gameObject.SetActive(false);
        dialogueFade.gameObject.SetActive(false);
        scrollArrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(.1f);
        showShop = false;
        showChoice = false;
        gameObject.SetActive(false);
        dialogueDone = true;
        waitingDecision = false;
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

    public IEnumerator EndDialogue()
    {
        StopCoroutine(moveCam);
        yield return new WaitForSeconds(.1f);
        skipDialogue = false;
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("FadeIn");
        playerHUD.SetActive(true);
        compHUD.SetActive(true);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        TestCharController.inDialogue = false;
        dialogueIMG.gameObject.SetActive(false);
        dialogueBackIMG.gameObject.SetActive(false);
        dialogueNPC.gameObject.SetActive(false);
        dialogueFade.gameObject.SetActive(false);
        scrollArrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(.1f);
        dialogueDone = true;
        showShop = false;
        showChoice = false;
        gameObject.SetActive(false);
    }
}
