using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Kyle_Controller : Monster
{
    //1 Up 2 Down 3 Left 4 Right
    int direction = 0;
    bool attacking = false;

    public GameObject teleLeft, teleRight, teleUp, teleDown;
    public GameObject shield;
    public GameObject charge, chargeBar;

    public Text bossBarText;
    public GameObject bossBarFrame;
    public GameObject bossBar;

    public float bossBarWidth = 201.4f;
    public float monsterMaxHealth = 1000;

    public bool dodgeTut = false;
    public bool invulnTut = false;
    public bool heavyTut = false;
    public GameObject systemMessage;
    public Text systemText;

    public GameObject dazed;

    // Use this for initialization
    void Start ()
    {
        monsterHealth = 1000;
        contactDamage = 10;

        player = GameObject.FindGameObjectWithTag("Player");
        monsterSprite = GetComponent<SpriteRenderer>();
        monsterRB = GetComponent<Rigidbody2D>();

        unstoppable = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float healthPercent = (float)monsterHealth / (float)monsterMaxHealth;
        var xpBarRect = bossBar.transform as RectTransform;
        xpBarRect.sizeDelta = new Vector2(bossBarWidth * healthPercent, xpBarRect.sizeDelta.y);
    }

    public IEnumerator KyleBehavior()
    {
        bossBarText.text = "Kyle Strauss";
        bossBarFrame.SetActive(true);
        bossBar.SetActive(true);

        while (monsterHealth > 500)
        {
            float distance = Vector2.Distance(TestCharController.player.transform.position, transform.position);

            if(distance > .75f && !attacking)
            {
                GetComponent<Rigidbody2D>().velocity =  (TestCharController.player.transform.position - transform.position).normalized;
            }
            else if(distance <= .75f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if(!attacking)
                {
                    yield return StartCoroutine(ProcessAttack());
                }
            }

            DetermineAnimation();

            yield return new WaitForSeconds(.05f);
        }

        int hpSnapshot = monsterHealth;

        dazed.SetActive(true);

        if (!heavyTut)
        {
            systemMessage.SetActive(false);
            systemMessage.SetActive(true);
            systemMessage.GetComponent<AudioSource>().Play();
            if (GameController.xbox360Enabled())
            {
                systemText.text = "Attack Basics:\nYou can hold the [LT] to charge a heavy attack, and release it to deal increased damage.";
            }
            else
            {
                systemText.text = "Attack Basics:\nYou can hold the [RMB] to charge a heavy attack, and release it to deal increased damage.";
            }
            heavyTut = true;
        }

        switch (direction)
        {
            default:
                break;
            //up
            case 1:
                GetComponent<Animator>().Play("Ad_Up_Weak");
                teleUp.SetActive(true);
                break;
            //down
            case 2:
                GetComponent<Animator>().Play("Ad_Down_Weak");
                teleDown.SetActive(true);
                break;
            //left
            case 3:
                GetComponent<Animator>().Play("Ad_Left_Weak");
                teleLeft.SetActive(true);
                break;
            //right
            case 4:
                GetComponent<Animator>().Play("Ad_Right_Weak");
                teleRight.SetActive(true);
                break;
        }

        while(monsterHealth >= hpSnapshot)
        {
            yield return new WaitForSeconds(.5f);
        }
        invulnerable = true;



    }

    IEnumerator ProcessAttack()
    {
        attacking = true;

        //determine attack direction
        DetermineFacing();

        if(!dodgeTut)
        {
            systemMessage.SetActive(false);
            systemMessage.SetActive(true);
            systemMessage.GetComponent<AudioSource>().Play();
            if (GameController.xbox360Enabled())
            {
                systemText.text = "Dodge Basics:\nYou can dash to dodge with the [L3] Button.";
            }
            else
            {
                systemText.text = "Dodge Basics:\nYou can dash to dodge with the [Space] key.";
            }
            dodgeTut = true;
        }


        switch (direction)
        {
            default:
                break;
            //up
            case 1:
                GetComponent<Animator>().Play("Ad_Up_Tele");
                teleUp.SetActive(true);
                break;
            //down
            case 2:
                GetComponent<Animator>().Play("Ad_Down_Tele");
                teleDown.SetActive(true);
                break;
            //left
            case 3:
                GetComponent<Animator>().Play("Ad_Left_Tele");
                teleLeft.SetActive(true);
                break;
            //right
            case 4:
                GetComponent<Animator>().Play("Ad_Right_Tele");
                teleRight.SetActive(true);
                break;
        }

        yield return new WaitForSeconds(1.5f);

        //Play Attack Animation
        switch (direction)
        {
            default:
                break;
            //up
            case 1:
                GetComponent<Animator>().Play("Ad_Up_Jab");
                teleUp.SetActive(false);
                break;
            //down
            case 2:
                GetComponent<Animator>().Play("Ad_Down_Jab");
                teleDown.SetActive(false);
                break;
            //left
            case 3:
                GetComponent<Animator>().Play("Ad_Left_jab");
                teleLeft.SetActive(false);
                break;
            //right
            case 4:
               GetComponent<Animator>().Play("Ad_Right_Jab");
                teleRight.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(.5f);

        if(monsterHealth < 750)
        {
            if (!invulnTut)
            {
                systemMessage.SetActive(false);
                systemMessage.SetActive(true);
                systemMessage.GetComponent<AudioSource>().Play();
                systemText.text = "Status Basics:\nEnemies with glowing yellow outlines cannot be damaged with physical attacks.";
                invulnTut = true;
            }
            physicalResist = true;       
            //Play Attack Animation
            switch (direction)
            {
                default:
                    break;
                //up
                case 1:
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
                    GetComponent<Animator>().Play("Ad_Up_Charged");
                    teleUp.SetActive(false);
                    break;
                //down
                case 2:
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
                    GetComponent<Animator>().Play("Ad_Down_Charged");
                    teleDown.SetActive(false);
                    break;
                //left
                case 3:
                    GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);
                    GetComponent<Animator>().Play("Ad_Left_Charged");
                    teleLeft.SetActive(false);
                    break;
                //right
                case 4:
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
                    GetComponent<Animator>().Play("Ad_Right_Charged");
                    teleRight.SetActive(false);
                    break;
            }

            charge.SetActive(true);
            //shield.SetActive(true);
            //Change the charge bar
            for (int i = 0; i < 100; i++)
            {
                float scale = i / 100f;
                chargeBar.transform.localScale = new Vector2(scale, 1f);
                float offset = -.17f + (.17f * scale);
                chargeBar.transform.localPosition = new Vector2(offset, 0);
                yield return new WaitForSeconds(.025f);
            }


            GameObject.Find("Ability_Frame").GetComponent<Animator>().Play("Ability_Enter");
            GameObject.Find("Ability_Frame").GetComponent<AudioSource>().Play();
            GameObject.Find("Ability_Frame").transform.Find("Ability_Name").GetComponent<Text>().text = "Arcana: Firebolt";
            switch (direction)
            {
                default:
                    break;
                //up
                case 1:
                    GetComponent<Animator>().Play("Ad_Up_Cast");
                    yield return new WaitForSeconds(.5f);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), transform.position, Quaternion.Euler(0, 0, 90));
                    break;
                //down
                case 2:
                    GetComponent<Animator>().Play("Ad_Down_Cast");
                    yield return new WaitForSeconds(.5f);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), transform.position, Quaternion.Euler(0, 0, 270));
                    break;
                //left
                case 3:
                    GetComponent<Animator>().Play("Ad_Left_Cast");
                    yield return new WaitForSeconds(.5f);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), transform.position, Quaternion.Euler(0, 0, 180));
                    break;
                //right
                case 4:
                    GetComponent<Animator>().Play("Ad_Right_Cast");
                    yield return new WaitForSeconds(.5f);
                    Instantiate(Resources.Load("Prefabs/SpellFX/Cast_Fire"), transform.position, Quaternion.Euler(0, 0, 0));
                    break;
            }
            GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/EnemyAttack/Fireball_Enemy"), transform.position, Quaternion.identity);
            tempObj.GetComponent<FireBolt_Enemy>().direction = direction;
            yield return new WaitForSeconds(1.5f);

            charge.SetActive(false);
            //shield.SetActive(false);
            physicalResist = false;
        }

        attacking = false;
    }

    void DetermineFacing()
    {
        Vector2 dir = transform.position - player.transform.position;

        //Left or right
        if(Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
        {
            if(dir.x > 0)
            {
                direction = 3;
            }
            else
            {
                direction = 4;
            }
        }
        else
        {
            if(dir.y > 0)
            {
                direction = 2;
            }
            else
            {
                direction = 1;
            }
        }


    }

    void DetermineAnimation()
    {
        if ((GetComponent<Rigidbody2D>().velocity.x != 0 || GetComponent<Rigidbody2D>().velocity.y != 0))
        {
            //X component greater
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) >= Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y))
            {
                //Left
                if (GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    GetComponent<Animator>().Play("Ad_Left_Run");
                    direction = 3;
                }
                //Right
                else if (GetComponent<Rigidbody2D>().velocity.x > 0)
                {
                    GetComponent<Animator>().Play("Ad_Right_Run");
                    direction = 4;
                }
            }
            //Y component greater
            else
            {
                //Up
                if (GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    GetComponent<Animator>().Play("Ad_Up_Run");
                    direction = 1;
                }
                //Down
                else if (GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    GetComponent<Animator>().Play("Ad_Down_Run");
                    direction = 2;
                }
            }
        }
        else
        {
            switch(direction)
            {
                default:
                    break;
                case 1:
                    GetComponent<Animator>().Play("Ad_Up_Idle");
                    break;
                case 2:
                    GetComponent<Animator>().Play("Ad_Down_Idle");
                    break;
                case 3:
                    GetComponent<Animator>().Play("Ad_Left_Idle");
                    break;
                case 4:
                    GetComponent<Animator>().Play("Ad_Right_Idle");
                    break;
            }
        }
    }
}
