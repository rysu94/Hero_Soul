using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dummy : Monster
{
    public int hitCount = 0;

    public GameObject dialogue;
    public GameObject dialogueImg;

    public GameObject tutMessage;
    public Text tutText;

    public GameObject cameraObject;

    public GameObject arrowRight;
    public GameObject arrowRight_2;
    

    // Use this for initialization
    void Start ()
    {
        monsterHealth = 1000;
        contactDamage = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        experienceDrop = 0;

        StartCoroutine(RefreshHealth());
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Byron2.phase1 && hitCount == 10)
        {
            StartCoroutine(PhaseOne());
        }
        if (Byron2.phase2 && hitCount >= 1 && TestCharController.chargeMult >= 3)
        {
            StartCoroutine(PhaseTwo());
        }
        else if (Byron2.phase2 && hitCount >= 1 && TestCharController.chargeMult < 3)
        {
            StartCoroutine(PhaseTwoWeak());
        }
        else if (Byron2.phase3 && TutorialDatabase.tut7)
        {
            StartCoroutine(PhaseThree());
        }
        else if(Byron2.phase4 && hitCount >= 3)
        {
            StartCoroutine(Phase4());
        }
	}

    IEnumerator RefreshHealth()
    {
        yield return new WaitForSeconds(1f);
        monsterHealth = 1000;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon" && !isTriggered && Byron2.phase1)
        {
            hitCount++;

            if (physicalResist || invulnerable)
            {
                int wepDamage = DamageManager.WeaponDamage(this.gameObject);
                DamageMonster(0);
            }
            else
            {
                int wepDamage = DamageManager.WeaponDamage(this.gameObject);
                DamageMonster(wepDamage);
            }


            if (!isCollapsing)
            {
                monsterSprite.color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutineEnemy");
            }

            if (!isSliding && !isCollapsing)
            {
                StartCoroutine("EnemySlideRoutine");
            }

            if (TestCharController.player.GetComponent<TestCharController>().isSliding)
            {
                TestCharController.player.GetComponent<TestCharController>().playerRB.velocity = new Vector2(0, 0);
            }

            if (TestCharController.startBreak)
            {
                print("hit");
                TestCharController.player.GetComponent<TestCharController>().playerRB.velocity = new Vector2(0, 0);
                TestCharController.breakHit = true;
                TestCharController.player.GetComponent<TestCharController>().breakTargetSingle = gameObject;
            }
        }

        if (collision.gameObject.tag == "Weapon" && !isTriggered && Byron2.phase2 && TestCharController.player.GetComponent<TestCharController>().isCharging)
        {
            hitCount++;

            if (physicalResist || invulnerable)
            {
                int wepDamage = DamageManager.WeaponDamage(this.gameObject);
                DamageMonster(0);
            }
            else
            {
                int wepDamage = DamageManager.WeaponDamage(this.gameObject);
                DamageMonster(wepDamage);
            }


            if (!isCollapsing)
            {
                monsterSprite.color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutineEnemy");
            }

            if (!isSliding && !isCollapsing)
            {
                StartCoroutine("EnemySlideRoutine");
            }

            if (TestCharController.player.GetComponent<TestCharController>().isSliding)
            {
                TestCharController.player.GetComponent<TestCharController>().playerRB.velocity = new Vector2(0, 0);
            }

            if (TestCharController.startBreak)
            {
                print("hit");
                TestCharController.player.GetComponent<TestCharController>().playerRB.velocity = new Vector2(0, 0);
                TestCharController.breakHit = true;
                TestCharController.player.GetComponent<TestCharController>().breakTargetSingle = gameObject;
            }
        }
        /*
        if((collision.gameObject.tag == "Spell_Destructible") && Byron2.phase4)
        {
            hitCount++;

            int spellDamage = DamageManager.MagicDamage(0, this.gameObject);
            DamageMonster(spellDamage);

            if (!isCollapsing)
            {
                monsterSprite.color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutineEnemy");
            }
            if (!isSliding && !isCollapsing)
            {
                StartCoroutine("EnemySlideRoutine");
            }
        }
        else if (collision.gameObject.tag == "Spell_Multi" && Byron2.phase4)
        {
            hitCount++;

            StartCoroutine("MultiDamage");

            if (!isCollapsing)
            {
                monsterSprite.color = new Color(1f, redValue, redValue);
                StartCoroutine("ColorReturnRoutineEnemy");
            }
            if (!isSliding && !isCollapsing)
            {
                StartCoroutine("EnemySlideRoutine");
            }
        }
        */
    }

    IEnumerator PhaseOne()
    {
        Byron2.phase1 = false;
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Good that's enough! Seems like you know your basics. Next, let's see if you have more advanced techniques up your sleeve. Try hitting the dummy with your best attack.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Right, it's time to see if all my practice payed off. Let's show them my special move I've been working on!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        tutMessage.GetComponent<LifespanHide>().active = false;
        tutMessage.SetActive(false);
        tutText.text = "Special Attack Basics:\nClick and hold the [RMB] to charge your special attack, and release the button to use your special attack. Charging uses stamina.";
        tutMessage.SetActive(true);
        hitCount = 0;
        Byron2.phase2 = true;
    }

    IEnumerator PhaseTwo()
    {
        Byron2.phase2 = false;
        
        hitCount = 0;
        yield return StartCoroutine(ShakeScreen(.5f));
        GameObject.Find("PassNoise").GetComponent<AudioSource>().Play();
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Impressive stuff kid! That last attack had some real power behind it. Your physical attacks are quite good, you pass.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yes! Only one more to go!", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        //arrowRight.SetActive(true);
        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = true;
        GameObject.Find("RoomDone").GetComponent<AudioSource>().Play();
        Byron2.phase3 = true;
    }

    IEnumerator PhaseTwoWeak()
    {
        hitCount = 0;
        TestCharController.inDialogue = true;
        yield return new WaitForSeconds(1.5f);
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("I know I can do better than that. Let's charge my attack more!", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
    }

    IEnumerator PhaseThree()
    {
        Byron2.phase3 = false;
        Deck.ResetDeck();
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("You have a deck built? Good, let's put it to some good use. Come back to the dummy and try to hit it with your Arcana.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }
        Byron2.phase4 = true;
        hitCount = 0;

        if(!TutorialDatabase.tut8)
        {
            tutText.text = "Arcana Basics:\nYou can see your \"Arcana Hand\" in the bottom left. You need Mana to use your Arcana which can be seen in the top left. The cost can be seen on the Arcana Cards.";
            tutMessage.SetActive(true);
            tutMessage.GetComponent<RectTransform>().position = new Vector2(tutMessage.GetComponent<RectTransform>().position.x, -80f);
            arrowRight_2.SetActive(true);
            TutorialDatabase.tut8 = true;
        }
    }

    IEnumerator Phase4()
    {
        GameObject.Find("PassNoise").GetComponent<AudioSource>().Play();
        Byron2.phase4 = false;
        dialogue.SetActive(true);
        dialogueImg.SetActive(true);
        dialogue.GetComponent<DialogueController>().Clear();
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Enough, that was some pretty impressive Arcana usage. You pass the combat test. Please move on to the final test.", "Cecilia/Cecilia Grey_thigh_1", "NPC/NPC_None", "NPC/NPC_Veteran", "Byron", .9f, -1));
        dialogue.GetComponent<DialogueController>().dialogueList.Add(new Dialogue("Yes! Only one more to go!", "Cecilia/Cecilia Grey_thigh_7", "NPC/NPC_None", "NPC/NPC_Veteran", "Cecilia", 1f, -1));
        dialogue.GetComponent<DialogueController>().StartDialogue();

        while (!dialogue.GetComponent<DialogueController>().dialogueDone)
        {
            yield return null;
        }

        LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear = true;
        GameObject.Find("RoomDone").GetComponent<AudioSource>().Play();
    }

    //Shakes the screen
    IEnumerator ShakeScreen(float duration)
    {
        float shakeDuration = duration;
        Vector2 originalPos = cameraObject.transform.position;
        GetComponent<AudioSource>().Play();
        
        while (shakeDuration > 0)
        {
            shakeDuration -= .01f;
            Vector2 newPos = new Vector2(originalPos.x + (Random.insideUnitCircle.x * .04f), originalPos.y);
            cameraObject.transform.position = new Vector3(newPos.x, newPos.y, -10);
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(.01f);
        cameraObject.transform.position = new Vector3(originalPos.x, originalPos.y, -10);
    }

}
