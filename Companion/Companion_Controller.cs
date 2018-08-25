using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Companion_Controller : MonoBehaviour
{
    public static GameObject originNode, playerNode;

    //The player gameobject
    public GameObject player;
    //The enemy the player is hitting
    public static GameObject playerTarget;
    //In combat?
    bool inCombat = false;

    public bool invulnerable = true;
    public bool isSliding = false;

    public int attackCount;

    //Leon Unique Skills
    public GameObject shield;
    public static int shieldAmount = 0;
    public static int shieldAmountMax = 240;
    public List<GameObject> shieldList = new List<GameObject>();

    //0 Up, 1 Down, 2 Left, 3 Right
    int direction = 0;

    //Companion Attack Noises
    public static AudioClip[] attackNoise;
    public static AudioClip[] hurtNoise;
    public static AudioClip[] startNoise;
    public static AudioClip deathNoise;

    //Companion Stats
    public static int compCurrentHealth;
    public static int compMaxHealth;
    public static int compDamage;

    public Text healthNum;
    public GameObject healthBar;

    public bool invuln = false;

    //Pathfinding
    public GameObject currentNode, lastNode;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        invuln = false;
        healthBar = GameObject.Find("HPBar_Comp");
        healthNum = GameObject.Find("HPText_Comp").GetComponent<Text>();

        //Init Leon Noises
        if(TestCharController.companionID == 2)
        {
            attackNoise = new AudioClip[5];
            attackNoise[0] = (AudioClip)Resources.Load("Sound/Leon/Male_Attack_01_Patrick2");
            attackNoise[1] = (AudioClip)Resources.Load("Sound/Leon/Male_Attack_02_Patrick2");
            attackNoise[2] = (AudioClip)Resources.Load("Sound/Leon/Male_Attack_03_Patrick2");
            attackNoise[3] = (AudioClip)Resources.Load("Sound/Leon/Male_Attack_04_Patrick2");
            attackNoise[4] = (AudioClip)Resources.Load("Sound/Leon/Male_Attack_05_Patrick2");

            hurtNoise = new AudioClip[3];
            hurtNoise[0] = (AudioClip)Resources.Load("Sound/Leon/Male_Hurt_01_Patrick2");
            hurtNoise[1] = (AudioClip)Resources.Load("Sound/Leon/Male_Hurt_02_Patrick2");
            hurtNoise[2] = (AudioClip)Resources.Load("Sound/Leon/Male_Hurt_03_Patrick2");

            deathNoise = (AudioClip)Resources.Load("Sound/Leon/Male_Death_03_Patrick2");

            startNoise = new AudioClip[2];
            startNoise[0] = (AudioClip)Resources.Load("Sound/Leon/Male_BattleStart_01_Patrick2");
            startNoise[1] = (AudioClip)Resources.Load("Sound/Leon/Male_BattleStart_02_Patrick2");

            UpdateLeon();
            StartCoroutine(LeonBehavior());
        }

        //Init Risette Noises

        //Init Sparrow Noises


        
	}
	
	// Update is called once per frame
	void Update ()
    {
        var hpRect = healthBar.transform as RectTransform;
           
        float hpIncrement = 92.6f / compMaxHealth;
        float newWidth = compCurrentHealth * hpIncrement;
        hpRect.sizeDelta = new Vector2(newWidth, hpRect.sizeDelta.y);

        healthNum.text = compCurrentHealth.ToString();

        //print(playerTarget);
    }

    //Init Leon
    void UpdateLeon()
    {
        compMaxHealth = (PlayerStats.playerLevel * 25) + 100;
        compCurrentHealth = compMaxHealth;
        compDamage = (PlayerStats.playerLevel * 5) + 10;

        shieldList.Add(GameObject.Find("Shield_1"));
        shieldList.Add(GameObject.Find("Shield_2"));
        shieldList.Add(GameObject.Find("Shield_3"));
        shieldList.Add(GameObject.Find("Shield_4"));
        shieldList.Add(GameObject.Find("Shield_5"));
        shieldList.Add(GameObject.Find("Shield_6"));

        shieldAmountMax = 40 + ((PlayerStats.playerLevel / 5) * 40);

    }

    public void DamageCompanion(int damage)
    {
        compCurrentHealth -= damage;

        GameObject tempObj = Instantiate(Resources.Load("Prefabs/PlayerBattleText")) as GameObject;
        tempObj.transform.SetParent(GameObject.Find("HUD").transform, false);
        RectTransform tempRect = tempObj.GetComponent<RectTransform>();

        tempRect.position = new Vector2(transform.position.x, transform.position.y);
        tempObj.GetComponent<Text>().text = damage.ToString();

        if (compCurrentHealth > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, .3f, .3f);
            GetComponent<AudioSource>().clip = hurtNoise[Random.Range(0, hurtNoise.Length)];
            GetComponent<AudioSource>().Play();

            if(playerTarget != null && !isSliding)
            {
                StartCoroutine(SlideRoutine(playerTarget));
            }
            StartCoroutine(ColorReturnRoutine());
            StartCoroutine(IFrameRoutine());
        }
        else
        {
            if(!invuln)
            {
                GetComponent<AudioSource>().clip = deathNoise;
                GetComponent<AudioSource>().Play();
                invuln = true;
                //Play weak animation
                switch(direction)
                {
                    default:
                        break;
                    case 0:
                        GetComponent<Animator>().Play("C_Leon_Up_Weak");
                        GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
                        break;
                    case 1:
                        GetComponent<Animator>().Play("C_Leon_Down_Weak");
                        GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
                        break;
                    case 2:
                        GetComponent<Animator>().Play("C_Leon_Left_Weak");
                        GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
                        break;
                    case 3:
                        GetComponent<Animator>().Play("C_Leon_Right_Weak");
                        GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
                        break;
                }
            }
        }
    }

    public void DetermineFacing()
    {
        //Determine facing direction
        Vector2 vel = (playerTarget.transform.position - transform.position).normalized;
        if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
        {
            //Right
            if (vel.x > 0)
            {
                direction = 3;
            }
            //Left
            else if (vel.x < 0)
            {
                direction = 2;
            }
        }
        else if (Mathf.Abs(vel.y) > Mathf.Abs(vel.x))
        {
            //Up
            if (vel.y > 0)
            {
                direction = 0;
            }
            //Down
            else if (vel.y < 0)
            {
                direction = 1;
            }
        }
    }

    //Determines the closest enemy to the player to attack
    public void DetermineTarget()
    {
        //Initial Target that is not invuln
        List<GameObject> enemyList = new List<GameObject>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //Only count enemies that aren't invuln
            if (!enemy.GetComponent<Monster>().invulnerable)
            {
                enemyList.Add(enemy);
            }
        }
        //Pick an enemy if valid
        if (enemyList.Count > 0)
        {
            float distance = 5;
            for(int i = 0; i < enemyList.Count; i++)
            {
                if(Vector2.Distance(player.transform.position, enemyList[i].transform.position) < distance)
                {
                    distance = Vector2.Distance(transform.position, enemyList[i].transform.position);
                    playerTarget = enemyList[i];
                }
            }
        }
    }

    //Checks to see if the companion's target is still alive
    public bool CheckTarget()
    {
        List<GameObject> enemyList = new List<GameObject>();

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //Only count enemies that aren't invuln
            if (!enemy.GetComponent<Monster>().invulnerable)
            {
                enemyList.Add(enemy);
            }
        }

        if(enemyList.Count > 0)
        {
            for(int i = 0; i < enemyList.Count; i++)
            {
                if(enemyList[i] == playerTarget)
                {
                    return true;
                }
            }

        }
        return false;
    }

    
    //Hit Player?
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }        
    }
    

    IEnumerator ColorReturnRoutine()
    {
        float tempColor = .3f;
        while (tempColor <= 1)
        {
            yield return new WaitForSeconds(.05f);
            tempColor += .1f;
            GetComponent<SpriteRenderer>().color = new Color(1f, tempColor, tempColor);
        }
    }

    public IEnumerator SlideRoutine(GameObject target)
    {
        Vector2 vec = target.transform.position;
        isSliding = true;
        switch (direction)
        {
            default:
                break;
            case 0:
                //GetComponent<Animator>().Play("C_Leon_Up_Weak");
                break;
            case 1:
                //GetComponent<Animator>().Play("C_Leon_Down_Weak");
                break;
            case 2:
                //GetComponent<Animator>().Play("C_Leon_Left_Weak");
                break;
            case 3:
                //GetComponent<Animator>().Play("C_Leon_Right_Weak");
                break;
        }
        for(int i = 0; i < 15; i++)
        {
            GetComponent<Rigidbody2D>().velocity = ((Vector2)transform.position - vec).normalized * .15f;
            yield return new WaitForSeconds(.1f);
        }

        isSliding = false;
    }

    public IEnumerator IFrameRoutine()
    {
        float time = 1.5f;
        bool flash = false;
        invulnerable = true;
        while (time > 0)
        {
            yield return new WaitForSeconds(.1f);
            if (!flash)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .25f);
                flash = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .75f);
                flash = false;
            }
            time -= .1f;
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        invulnerable = false;
    }

    //==========================================
    //                 LEON
    //==========================================

    public void UpdateShieldHUD()
    {
        for(int i = 0; i < shieldList.Count; i++)
        {
            shieldList[i].SetActive(false);
        }
        //Determine how many to show
        int numShields = shieldAmountMax / 40;

        for(int i = 0; i < numShields; i++)
        {
            shieldList[i].SetActive(true);
            //Update the shield masks
            GameObject mask = shieldList[i].transform.Find("Shield_Mask").gameObject;
            int maskAmount = shieldAmount - (i * 40);
            if(maskAmount < 0)
            {
                maskAmount = 0;
            }

            var shieldRect = mask.transform as RectTransform;
            shieldRect.sizeDelta = new Vector2(shieldRect.sizeDelta.x, (maskAmount/40f) * 12f);
        }
    }

    IEnumerator LeonShieldReturn()
    {
        while(true)
        {
            if(shieldAmount < shieldAmountMax && !shield.activeInHierarchy)
            {
                shieldAmount++;
                //print(shieldAmount);              
            }
            UpdateShieldHUD();
            yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator LeonBehavior()
    {
        yield return new WaitForSeconds(.1f);
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y - .15f);
        invulnerable = false;

        StartCoroutine(LeonShieldReturn());

        //Is the level dark?
        if (LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].biomeLevel == 2 ||
            LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].biomeLevel == 4)
        {
            GetComponent<SpriteRenderer>().material = Resources.Load("Prefabs/Cave_Tiles/CaveMaterial") as Material;
        }

        switch (LevelCreator.startTag)
        {
            default:
                break;
            case "Up":
                GetComponent<Animator>().Play("C_Leon_Up_Idle");
                direction = 0;
                break;
            case "Down":
                GetComponent<Animator>().Play("C_Leon_Down_Idle");
                direction = 1;
                break;
            case "Left":
                GetComponent<Animator>().Play("C_Leon_Left_Idle");
                direction = 2;
                break;
            case "Right":
                GetComponent<Animator>().Play("C_Leon_Right_Idle");
                direction = 3;
                break;
        }

        yield return new WaitForSeconds(.5f);

        int startChance = Random.Range(0, 100);
        if(startChance < 25 && !LevelCreator.levelGrid[LevelCreator.playerCurrentX, LevelCreator.playerCurrentY].roomClear)
        {
            GetComponent<AudioSource>().clip = startNoise[Random.Range(0, startNoise.Length)];
            GetComponent<AudioSource>().Play();
        }

        while (compCurrentHealth > 0)
        {

            //Determine what companion should be attacking
            DetermineTarget();

            //Check if the companion is in combat
            if (CheckTarget())
            {
                inCombat = true;
            }
            else
            {
                inCombat = false;
            }

            //Determine Distance to Player
            float distance = Vector2.Distance(player.transform.position, transform.position);
            //print(distance);

            //---Not in combat Behavior----
            if(!inCombat && !isSliding && !TestCharController.inDialogue)
            {
                shield.SetActive(false);

                //Stop at player
                if (distance < .45f)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    //Determine Direction
                    switch(direction)
                    {
                        default:
                            break;
                        case 0:
                            GetComponent<Animator>().Play("C_Leon_Up_Idle");
                            break;
                        case 1:
                            GetComponent<Animator>().Play("C_Leon_Down_Idle");
                            break;
                        case 2:
                            GetComponent<Animator>().Play("C_Leon_Left_Idle");
                            break;
                        case 3:
                            GetComponent<Animator>().Play("C_Leon_Right_Idle");
                            break;
                    }
                }
                //Move to player, Walk
                else if (distance >= .45f && distance < .55f)
                {
                    LevelCreator.nodeGrid.GetComponent<Path_Nodes>().UpdateNodes();
                    originNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(transform.position, currentNode);
                    playerNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(player.transform.position, currentNode);

                    GetComponent<Rigidbody2D>().velocity = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().Pathfind(originNode, playerNode, gameObject, ref currentNode, ref lastNode) * .55f;
                    //Determine Direction
                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    if(Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
                    {
                        //Right
                        if(vel.x > 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Right_Walk");
                            direction = 3;
                        }
                        //Left
                        else if(vel.x < 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Left_Walk");
                            direction = 2;
                        }
                    }
                    else if(Mathf.Abs(vel.y) > Mathf.Abs(vel.x))
                    {
                        //Up
                        if(vel.y > 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Up_Walk");
                            direction = 0;
                        }
                        //Down
                        else if(vel.y < 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Down_Walk");
                            direction = 1;
                        }
                    }
                }
                //Move to player, Run
                else if(distance >= .55f /*&& distance < 1.25f*/)
                {
                    LevelCreator.nodeGrid.GetComponent<Path_Nodes>().UpdateNodes();
                    originNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(transform.position, currentNode);
                    playerNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(player.transform.position, currentNode);

                    GetComponent<Rigidbody2D>().velocity = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().Pathfind(originNode, playerNode, gameObject, ref currentNode, ref lastNode) * 1f;
                    //Determine Direction
                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
                    {
                        //Right
                        if (vel.x > 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Right_Run");
                            direction = 3;
                        }
                        //Left
                        else if (vel.x < 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Left_Run");
                            direction = 2;
                        }
                    }
                    else if (Mathf.Abs(vel.y) > Mathf.Abs(vel.x))
                    {
                        //Up
                        if (vel.y > 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Up_Run");
                            direction = 0;
                        }
                        //Down
                        else if (vel.y < 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Down_Run");
                            direction = 1;
                        }
                    }
                }
                //Teleport to player
                else
                {
                    transform.position = player.transform.position;  
                }
            }


            //---In Combat Behavior---
            else if(inCombat && !isSliding && !TestCharController.inDialogue)
            {
                ColliderDistance2D combatDistance = GetComponent<Collider2D>().Distance(playerTarget.GetComponent<Collider2D>());
                //print(combatDistance.distance);
                //Move towards the enemy based off enemy hitbox
                if(combatDistance.distance >= .18f)
                {
                    LevelCreator.nodeGrid.GetComponent<Path_Nodes>().UpdateNodes();
                    originNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(transform.position, currentNode);
                    playerNode = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().FindClosestNode(playerTarget.transform.position,currentNode);

                    GetComponent<Rigidbody2D>().velocity = LevelCreator.nodeGrid.GetComponent<Path_Nodes>().Pathfind(originNode, playerNode, gameObject, ref currentNode, ref lastNode) * .35f;
                    //GetComponent<Rigidbody2D>().velocity = (playerTarget.transform.position - transform.position).normalized * .35f;
                    Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                    if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
                    {
                        //Right
                        if (vel.x > 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Right_Walk");
                            direction = 3;
                            if(shieldAmount > 0)
                            {
                                shield.SetActive(true);
                                shield.transform.rotation = Quaternion.Euler(0, 0, 90);
                            }
                            else
                            {
                                shield.SetActive(false);
                            }
                        }
                        //Left
                        else if (vel.x < 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Left_Walk");
                            direction = 2;
                            if (shieldAmount > 0)
                            {
                                shield.SetActive(true);
                                shield.transform.rotation = Quaternion.Euler(0, 0, 270);
                            }
                            else
                            {
                                shield.SetActive(false);
                            }
                        }
                    }
                    else if (Mathf.Abs(vel.y) > Mathf.Abs(vel.x))
                    {
                        //Up
                        if (vel.y > 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Up_Walk");
                            direction = 0;
                            if (shieldAmount > 0)
                            {
                                shield.SetActive(true);
                                shield.transform.rotation = Quaternion.Euler(0, 0, 180);
                            }
                            else
                            {
                                shield.SetActive(false);
                            }
                        }
                        //Down
                        else if (vel.y < 0)
                        {
                            GetComponent<Animator>().Play("C_Leon_Down_Walk");
                            direction = 1;
                            if (shieldAmount > 0)
                            {
                                shield.SetActive(true);
                                shield.transform.rotation = Quaternion.Euler(0, 0, 0);
                            }
                            else
                            {
                                shield.SetActive(false);
                            }
                        }
                    }
                }
                
                //Attack the player target
                else
                {
                    //Determine Facing Direction
                    DetermineFacing();

                    shield.SetActive(false);

                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    switch (direction)
                    {
                        default:
                            break;
                        //Up
                        case 0:
                            GetComponent<Animator>().Play("C_Leon_Up_Attack1");
                            break;
                        //Down
                        case 1:
                            GetComponent<Animator>().Play("C_Leon_Down_Attack1");
                            break;
                        //Left
                        case 2:
                            GetComponent<Animator>().Play("C_Leon_Left_Attack1");
                            break;
                        //Right
                        case 3:
                            GetComponent<Animator>().Play("C_Leon_Right_Attack1");
                            break;
                    }
                    //Play Attack Audio
                    GetComponent<AudioSource>().clip = attackNoise[Random.Range(0, attackNoise.Length)];
                    GetComponent<AudioSource>().Play();

                    //GetComponent<Rigidbody2D>().velocity = (playerTarget.transform.position - transform.position).normalized * .1f;
                    yield return new WaitForSeconds(.75f);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                }

            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
