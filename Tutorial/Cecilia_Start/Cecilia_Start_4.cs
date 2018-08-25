using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Cecilia_Start_4 : MonoBehaviour
{
    public AudioClip countBGM;

    public GameObject blackMask;
    public GameObject HUD, compHUD, menu;
    public GameObject systemMessage;
    public Text systemText;

    public GameObject dialogue;
    public bool finished = false;


    public GameObject count;
    public GameObject NPC, NPC2;
    public GameObject Maur;

	// Use this for initialization
	void Start ()
    {
		if(!finished)
        {
            StartCoroutine(EventRoutine());
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //1.828043
    IEnumerator EventRoutine()
    {
        yield return new WaitForEndOfFrame();
        TestCharController.inDialogue = true;
        HUD.SetActive(false);
        compHUD.SetActive(false);
        menu.SetActive(false);
        blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Camera.main.orthographicSize = 0.730533f;
        Camera.main.transform.position = new Vector3(0, -1.289f, -10);
        //Fade In
        for (float i = 1; i > 0; i -= .01f)
        {
            blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.03f);
        }

        dialogue.SetActive(true);

        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().Clear();

        Dialogue tempDia = new Dialogue("There! That should do it. I sure hope Theo isn't too mad I'm late.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 0);
        tempDia.dialogueAction = SpawnExclaim();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);

        Dialogue tempDia2 = new Dialogue("What was that? Sounds like something is coming this way. I sure hope it's not more monsters.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 0);
        tempDia2.dialogueAction = CameraManager.PanCamera(new Vector3(0, -0.193f, -10), 5, 500);
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia2);

        Dialogue tempDia3 = new Dialogue("It's over here.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Bandit A", .9f, 0);
        tempDia3.dialogueAction = MoveBandits();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia3);

        Dialogue tempDia4 = new Dialogue("Hehehe, looks like the monsters we released did the trick.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Bandit B", .9f, 0);
        tempDia4.dialogueAction = CameraManager.PanCamera(new Vector3(0, -1.289f, -10), 1.5f, 250);
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia4);

        Dialogue tempDia5 = new Dialogue("Cripes, bandits! It looks like this was all a trap.", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 0);
        tempDia5.dialogueAction = CameraManager.PanCamera(new Vector3(0, -0.193f, -10), 1.5f, 250);
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia5);

        Dialogue tempDia6 = new Dialogue("Alright let's clean out this cart...", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Bandit A", .9f, 0);
        tempDia6.dialogueAction = MoveCount();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia6);


        Dialogue tempDia7 = new Dialogue("Wait! Somebody is coming!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Bandit A", .9f, 0);
        tempDia7.dialogueAction = MoveCount2();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia7);


        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Don't come any closer!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Bandit A", .9f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("...", "Count/Count Beaumont_thigh up", "NPC/NPC_None", "NPC/NPC_None", "???", .9f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Beat it and pretend you saw nothin' or you'll regret it!", "Count/Count Beaumont_thigh up", "NPC/NPC_None", "NPC/NPC_None", "Bandit B", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukukuku.", "Count/Count Beaumont_thigh up", "NPC/NPC_None", "NPC/NPC_None", "???", .9f, 0));

        Dialogue tempDia8 = new Dialogue("What are ya' crazy? This is your last warning! ", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Bandit A", .9f, 2);
        tempDia8.dialogueAction = CountCharge();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia8);


        Dialogue tempDia9 = new Dialogue("Yeah you...", "Count/Count Beaumont_thigh up", "NPC/NPC_None", "NPC/NPC_None", "Bandit B", .9f, 2);
        tempDia9.dialogueAction = BanditBleed();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia9);

        Dialogue tempDia10 = new Dialogue("Y-you killed him! H-how...", "Count/Count Beaumont_thigh up", "NPC/NPC_None", "NPC/NPC_None", "Bandit B", .9f, 2);
        tempDia10.dialogueAction = BanditBleed2();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia10);

        Dialogue tempDia11 = new Dialogue("Kukuku... M-MARVELOUS! Even filth can become pure in death.", "Count/Count Beaumont_thigh up", "NPC/NPC_None", "NPC/NPC_None", "???", .9f, 0);
        tempDia11.dialogueAction = Laugh();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia11);

        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("C-cripes, he killed them both!", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 0));

        Dialogue tempDia12 = new Dialogue("And now, what to do with the little mouse behind the cart.", "Count/Count Beaumont_thigh up", "NPC/NPC_None", "NPC/NPC_None", "???", .9f, 0);
        tempDia12.dialogueAction = SpawnExclaim();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia12);

        Dialogue tempDia13 = new Dialogue("He's coming this way! Oh goodness, what am I going to do?", "NPC/NPC_None", "NPC/NPC_None", "NPC/NPC_None", "Maurice", .9f, 0);
        tempDia13.dialogueAction = ClosingScene();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia13);

        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        StartCoroutine(ClosingScene2());

    }

    IEnumerator Laugh()
    {
        StartCoroutine(CameraManager.PanCamera(new Vector3(0, -1.289f, -10), 1.5f, 250));
        count.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ClosingScene2()
    {        
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("BlankScreen");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cecilia_Town_Guild");
        LevelCreator.playerStartX = -0.26f;
        LevelCreator.playerStartY = 0;
        LevelCreator.startTag = "Right";
    }

    IEnumerator ClosingScene()
    {
        StartCoroutine(CameraManager.PanCamera(new Vector3(0.221f, -0.193f, -10), 1, 500));
        count.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -.15f);
        count.GetComponent<Animator>().Play("Count_Down_Walk");

        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("FadeOut");
        for (float i = .1f; i > 0; i -= .01f)
        {
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = i;
            yield return new WaitForSeconds(.6f);
        }
        GameObject.Find("WipeScreen").GetComponent<Animator>().Play("BlankScreen");
        SceneManager.LoadScene("Cecilia_Town_Guild");
        LevelCreator.playerStartX = -0.26f;
        LevelCreator.playerStartY = 0;
        LevelCreator.startTag = "Right";
    }


    IEnumerator BanditBleed2()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Dialogue/Count_Slash"), NPC2.transform);
        yield return new WaitForSeconds(.5f);
        Instantiate(Resources.Load<GameObject>("Prefabs/Dialogue/Blood"), NPC2.transform);
        yield return new WaitForSeconds(1f);
        NPC2.GetComponent<Animator>().Play("M3_NPC_Down_Dead");
    }

    IEnumerator BanditBleed()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Dialogue/Count_Slash"), NPC.transform);
        yield return new WaitForSeconds(.5f);
        Instantiate(Resources.Load<GameObject>("Prefabs/Dialogue/Blood"), NPC.transform);
        yield return new WaitForSeconds(1f);
        NPC.GetComponent<Animator>().Play("M_NPC_Left_Dead");
        Instantiate(Resources.Load("Prefabs/Dialogue/Exclaim_Balloon"), NPC2.transform);
        NPC2.GetComponent<Animator>().Play("M3_NPC_Down_Idle");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator CountCharge()
    {
        NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(-.25f, 0);
        NPC.GetComponent<Animator>().Play("M_NPC_Left_Walk");

        yield return new WaitForSeconds(1.5f);
        NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        NPC.GetComponent<Animator>().Play("M_NPC_Left_Idle");

        count.GetComponent<Animator>().Play("Count_Right_Charge");
        yield return new WaitForSeconds(.5f);
        count.GetComponent<Animator>().Play("Count_Right_Dash");
        StartCoroutine(CameraManager.PanCamera(new Vector3(0.221f, -0.193f, -10), 1, 250));
        while (count.transform.position.x < 1.133f)
        {
            count.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            Instantiate(Resources.Load<GameObject>("Prefabs/NPC/Count_AfterImage"), count.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(.01f);
        }
        
        count.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        count.GetComponent<Animator>().Play("Count_Right_Idle");
        yield return new WaitForEndOfFrame();
    }

    IEnumerator MoveCount2()
    {
        count.GetComponent<Rigidbody2D>().velocity = new Vector2(.25f, 0);
        count.GetComponent<Animator>().Play("Count_Right_Walk");
        yield return new WaitForSeconds(3f);
        count.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        count.GetComponent<Animator>().Play("Count_Right_Idle");
        yield return new WaitForEndOfFrame();
    }

    IEnumerator MoveCount()
    {
        Instantiate(Resources.Load("Prefabs/Dialogue/Exclaim_Balloon"), NPC.transform);
        Instantiate(Resources.Load("Prefabs/Dialogue/Exclaim_Balloon"), NPC2.transform);
        yield return new WaitForSeconds(1f);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = countBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        yield return new WaitForEndOfFrame();
    }

    IEnumerator MoveBandits()
    {
        NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(-.25f, 0);
        NPC.GetComponent<Animator>().Play("M_NPC_Left_Walk");

        NPC2.GetComponent<Rigidbody2D>().velocity = new Vector2(-.25f, 0);
        NPC2.GetComponent<Animator>().Play("M3_NPC_Left_Walk");

        yield return new WaitForSeconds(3f);

        NPC.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        NPC.GetComponent<Animator>().Play("M_NPC_Left_Idle");

        NPC2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        NPC2.GetComponent<Animator>().Play("M3_NPC_Left_Idle");
        yield return new WaitForEndOfFrame();
    }

    IEnumerator SpawnExclaim()
    {
        Instantiate(Resources.Load("Prefabs/Dialogue/Exclaim_Balloon"), Maur.transform);
        yield return new WaitForSeconds(1f);
    }
}
