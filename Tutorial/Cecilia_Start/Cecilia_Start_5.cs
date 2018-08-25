using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Cecilia_Start_5 : MonoBehaviour
{
    public AudioClip guildBGM;
    public AudioClip battleBGM;
    public GameObject blackMask;
    public GameObject HUD, compHUD, menu;
    public GameObject systemMessage;
    public Text systemText;
    public GameObject startWipe;

    public GameObject NPC;

    public GameObject dialogue;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(EventRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //1.828043

    IEnumerator EventRoutine()
    {
        yield return new WaitForEndOfFrame();

        HUD.SetActive(false);
        compHUD.SetActive(false);
        menu.SetActive(false);

        NPC.GetComponent<Animator>().Play("Ad_Left_Idle");

        blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = guildBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = 0;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        startWipe.SetActive(true);

        
        for (int i = 1; i < 100; i++)
        {
            blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (1- i*.01f));
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = i * .001f;
            yield return new WaitForSeconds(.02f);
        }

        startWipe.SetActive(false);
        dialogue.SetActive(true);

        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noSkip = true;
        dialogue.GetComponent<DialogueController>().Clear();

        Dialogue tempDia = new Dialogue("Welcome to the practical portion of the exam.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2);
        tempDia.dialogueAction = PanCamera();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);

        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Haha very good. For this portion of the exam you will spar with our proctor, Kyle. Try to hang in there as long as you can! Are you ready?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I sure am!", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, 0));

        Dialogue tempDia2 = new Dialogue("Good, please get in position and on my mark you may begin.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Byron", .9f, 2);
        tempDia2.dialogueAction = WalkPaces();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia2);

        Dialogue tempDia3 = new Dialogue("Good, show us what you've got!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Byron", .9f, 2);
        tempDia3.dialogueAction = Point();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia3);

        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Get, ready here I come!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Kyle", .9f, 0));

        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        Camera.main.orthographicSize = 1.828043f;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        menu.SetActive(true);
        TestCharController.attackEnabled = true;
        StartCoroutine(NPC.GetComponent<Kyle_Controller>().KyleBehavior());

    }

    IEnumerator WalkPaces()
    {
        TestCharController.player.GetComponent<Animator>().Play("Sprint_Left");
        NPC.GetComponent<Animator>().Play("Ad_Right_Run");
        for (int i = 0; i < 30; i++)
        {
            TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(-.25f, 0);
            NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(.25f, 0);
            yield return new WaitForSeconds(.1f);
        }
        TestCharController.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        TestCharController.player.GetComponent<Animator>().Play("TestLeftIdle");
        NPC.GetComponent<Animator>().Play("Ad_Right_Idle");

    }

    IEnumerator PanCamera()
    {
        StartCoroutine(CameraManager.PanCamera(new Vector3(0, 0, -10), 5, 500));
        yield return null;
    }

    IEnumerator Point()
    {
        TestCharController.player.GetComponent<Animator>().Play("TestRightIdle");
        NPC.GetComponent<Animator>().Play("Ad_Point");
        yield return new WaitForSeconds(1f);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = battleBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
    }
}
