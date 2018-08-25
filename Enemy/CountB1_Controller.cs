using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountB1_Controller : Monster
{
    public Animator countAnimator;
    public GameObject blackMask;

    public GameObject bossBarFrame;
    public GameObject bossBar;
    public Text bossBarText;
    public float bossBarWidth = 201.4f;
    public float monsterMaxHealth = 5000;

    public AudioClip bossMusic;
    public AudioClip normalMusic;

    public GameObject bossWipe;

    //Charge Bar
    public float chargeMult;
    public GameObject chargeBar, chargeFrame;

    public GameObject dialogue;

    // Use this for initialization
    void Start ()
    {
        monsterName = "Count Beaumont";

        monsterHealth = 5000;
        monsterMaxHealth = 5000;
        contactDamage = 10;


        player = GameObject.FindGameObjectWithTag("Player");
        countAnimator = GetComponent<Animator>();
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        //Count Loot Table

        //Red Mushroom
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[7]);
        monsterDropChance.Add(250);
        //Mighty Copper Ring
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[11]);
        monsterDropChance.Add(10);
        //Nimble Copper Ring   
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[12]);
        monsterDropChance.Add(10);
        //Pendent of Might
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[31]);
        monsterDropChance.Add(10);
        //Pendent of Agility
        monsterDrops.Add(GameObject.Find("InventoryController").GetComponent<ItemDatabase>().itemData[32]);
        monsterDropChance.Add(10);

        //Arcana [fire, water, earth, air, life]
        arcanaDrop.Add(50);
        arcanaDrop.Add(50);
        arcanaDrop.Add(50);
        arcanaDrop.Add(50);
        arcanaDrop.Add(25);

        //experience drop
        experienceDrop = 15;

        StartCoroutine(StartRoutine());
    }
	
	// Update is called once per frame
	void Update ()
    {
        float healthPercent = (float)monsterHealth / (float)monsterMaxHealth;
        var xpBarRect = bossBar.transform as RectTransform;
        xpBarRect.sizeDelta = new Vector2(bossBarWidth * healthPercent, xpBarRect.sizeDelta.y);
    }

    IEnumerator CountRoutine()
    {
        yield return new WaitForEndOfFrame();
        while(monsterHealth > 4000)
        {
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForEndOfFrame();
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = normalMusic;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();

        player.transform.position = new Vector2(0, -0.637f);
        LevelCreator.startTag = "Up";
        player.GetComponent<TestCharController>().SetPlayerDirection();

        for (int i = 1; i < 100; i++)
        {
            blackMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (1 - i * .01f));
            GameObject.Find("BGM").GetComponent<AudioSource>().volume = i * .001f;
            yield return new WaitForSeconds(.1f);
        }

        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().resetChar = false;
        dialogue.GetComponent<DialogueController>().noFade = true;
        dialogue.GetComponent<DialogueController>().noMove = true;
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukukku, it would appear I have a guest. Say my little kitten, are you enjoying the show?", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "???", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Show? W-what is happening here...", "Cecilia/Cecilia Grey_thigh_2", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "Cecilia", 1f, 0));
        Dialogue tempDia = new Dialogue("Suffering... Hatred... it's so... MARVELOUS!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "???", .9f, 2);
        tempDia.dialogueAction = LaughRoutine();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(tempDia);
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("...Yes... No... She will not be a problem...", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "???", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Sorry mister, whatever you're doing, if you're causing all this trouble I'm going to have to stop you!", "Cecilia/Cecilia Grey_thigh_3", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("..Kukuku, so the kitten does have some claws.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "???", .9f, 2));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I-I'm sorry I can't let you leave.", "Cecilia/Cecilia Grey_thigh_3", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "Cecilia", 1f, 0));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Kukuku, I'D LIKE TO SEE YOU TRY!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "Count/Count Beaumont_thigh up", "???", .9f, 2));
        dialogue.GetComponent<DialogueController>().StartDialogue();
        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        bossBarText.text = "Count Beaumont";
        bossBarFrame.SetActive(true);
        bossBar.SetActive(true);
        TestCharController.inDialogue = true;
        countAnimator.GetComponent<Animator>().Play("Count_Right_Laugh");
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("BGM").GetComponent<AudioSource>().Stop();
        GameObject.Find("BGM").GetComponent<AudioSource>().clip = bossMusic;
        GameObject.Find("BGM").GetComponent<AudioSource>().Play();
        bossWipe.SetActive(true);
        yield return new WaitForSeconds(2f);
        bossWipe.SetActive(false);
        TestCharController.inDialogue = false;
        countAnimator.GetComponent<Animator>().Play("Count_Down_Idle");

        StartCoroutine(CountRoutine());

    }

    IEnumerator LaughRoutine()
    {
        countAnimator.GetComponent<Animator>().Play("Count_Right_Laugh");
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2.5f);
        countAnimator.GetComponent<Animator>().Play("Count_Down_Idle");
    }
}
