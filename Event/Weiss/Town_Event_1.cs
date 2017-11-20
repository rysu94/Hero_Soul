using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Town_Event_1 : MonoBehaviour
{

    public static bool start = true;

    public GameObject mainCam;

    public GameObject dialogueController;
    public GameObject dialogueIMG;

    public GameObject leftEye;
    public GameObject rightEye;

    public AudioSource bgm;
    public float volume;
    public AudioClip tension;
    public AudioClip bgmClip;

    public AudioSource snap;

    public GameObject pulse;
    public GameObject pulse2;
    public GameObject pulse3;

    public GameObject[] villagers = new GameObject[5];

    public GameObject evilGuy;

    public GameObject[] daze = new GameObject[5];

    public GameObject[] paralyze = new GameObject[5];

    public GameObject weapon;

    public GameObject flash;

    public GameObject[] blood = new GameObject[5];

    public GameObject wipe;

    public GameObject laugh;
	// Use this for initialization
	void Start ()
    {
        evilGuy.GetComponent<Animator>().Play("M_NPC_Down_Idle");
        villagers[0].GetComponent<Animator>().Play("M_NPC_Up_Idle");
        villagers[2].GetComponent<Animator>().Play("M3_NPC_Up_Idle");
        villagers[3].GetComponent<Animator>().Play("M2_NPC_Up_Idle");
        villagers[4].GetComponent<Animator>().Play("M_NPC_Up_Idle");



        GameObject.Find("BGM").GetComponent<AudioSource>().clip = bgmClip;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;

        StartCoroutine(MoveCamera());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator MoveCamera()
    {
        villagers[1].GetComponent<Animator>().Play("M_NPC_Up_Walk");
        villagers[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0,.1f);
        StartCoroutine(EventRoutine());
        for (float i = -.75f; i < 0; i += .002f)
        {
            mainCam.transform.position = new Vector3(i, 0, -10);
            yield return new WaitForSeconds(.005f);
        }
        villagers[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        villagers[1].GetComponent<Animator>().Play("M_NPC_Up_Idle");
        
    }

    IEnumerator FadeOutRoutine()
    {
        volume = GameObject.Find("BGM").GetComponent<AudioSource>().volume;
        while(volume > 0)
        {
            volume -= .01f;
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = volume;
            yield return new WaitForSeconds(.1f);
        }

    }

    IEnumerator EventRoutine()
    {
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("", "NPC/NPC_None", "Meanwhile...", 1f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("What's going on here? Why is everyone gathered here?", "NPC/NPC_None", "Villager A", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("A performer just came into town. I think he's going to begin soon.", "NPC/NPC_None", "Villager B", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("A performance, huh? Sounds pretty interesting.", "NPC/NPC_None", "Villager C", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukukuku... Please bear with me ladies and gentlemen, the show will begin shortly.", "Count/Count Beaumont_thigh up", "???", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I wonder what he will perform?", "NPC/NPC_None", "Villager B", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("It has to be magic, just look at how he's dressed. Anyone dressed that well must be a magician!", "NPC/NPC_None", "Villager A", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku, I'm terribly sorry to say that I am no magician just a simple artist trying to get by, but rest assured I'm sure I'll be able to captivate you... Allow me to introduce myself, I am Count Beaumont, traveling artist.", "Count/Count Beaumont_thigh up", "???", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("An artist? Ha, how? I don't see any paint or a canvas?", "NPC/NPC_None", "Villager C", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku, allow me to show you...", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();



        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        StartCoroutine(FadeOutRoutine());
        yield return new WaitForSeconds(1.5f);
        snap.Play();

        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Eins! Drais! Come out show our audience your beauty!", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        leftEye.SetActive(true);
        leftEye.GetComponent<Animator>().Play("Big_Eye_Open");
        rightEye.SetActive(true);
        rightEye.GetComponent<Animator>().Play("Big_Eye_Open");

        yield return new WaitForSeconds(3f);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = tension;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();

        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("W-what are those?", "NPC/NPC_None", "Villager A", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Those are.. D-demons!", "NPC/NPC_None", "Villager B", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("B-but how?! They were all supposed to be killed ten years ago!", "NPC/NPC_None", "Villager B", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yes... Yes! This atmosphere! This tension! MARVELOUS! SPECTACULAR! This is my art! Kukuku!", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Run!", "NPC/NPC_None", "Villager A", 0.9f));
        
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        for(int i = 0; i < villagers.Length; i++)
        {
            villagers[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, -.07f);
        }
        villagers[0].GetComponent<Animator>().Play("M_NPC_Up_Walk");
        villagers[1].GetComponent<Animator>().Play("M_NPC_Up_Walk");
        villagers[2].GetComponent<Animator>().Play("M3_NPC_Up_Walk");
        villagers[3].GetComponent<Animator>().Play("M2_NPC_Up_Walk");
        villagers[4].GetComponent<Animator>().Play("M_NPC_Up_Walk");

        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < villagers.Length; i++)
        {
            villagers[i].GetComponent<Animator>().Play("M_NPC_Up_Idle");
            villagers[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        villagers[2].GetComponent<Animator>().Play("M3_NPC_Up_Idle");
        villagers[3].GetComponent<Animator>().Play("M2_NPC_Up_Idle");

        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Tsk, tsk. We can't have you doing that, we haven't even got to the main event.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        snap.Play();
        yield return new WaitForSeconds(1.5f);
        leftEye.GetComponent<Animator>().Play("Big_Eye_Angry_Left");
        rightEye.GetComponent<Animator>().Play("Big_Eye_Right");

        leftEye.GetComponent<AudioSource>().Play();
        pulse.SetActive(true);
        yield return new WaitForSeconds(.25f);
        pulse2.SetActive(true);
        yield return new WaitForSeconds(.25f);
        pulse3.SetActive(true);
        yield return new WaitForSeconds(.5f);

        for(int i = 0; i < daze.Length; i++)
        {
            daze[i].SetActive(true);
            paralyze[i].SetActive(true);
        }
        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("My body... W-what have you done?", "NPC/NPC_None", "Villager A", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku... What good is an artist if they can't hold an audience?", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Y-you Bastard, you won't get away with this. The guard...", "NPC/NPC_None", "Villager B", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("♥Ahhh♥ Your anger, your fear, such raw emotion... MAGNIFICENT! Remember this feeling as your curtain falls.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        laugh.GetComponent<AudioSource>().Play();
        evilGuy.GetComponent<Animator>().Play("M_NPC_Down_Dash");
        flash.SetActive(true);
        evilGuy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3f);
        for (float i = 0; i < .25f; i += .05f)
        {
            evilGuy.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (1f - (i * 4)));
            yield return new WaitForSeconds(.05f);
        }
        weapon.SetActive(true);
        yield return new WaitForSeconds(.15f);

        evilGuy.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        evilGuy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        evilGuy.GetComponent<Animator>().Play("M_NPC_Down_Idle");
        yield return new WaitForSeconds(.5f);
        weapon.SetActive(false);
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < blood.Length; i++)
        {
            blood[i].SetActive(true);
            daze[i].SetActive(false);
            paralyze[i].SetActive(false);
        }

        leftEye.GetComponent<Animator>().Play("Big_Eye_Idle");
        rightEye.GetComponent<Animator>().Play("Big_Eye_Idle");

        dialogueController.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogueController.GetComponent<DialogueController>().Clear();
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Arrrrgh!", "NPC/NPC_None", "Villagers", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Y-you're a monster! Just what are you?", "NPC/NPC_None", "Villager A", 0.9f));
        dialogueController.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Hmmm, still alive? What am I? I told you I am just a simple artist. Kukuku.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogueController.GetComponent<DialogueController>().StartDialogue();

        while (!dialogueController.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        laugh.GetComponent<AudioSource>().Play();
        evilGuy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -.15f);
        evilGuy.GetComponent<Animator>().Play("M_NPC_Down_Walk");
        wipe.GetComponent<Animator>().Play("FadeScreenEvent");


        yield return new WaitForSeconds(5f);
        Town_Event_2.start = true;
        SceneManager.LoadScene("Town_Inn");


    }
}
