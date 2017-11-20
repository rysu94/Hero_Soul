using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountController : Monster
{
    public static bool start = false;

    public Animator countAnim;

    public GameObject bossBar;
    public GameObject bossBarFrame;

    public Text bossBarText;

    public float bossBarWidth = 201.4f;
    public float monsterMaxHealth = 5000;

    public GameObject[] lineAttack = new GameObject[4];
    public GameObject[] coneAttack = new GameObject[4];
    public GameObject circleAttack;

    public GameObject teleport;
    public GameObject shield;

    public GameObject dazePrefab;
    public GameObject weapon;
    public GameObject flash;
    public GameObject wipe;
    public GameObject laugh;
    public GameObject hud;

    public GameObject dialogue;
    public GameObject dialogueIMG;

    public float volume;
    public AudioClip pregameBGM;
    public AudioClip heroBGM;
    public GameObject puck;
    public GameObject circleWipe;
    public GameObject canvasWipe;

    // Use this for initialization
    void Start ()
    {
        monsterHealth = 500;
        monsterMaxHealth = 500;
        contactDamage = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        countAnim = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();


    }
	
	// Update is called once per frame
	void Update ()
    {
        float healthPercent = (float)monsterHealth / (float)monsterMaxHealth;
        var xpBarRect = bossBar.transform as RectTransform;
        xpBarRect.sizeDelta = new Vector2(bossBarWidth * healthPercent, xpBarRect.sizeDelta.y);

        if (start)
        {
            start = false;
            StartCoroutine(BehaviorRoutine());
        }
	}
    
    IEnumerator BehaviorRoutine()
    {
        TestCharController.inDialogue = false;
        bossBarText.text = "Count Beaumont";
        bossBarFrame.SetActive(true);
        bossBar.SetActive(true);
        yield return new WaitForSeconds(3f);
        while (monsterHealth > 250)
        {
            shield.SetActive(false);
            teleport.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            for (float i = 0; i < .25f; i += .025f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (1f - (i * 4)));
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .294f * (1f - (i * 4)));
                yield return new WaitForSeconds(.05f);
            }
            Instantiate(circleAttack, TestCharController.player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 5; i++)
            {
                StartCoroutine(AttackRoutine());
                yield return new WaitForSeconds(.5f);
            }
            yield return new WaitForSeconds(7f);

            for (float i = 0; i < .25f; i += 0.025f)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i * 4);
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .294f * i * 4);
                yield return new WaitForSeconds(.1f);
            }
            invulnerable = false;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            yield return new WaitForSeconds(5f);
            shield.SetActive(true);
            invulnerable = true;
            yield return new WaitForSeconds(2f);
        }
        TestCharController.inDialogue = true;
        StartCoroutine(FadeOutRoutine());
        canvasWipe.GetComponent<Animator>().Play("FadeOut");
        yield return new WaitForSeconds(5f);
        TestCharController.player.transform.position = new Vector2(0, 0.875f);
        TestCharController.player.GetComponent<Animator>().Play("TestUpIdle");

        float x = TestCharController.player.transform.position.x;
        float y = TestCharController.player.transform.position.y + .385f;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        transform.position = new Vector2(x, y);
        GetComponent<Animator>().Play("M_NPC_Down_Idle");

        canvasWipe.GetComponent<Animator>().Play("FadeIn");
        TestCharController.inDialogue = false;
        dialogue.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Enough! I grow tired of this. It's time to end our dance.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }



        dialogue.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I will, however, commend you for getting this far.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("My body, it won't move.", "Cecilia/Cecilia Grey_thigh_6", "Cecilia", 1f));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        TestCharController.inDialogue = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
        GetComponent<Animator>().Play("M_NPC_Down_Dash");
        flash.SetActive(true);
        for (float i = 0; i < .25f; i += .05f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (1f - (i * 4)));
            yield return new WaitForSeconds(.05f);
        }
        weapon.SetActive(true);
        yield return new WaitForSeconds(.15f);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        GetComponent<Animator>().Play("M_NPC_Down_Idle");
        yield return new WaitForSeconds(.5f);
        
        weapon.SetActive(false);
        PlayerStats.health = 1;
        TestCharController.inDialogue = false;

        dialogue.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Damn...", "Cecilia/Cecilia Grey_thigh_6", "Cecilia", 1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku... Your face warped in pain... MARVELOUS.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        TestCharController.inDialogue = true;
        laugh.GetComponent<AudioSource>().Play();
        wipe.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        dialogue.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("T-this is bad... I just might...", "Cecilia/Cecilia Grey_thigh_6", "Cecilia", 1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Adieu, we won't meet again.", "Count/Count Beaumont_thigh up", "Count Beaumont", 0.9f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Everythings going dark...", "Cecilia/Cecilia Grey_thigh_6", "Cecilia", 1f));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        wipe.GetComponent<Animator>().Play("FadeScreenEvent");
        TestCharController.inDialogue = true;

        yield return new WaitForSeconds(5f);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = pregameBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .05f;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        puck.SetActive(true);
        yield return new WaitForSeconds(16f);
        circleWipe.SetActive(true);


        yield return new WaitForSeconds(2f);

        dialogue.SetActive(true);
        dialogueIMG.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("What's happening? The pain... it's gone. What happened to the fight? Where am I?", "Cecilia/Cecilia Grey_thigh_2", "Cecilia", 1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You exist on the border of life and death.", "NPC/NPC_None", "???", 1.1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Death? Then, you saved me?", "Cecilia/Cecilia Grey_thigh_1", "Cecilia", 1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I have only saved you for the time being, but your real body and the town are still in danger.", "NPC/NPC_None", "???", 1.1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Then I have to go back! As a junior member of the Guild it is my duty to protect.", "Cecilia/Cecilia Grey_thigh_3", "Cecilia", 1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I can bring you back, but answer this. Why do you fight?", "NPC/NPC_None", "???", 1.1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("That's east, I fight becuase I want to be a hero!", "Cecilia/Cecilia Grey_thigh_7", "Cecilia", 1f));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I see. It appears I was right to save you...", "NPC/NPC_None", "???", 1.1f));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        circleWipe.SetActive(false);
        wipe.GetComponent<Animator>().Play("WipeScreenEvent");
        yield return new WaitForSeconds(3f);
        wipe.SetActive(false);
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = heroBGM;
        GameObject.Find("BGM").GetComponent<AudioSource>().volume = .1f;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();


    }

    IEnumerator AttackRoutine()
    {

        //print("start");
        /*
        for (float i = 0; i < .25f; i += .025f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (1f - (i * 4)));
            shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .294f * (1f - (i * 4)));
            yield return new WaitForSeconds(.05f);
        }    
        */
        yield return new WaitForSeconds(1.5f);

        //Determine a valid direction to attack from
        List<int> validDir = new List<int>();
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        //North
        if (playerY < .8f)
        {
            validDir.Add(1);
        }
        //South
        if (playerY > -1.1f)
        {
            validDir.Add(2);
        }
        //East
        if (playerX < 2.0f)
        {
            validDir.Add(3);
        }
        //West
        if (playerX > -2.0f)
        {
            validDir.Add(4);
        }


        for(int i = 0; i < lineAttack.Length; i++)
        {
            lineAttack[i].SetActive(false);
        }

        int tempInt = Random.Range(0, validDir.Count);
        float newY, newX;
        int tempInt2 = Random.Range(1, 3);
        switch (validDir[tempInt])
        {
            default:
                break;
            //north
            case 1:
                GetComponent<Animator>().Play("M_NPC_Down_Idle");
                newY = playerY + .39f;
                transform.position = new Vector2(playerX, newY);
                /*
                for (float i = 0; i < .25f; i += 0.025f)
                {
                    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i * 4);
                    shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .294f * i * 4);
                    yield return new WaitForSeconds(.1f);
                }
                */
                if (tempInt2 == 1)
                {
                    Instantiate(lineAttack[3], transform.position, Quaternion.identity).SetActive(true);
                }
                else
                {
                    Instantiate(coneAttack[3], transform.position, Quaternion.identity).SetActive(true);
                }


                break;
            //south
            case 2:
                GetComponent<Animator>().Play("M_NPC_Up_Idle");
                newY = playerY - .4f;
                transform.position = new Vector2(playerX, newY);
                /*
                for (float i = 0; i < .25f; i += 0.025f)
                {
                    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i * 4);
                    shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .294f * i * 4);
                    yield return new WaitForSeconds(.1f);
                }
                */
                if (tempInt2 == 1)
                {
                    Instantiate(lineAttack[2], transform.position, Quaternion.identity).SetActive(true);
                }
                else
                {
                    Instantiate(coneAttack[2], transform.position, Quaternion.identity).SetActive(true);
                }

                break;
            //East
            case 3:
                GetComponent<Animator>().Play("M_NPC_Left_Idle");
                newX = playerX + .35f;
                transform.position = new Vector2(newX, playerY);
                /*
                for (float i = 0; i < .25f; i += 0.025f)
                {
                    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i * 4);
                    shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .294f * i * 4);
                    yield return new WaitForSeconds(.1f);
                }
                */
                if (tempInt2 == 1)
                {
                    Instantiate(lineAttack[0], transform.position, Quaternion.identity).SetActive(true);
                }
                else
                {
                    Instantiate(coneAttack[0], transform.position, Quaternion.identity).SetActive(true);
                }
                break;
            //west
            case 4:
                GetComponent<Animator>().Play("M_NPC_Right_Idle");
                newX = playerX - .35f;
                transform.position = new Vector2(newX, playerY);
                /*
                for (float i = 0; i < .25f; i += 0.025f)
                {
                    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, i * 4);
                    shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .294f * i * 4);
                    yield return new WaitForSeconds(.1f);
                }
                */
                if (tempInt2 == 1)
                {
                    Instantiate(lineAttack[1], transform.position, Quaternion.identity).SetActive(true);
                }
                else
                {
                    Instantiate(coneAttack[1], transform.position, Quaternion.identity).SetActive(true);
                }
                break;
        }

        yield return new WaitForSeconds(3f);
        teleport.SetActive(false);
        //print("end");
    }

    IEnumerator FadeOutRoutine()
    {
        volume = GameObject.Find("BGM").GetComponent<AudioSource>().volume;
        while (volume > 0)
        {
            volume -= .01f;
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = volume;
            yield return new WaitForSeconds(.1f);
        }
    }

}
